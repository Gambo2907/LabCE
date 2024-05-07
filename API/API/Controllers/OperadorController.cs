using API.EmailSender;
using API.Encriptacion;
using API.Models;
using API.RandPassword;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*OperadorController el cual se encarga de generar todas las consultas, además de añadir o
 * eliminar datos de la tabla Operadores que se encuentra en SQL Server
*/

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadorController : ControllerBase
    {
        //Constructor de la clase encargarda de encriptar y desencriptar passwords
        EncryptMD5 encrypt = new EncryptMD5();
        //Constructor de la clase encargada de enviar correos 
        EmailSend email = new EmailSend();
        //Constructor de la clase encargada de generar passwords aleatorias 
        PasswordGen passwordGen = new PasswordGen();
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;
        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public OperadorController(LabCEContext context)
        {
            _context = context;
        }

        /*
         *RegistrarOperador: se encarga de añadir una tupla a la tabla Operadores en la db 
         */

        [HttpPost]
        [Route("registrar_operador")]
        public async Task<IActionResult>RegistrarOperador(Operador operador)
        {
           
            Operador operador1 = new Operador()
            {
                Carnet = operador.Carnet,
                Cedula = operador.Cedula,
                Correo = operador.Correo,
                Password = encrypt.Encrypt(operador.Password),
                Nombre = operador.Nombre,
                Ap1 = operador.Ap1,
                Ap2 = operador.Ap2,
                Nacimiento = operador.Nacimiento,
                //Calcular la edad basada en la fecha de nacimiento
                Edad = DateTime.Now.Year - operador.Nacimiento.Year,
                Aprobado = false
            };
            await _context.Operadores.AddAsync(operador1);
            await _context.SaveChangesAsync();

            return Ok();
        }
        /*
         *ListaOperadores: se encarga de obtener todas las tuplas a la tabla Operadores en la db 
         */
        [HttpGet]
        [Route("lista_operadores")]
        public async Task<ActionResult<IEnumerable<Operador>>> ListaOperadores()
        {
            var operadores = await (from _O in _context.Operadores
                                    select new
                                    {
                                        _O.Carnet,
                                        _O.Cedula,
                                        _O.Correo,
                                        _O.Nombre,
                                        _O.Ap1,
                                        _O.Ap2,
                                        _O.Nacimiento,
                                        _O.Edad,
                                        _O.Aprobado
                                    }).ToListAsync();
            return Ok(operadores);
        }

        /*
         *ObtenerOperadorCarnet: se encarga de obtener la tupla de la tabla Operadores con el atributo carnet
         *digitado
         */

        [HttpGet]
        [Route("lista_operadores/{carnet}")]
        public async Task<ActionResult<Operador>> ObtenerOperadorCarnet(int carnet)
        {   
            var operador = await _context.Operadores.FindAsync(carnet);
            
            if( operador == null)
            {
                return NotFound();
            }
            return Ok(operador);
        }
        /*
        *ListaOperadoresNoAprobados: se encarga de obtener las tupla de la tabla Operadores con el atributo aprobado
        *falso
        */
        [HttpGet]
        [Route("lista_operadores_no_aprobados")]
        public async Task<ActionResult<IEnumerable<Operador>>> ListaOperadoresNoAprobados()
        {

            var operadores_no_aprobados = await (from _O in _context.Operadores
                                                 where _O.Aprobado == false
                                                 select new
                                                 {
                                                     _O.Carnet,
                                                     _O.Cedula,
                                                     _O.Correo,
                                                     _O.Nombre,
                                                     _O.Ap1,
                                                     _O.Ap2,
                                                     _O.Nacimiento,
                                                     _O.Edad,
                                                 }).ToListAsync();
            return Ok(operadores_no_aprobados);
        }
        /*
        *ActualizarOperador: se encarga de obtener la tupla de la tabla Operadores con el atributo carnet
        *digitado y actualiza los atributos con los valores digitados por el usuario.
        */
        [HttpPut]
        [Route("actualizar_operador")]
        public async Task<IActionResult> ActualizarOperador(int carnet, Operador operador)
        {
            var OperadorExistente = await _context.Operadores.FindAsync(carnet);
            OperadorExistente!.Cedula = operador.Carnet;   
            OperadorExistente!.Correo = operador.Correo;
            OperadorExistente!.Nombre = operador.Nombre;
            OperadorExistente!.Ap1 = operador.Ap1;
            OperadorExistente!.Ap2 = operador.Ap2;
            OperadorExistente!.Nacimiento = operador.Nacimiento;
            OperadorExistente!.Edad = DateTime.Now.Year - operador.Nacimiento.Year;


            await _context.SaveChangesAsync();
            return Ok();
        }
        /*
         *ActualizarPasswordOperador: se encarga de actualizar el atributo password de la tupla con el 
         *atributo carnet igual al valor digitado por el usuario, el password se genera aleatoriamente con
         *el metodo de la clase PasswordGen y se encripta con el metodo de la clase EncryptMD5, además envia un correo
         *al usuario con su nuevo password con el metodo de la clase EmailSend
         */
        [HttpPut]
        [Route("actualizar_password_operador")]
        public async Task<IActionResult> ActualizarPasswordOperador(int carnet)
        {
            var OperadorExistente = await _context.Operadores.FindAsync(carnet);
            
            OperadorExistente!.Password = encrypt.Encrypt(passwordGen.GeneratePassword(8)); ;
            var receptor = OperadorExistente.Correo;
            var asunto = "Contraseña LabCE";
            var mensaje = "Su nueva contraseña para acceder al sistema es: " + encrypt.Decrypt(OperadorExistente.Password);
            await _context.SaveChangesAsync();
            await email.SendEmailAsync(receptor, asunto, mensaje);
            return Ok();
        }
        /*
         *AprobarOperador: Se encarga de actualizar el atributo aprobado de la tupla correspondiente al
         *atributo carnet igual al valor digitado por el usuario
         */
        [HttpPut]
        [Route("aprobar_operador")]
        public async Task<IActionResult>AprobarOperador(int carnet)
        {
            var OperadorExistente = await _context.Operadores.FindAsync(carnet);
            OperadorExistente!.Aprobado = true;
            await _context.SaveChangesAsync();
            var receptor = OperadorExistente.Correo;
            var asunto = "Ingreso Aprobado al sistema LabCE - Ventana Operador";
            var mensaje = "Un admin ha aprobado su ingreso al sistema LabCE, a partir de este momento puede operar con normalidad.";
            await email.SendEmailAsync(receptor, asunto, mensaje);
            return Ok();
        }
        /*
         *EliminarOperador: se encarga de eliminar la tupla correspondiente al atributo carnet
         *igual al valor digitado por el usuario.
         */
        [HttpDelete]
        [Route("eliminar_operador")]
        public async Task<IActionResult> EliminarOperador(int carnet)
        {
            var operador_borrado = await _context.Operadores.FindAsync(carnet);
            _context.Operadores.Remove(operador_borrado!);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
