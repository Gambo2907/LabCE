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
        private readonly LabCEContext _context;
        public LoginController(LabCEContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login_operador")]
        public async Task<IActionResult> LoginOperador(LoginModel modelo)
        {
            EncryptMD5 encrypt = new EncryptMD5();
            Operador? operadors = await _context.Operadores.Where(o => o.Correo == modelo.Correo && o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if(operadors == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPost]
        [Route("login_admin")]
        public async Task<IActionResult> LoginAdmin(LoginModel modelo)
        {
            EncryptMD5 encrypt = new EncryptMD5();
            Administrador? admin = await _context.Administradores.Where(o => o.Correo == modelo.Correo && o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if (admin == null)
            {
                return NotFound();
            }
            return Ok();
        }
        public async Task<IActionResult> LoginProfesores(LoginModel modelo)
        {
            EncryptMD5 encrypt = new EncryptMD5();
            Profesor? profesor = await _context.Profesores.Where(o => o.Correo == modelo.Correo && o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if (profesor == null)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}
