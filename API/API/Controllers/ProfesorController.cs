using API.EmailSender;
using API.Encriptacion;
using API.Models;
using API.RandPassword;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        PasswordGen passwordGen = new PasswordGen();
        EncryptMD5 encrypt = new EncryptMD5();
        EmailSend email = new EmailSend();

        private readonly LabCEContext _context;
        public ProfesorController(LabCEContext context)
        {
            _context = context;
            
        }

        [HttpPost]
        [Route("crear_profesor")]
        public async Task<IActionResult> CrearProfesor(Profesor modelo)
        {
   
            Profesor profesor = new Profesor()
            {
                Cedula = modelo.Cedula,
                Correo = modelo.Correo,
                Password = encrypt.Encrypt(passwordGen.GeneratePassword(8)),
                Nombre = modelo.Nombre,
                Ap1 = modelo.Ap1,
                Ap2 = modelo.Ap2,
                Nacimiento = modelo.Nacimiento,
                //Calcular la edad basada en la fecha de nacimiento
                Edad = DateTime.Now.Year - modelo.Nacimiento.Year,
            };
            var receptor = profesor.Correo;
            var asunto = "Contraseña LabCE - Ventana Profesor";
            var mensaje = "Su contraseña para acceder al sistema es: " + encrypt.Decrypt(profesor.Password);
            await _context.Profesores.AddAsync(profesor);
            await _context.SaveChangesAsync();
            await email.SendEmailAsync(receptor, asunto, mensaje);
            return Ok();
        }

        [HttpGet]
        [Route("lista_profesores")]
        public async Task<ActionResult<IEnumerable<Profesor>>> ListaProfesores()
        {
            var profesores = await _context.Profesores.ToListAsync();
            return Ok(profesores);
        }

        [HttpGet]
        [Route("lista_profesores/{cedula}")]
        public async Task<ActionResult<Profesor>> ObtenerProfesorPorCedula(int cedula)
        {
            var profesor = await _context.Profesores.FindAsync(cedula);

            if (profesor == null)
            {
                return NotFound(); 
            }

            return Ok(profesor);
        }

        [HttpPut]
        [Route("actualizar_profesor")]
        public async Task<IActionResult> ActualizarProfesor(int cedula, Profesor profesor)
        {
            var ProfesorExistente = await _context.Profesores.FindAsync(cedula);
            ProfesorExistente!.Cedula = profesor.Cedula;
            ProfesorExistente!.Correo = profesor.Correo;
            ProfesorExistente!.Nombre = profesor.Nombre;
            ProfesorExistente!.Ap1 = profesor.Ap1;
            ProfesorExistente!.Ap2 = profesor.Ap2;
            ProfesorExistente!.Nacimiento = profesor.Nacimiento;
            ProfesorExistente!.Edad = DateTime.Now.Year - profesor.Nacimiento.Year;


            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("cambio_password_profesor")]
        public async Task<IActionResult> CambioPasswordProfesor(int cedula)
        {

            var ProfesorExistente = await _context.Profesores.FindAsync(cedula);
            if (ProfesorExistente == null)
            {
                return NotFound();
            }
            ProfesorExistente!.Password = encrypt.Encrypt(passwordGen.GeneratePassword(8));
            var receptor = ProfesorExistente.Correo;
            var asunto = "Nueva contraseña LabCE";
            var mensaje = "Su nueva contraseña para acceder al sistema es: " + encrypt.Decrypt(ProfesorExistente.Password);
            await email.SendEmailAsync(receptor, asunto, mensaje);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar_profesor")]
        public async Task<IActionResult> EliminarProfesor(int cedula)
        {
            var profesor_borrado = await _context.Profesores.FindAsync(cedula);
            _context.Profesores.Remove(profesor_borrado!);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
