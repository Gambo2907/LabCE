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
        [Route("crear_operador")]
        public async Task<IActionResult> CrearOperador(Operador operador)
        {
            //Calcular la edad basada en la fecha de nacimiento
            operador.Edad = DateTime.Now.Year - operador.Nacimiento.Year;
            //Encriptador
            EncryptMD5 encrypt = new EncryptMD5();
            operador.Password = encrypt.Encrypt(operador.Password);
            operador.Aprobado = "No";
            await _context.Operadores.AddAsync(operador);
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
        [Route("lista_operadores/{correo}")]
        public async Task<ActionResult<Operador>> ObtenerOperadorPorCarnet(int carnet)
        {
            var operador = await _context.Operadores.FindAsync(carnet);
            
            if (operador == null)
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
