using API.Encriptacion;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        EncryptMD5 encrypt = new EncryptMD5();
        private readonly LabCEContext _context;
        public LoginController(LabCEContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login_operador")]
        public async Task<IActionResult> LoginOperador(LoginModel modelo)
        {
            
            Operador? operador = await _context.Operadores.Where(o => o.Correo == modelo.Correo && o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if(operador == null)
            {
                return NotFound();
            }
            return Ok(operador);
        }
        [HttpPost]
        [Route("login_admin")]
        public async Task<IActionResult> LoginAdmin(LoginModel modelo)
        {
            
            Administrador? admin = await _context.Administradores.Where(o => o.Correo == modelo.Correo && o.Password == modelo.Password).FirstOrDefaultAsync();
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }
        [HttpPost]
        [Route("login_profesor")]
        public async Task<IActionResult> LoginProfesores(LoginModel modelo)
        {
            Profesor? profesor = await _context.Profesores.Where(o => o.Correo == modelo.Correo && o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if (profesor == null)
            {
                return NotFound();
            }
            return Ok(profesor);
        }


    }
}
