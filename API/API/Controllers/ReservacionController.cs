using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionController : ControllerBase
    {
        private readonly LabCEContext _context;
        public ReservacionController(LabCEContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear_reservacion_estudiante")]
        public async Task<IActionResult> CrearReservacionEstudiante(Reservacion modelo)
        {
            Reservacion reservacion = new Reservacion()
            {
                ID = modelo.ID,
                Fecha = modelo.Fecha,
                HoraInicio = modelo.HoraInicio,
                HoraFin = modelo.HoraFin,
                CantHoras = modelo.HoraFin.Hour - modelo.HoraInicio.Hour,
                NombreLab = modelo.NombreLab,
                CedProf = null,
                CarnetOP = modelo.CarnetOP,
                NombreEstudiante = modelo.NombreEstudiante,
                AP1Estudiante = modelo.AP1Estudiante,
                AP2Estudiante = modelo.AP2Estudiante,
                CorreoEstudiante = modelo.CorreoEstudiante,
                CarnetEstudiante = modelo.CarnetEstudiante,
                
            };
            await _context.Reservaciones.AddAsync(reservacion);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpPost]
        [Route("crear_reservacion_profesor")]
        public async Task<IActionResult> CrearReservacionProfesor(Reservacion modelo)
        {
            Reservacion reservacion = new Reservacion()
            {
                ID = modelo.ID,
                Fecha = modelo.Fecha,
                HoraInicio = modelo.HoraInicio,
                HoraFin = modelo.HoraFin,
                CantHoras = modelo.HoraFin.Hour - modelo.HoraInicio.Hour,
                NombreLab = modelo.NombreLab,
                CedProf = modelo.CedProf,
                CarnetOP = null,
                NombreEstudiante = null,
                AP1Estudiante = null,
                AP2Estudiante = null,
                CorreoEstudiante = null,
                CarnetEstudiante = null,
            };
            await _context.Reservaciones.AddAsync(reservacion);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet]
        [Route("reservaciones_estudiantes")]
        public async Task<ActionResult<IEnumerable<Reservacion>>> ObtenerReservacionesEstudiantes()
        {
            var reservaciones = await (from _ReservacionesEst in _context.Reservaciones
                                       where _ReservacionesEst.CedProf == null
                                       select new
                                       {
                                           _ReservacionesEst.Fecha,
                                           _ReservacionesEst.HoraInicio,
                                           _ReservacionesEst.CantHoras,
                                           _ReservacionesEst.NombreLab,
                                           _ReservacionesEst.CarnetEstudiante
                                       }
                                       ).ToListAsync();

            if (reservaciones == null)
            {
                return NotFound();
            }
            return Ok(reservaciones);
        }
        [HttpGet]
        [Route("reservaciones_profesores")]
        public async Task<ActionResult<IEnumerable<Reservacion>>> ObtenerReservacionesProfesores()
        {
            var reservaciones = await (from _ReservacionesP in _context.Reservaciones
                                       join _Profesores in _context.Profesores on _ReservacionesP.CedProf equals
                                       _Profesores.Cedula
                                       select new
                                       {
                                           _ReservacionesP.Fecha,
                                           _ReservacionesP.HoraInicio,
                                           _ReservacionesP.CantHoras,
                                           _ReservacionesP.NombreLab,
                                           _Profesores.Nombre,
                                           _Profesores.Ap1,
                                           _Profesores.Ap2
                                       }
                                       ).ToListAsync();

            if (reservaciones == null)
            {
                return NotFound();
            }
            return Ok(reservaciones);
        }
    }
}
