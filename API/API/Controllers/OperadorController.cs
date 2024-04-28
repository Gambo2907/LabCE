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
            EncryptMD5 encrypt = new EncryptMD5();
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
                Aprobado = "No"
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
