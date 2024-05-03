using API.Encriptacion;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        EncryptMD5 encrypt = new EncryptMD5();
        private readonly LabCEContext _context;
        public AutenticacionController(LabCEContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("autenticacion_operador")]
        public async Task<IActionResult> AutenticacionOperador(AutenticacionModel modelo)
        {

            Operador? operador = await _context.Operadores.Where(o => o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if (operador == null)
            {
                return NotFound();
            }
            return Ok(operador);
        }
        [HttpPost]
        [Route("autenticacion_profesor")]
        public async Task<IActionResult> AutenticacionProfesor(AutenticacionModel modelo)
        {

            Profesor? profesor = await _context.Profesores.Where(o => o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if (profesor == null)
            {
                return NotFound();
            }
            return Ok(profesor);
        }
    }
}
