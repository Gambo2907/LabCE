using API.Encriptacion;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


/*
 * LoginController: Se encarga de los logins 
 * */
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //Constructor de la clase que encripta y desencripta passwords, para así realizar las consultas necesarias con la db
        EncryptMD5 encrypt = new EncryptMD5();
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;

        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public LoginController(LabCEContext context)
        {
            _context = context;
        }

        /*
         *LoginOperador: Se encarga de corroborar los datos de logeo de un operador  
         */

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

        /*
         *LoginOperador: Se encarga de corroborar los datos de logeo de un administrador 
         */

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

        /*
         *LoginOperador: Se encarga de corroborar los datos de logeo de un profesor 
         */
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
