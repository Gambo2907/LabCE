using API.EmailSender;
using API.Messages;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly LabCEContext _context;
        public PrestamosController(LabCEContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear_prestamo_estudiante")]
        public async Task<IActionResult> CrearPrestamoEstudiante(Prestamo modelo)
        {
            Prestamo prestamo = new Prestamo()
            {
                ID = 0,
                Fecha = modelo.Fecha,
                Hora = modelo.Hora,
                PlacaActivo = modelo.PlacaActivo,
                CedProf = null,
                CarnetOP = modelo.CarnetOP,
                NombreEstudiante = modelo.NombreEstudiante,
                AP1Estudiante = modelo.AP1Estudiante,
                AP2Estudiante = modelo.AP2Estudiante,
                CorreoEstudiante = modelo.CorreoEstudiante,

            };
            var ActivoExistente = await _context.Activos.FindAsync(prestamo.PlacaActivo);
            ActivoExistente!.Id_Estado = 2;
            await _context.Prestamos.AddAsync(prestamo);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpPost]
        [Route("crear_prestamo_profesor")]
        public async Task<IActionResult> CrearPrestamoProfesor(Prestamo modelo)
        {
            Prestamo prestamo = new Prestamo()
            {
                ID = 0,
                Fecha = modelo.Fecha,
                Hora = modelo.Hora,
                PlacaActivo = modelo.PlacaActivo,
                CedProf = modelo.CedProf,
                CarnetOP = null,
                NombreEstudiante = null,
                AP1Estudiante = null,
                AP2Estudiante = null,
                CorreoEstudiante = null,

            };
            var ActivoExistente = await _context.Activos.FindAsync(prestamo.PlacaActivo);
            ActivoExistente!.Id_Estado = 2;
            await _context.Prestamos.AddAsync(prestamo);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet]
        [Route("prestamos_estudiantes")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> ObtenerPrestamosEstudiantes()
        {
            var prestamos = await (from _PrestamosE in _context.Prestamos
                                       where _PrestamosE.CedProf == null
                                       select new
                                       {
                                           _PrestamosE.Fecha,
                                           _PrestamosE.Hora,
                                           _PrestamosE.NombreEstudiante,
                                           _PrestamosE.AP1Estudiante,
                                           _PrestamosE.AP2Estudiante,
                                           _PrestamosE.CorreoEstudiante,
                                       }
                                       ).ToListAsync();

            if (prestamos == null)
            {
                return NotFound();
            }
            return Ok(prestamos);
        }
        [HttpGet]
        [Route("prestamos_profesores")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> ObtenerPrestamosProfesores()
        {
            var prestamos = await (from _PrestamosP in _context.Prestamos
                                   join _Pr in _context.Profesores on _PrestamosP.CedProf equals
                                   _Pr.Cedula
                                   select new
                                   {
                                       _PrestamosP.Fecha,
                                       _PrestamosP.Hora,
                                       _Pr.Nombre,
                                       _Pr.Ap1,
                                       _Pr.Ap2,
                                       _Pr.Correo
                                   }).ToListAsync();

            if (prestamos == null)
            {
                return NotFound();
            }
            return Ok(prestamos);
        }
        [HttpGet]
        [Route("prestamos_profesores/{cedula}")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> ObtenerPrestamosProfesor(int cedula)
        {
            var prestamos = await (from _PrestamosP in _context.Prestamos
                                   join _Pr in _context.Profesores on _PrestamosP.CedProf equals
                                   _Pr.Cedula
                                   where _PrestamosP.CedProf == cedula
                                   select new
                                   {
                                       _PrestamosP.Fecha,
                                       _PrestamosP.Hora,
                                       _Pr.Nombre,
                                       _Pr.Ap1,
                                       _Pr.Ap2,
                                       _Pr.Correo
                                   }).ToListAsync();

            if (prestamos == null)
            {
                return NotFound();
            }
            return Ok(prestamos);
        }
    }
}
