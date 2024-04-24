using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly LabCEContext _context;
        public ProfesorController(LabCEContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear_profesor")]
        public async Task<IActionResult> CrearProfesor(Profesor profesor)
        {
            // Método para calcular la edad basada en la fecha de nacimiento
            profesor.Edad = DateTime.Now.Year - profesor.Nacimiento.Year;
            await _context.Profesores.AddAsync(profesor);
            await _context.SaveChangesAsync();

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
            ProfesorExistente!.Password = profesor.Password;
            ProfesorExistente!.Nombre = profesor.Nombre;
            ProfesorExistente!.Ap1 = profesor.Ap1;
            ProfesorExistente!.Ap2 = profesor.Ap2;
            ProfesorExistente!.Nacimiento = profesor.Nacimiento;
            ProfesorExistente!.Edad = DateTime.Now.Year - profesor.Nacimiento.Year;


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
