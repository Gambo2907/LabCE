using API.EmailSender;
using API.Encriptacion;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadorController : ControllerBase
    {
        EncryptMD5 encrypt = new EncryptMD5();
        EmailSend email = new EmailSend();
        private readonly LabCEContext _context;
        public OperadorController(LabCEContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("registrar_operador")]
        public async Task<IActionResult>RegistrarOperador(Operador operador)
        {
            //Encriptador
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

        [HttpGet]
        [Route("lista_operadores")]
        public async Task<ActionResult<IEnumerable<Operador>>> ListaOperadores()
        {
            var operadores = await _context.Operadores.ToListAsync();
            return Ok(operadores);
        }


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

        [HttpGet]
        [Route("lista_operadores_no_aprobados")]
        public async Task<ActionResult<IEnumerable<Operador>>> ListaOperadoresNoAprobados()
        {

            var operadores_no_aprobados = await _context.Operadores.Where(o => o.Aprobado == false).ToListAsync();
            return Ok(operadores_no_aprobados);
        }

        [HttpPut]
        [Route("actualizar_operador")]
        public async Task<IActionResult> ActualizarOperador(int carnet, Operador operador)
        {
            var OperadorExistente = await _context.Operadores.FindAsync(carnet);
            OperadorExistente!.Cedula = operador.Carnet;   
            OperadorExistente!.Correo = operador.Correo;
            OperadorExistente!.Password = operador.Password;
            OperadorExistente!.Nombre = operador.Nombre;
            OperadorExistente!.Ap1 = operador.Ap1;
            OperadorExistente!.Ap2 = operador.Ap2;
            OperadorExistente!.Nacimiento = operador.Nacimiento;
            OperadorExistente!.Edad = DateTime.Now.Year - operador.Nacimiento.Year;


            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("aprobar_operador")]
        public async Task<IActionResult>AprobarOperador(int carnet)
        {
            var OperadorExistente = await _context.Operadores.FindAsync(carnet);
            OperadorExistente!.Aprobado = true;
            await _context.SaveChangesAsync();
            var receptor = OperadorExistente.Correo;
            var asunto = "Ingreso Aprobado al sistema LabCE";
            var mensaje = "Un admin ha aprobado su ingreso al sistema LabCE, a partir de este momento puede operar con normalidad";
            await email.SendEmailAsync(receptor, asunto, mensaje);
            return Ok();
        }
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
