using API.EmailSender;
using API.Messages;
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
        EmailSend email = new EmailSend();
        Message message = new Message();
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
            string nombre_completo = string.Concat(reservacion.AP1Estudiante, " ", reservacion.AP2Estudiante, " ", reservacion.NombreEstudiante);

            var receptor = reservacion.CorreoEstudiante;
            var asunto = "Confirmación Reserva Laboratorio: " + reservacion.NombreLab;
            var mensaje = message.MensajeReserva(nombre_completo, reservacion.NombreLab, reservacion.Fecha.ToString(), reservacion.HoraInicio.ToString(), reservacion.HoraFin.ToString());
            await email.SendEmailAsync(receptor, asunto, mensaje);
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
            var profesor = await (from _ReservacionesP in _context.Reservaciones
                                  join _Profesores in _context.Profesores on _ReservacionesP.CedProf equals
                                  _Profesores.Cedula
                                  where _ReservacionesP.CedProf == reservacion.CedProf
                                  select new
                                  {
                                      _Profesores.Nombre,
                                      _Profesores.Ap1,
                                      _Profesores.Ap2,
                                      _Profesores.Correo
                                  }).FirstAsync();

            string nombre_completo = string.Concat(profesor.Ap1, " ", profesor.Ap2, " ", profesor.Nombre);
            var receptor = profesor.Correo;
            var asunto = "Confirmación Reserva Laboratorio: " + modelo.NombreLab;
            var mensaje = message.MensajeReserva(nombre_completo, reservacion.NombreLab, reservacion.Fecha.ToString(), reservacion.HoraInicio.ToString(), reservacion.HoraFin.ToString());
            await _context.Reservaciones.AddAsync(reservacion);
            await _context.SaveChangesAsync();
            await email.SendEmailAsync(receptor, asunto, mensaje);
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
                                           _ReservacionesEst.HoraFin,
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
        [Route("reservaciones_estudiantes/{nombrelab}/{fecha}")]
        public async Task<ActionResult<IEnumerable<Reservacion>>> ObtenerReservacionesEstudiantesNombreLabYFecha(string nombrelab, DateOnly fecha)
        {
            var reservaciones = await (from _ReservacionesEst in _context.Reservaciones
                                       where _ReservacionesEst.CedProf == null && _ReservacionesEst.NombreLab == nombrelab && _ReservacionesEst.Fecha == fecha
                                       select new
                                       {
                                           _ReservacionesEst.HoraInicio,
                                           _ReservacionesEst.HoraFin,
                                           _ReservacionesEst.CantHoras,
                                           _ReservacionesEst.NombreEstudiante,
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
                                           _ReservacionesP.HoraFin,
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

        [HttpGet]
        [Route("reservaciones_profesores/{nombrelab}/{fecha}")]
        public async Task<ActionResult<IEnumerable<Reservacion>>> ObtenerReservacionesProfesoresPorLabYFecha(string nombrelab, DateOnly fecha)
        {
            var reservaciones = await (from _ReservacionesP in _context.Reservaciones
                                       join _Profesores in _context.Profesores on _ReservacionesP.CedProf equals
                                       _Profesores.Cedula
                                       where _ReservacionesP.NombreLab == nombrelab && _ReservacionesP.Fecha == fecha
                                       select new
                                       {
                                           _ReservacionesP.HoraInicio,
                                           _ReservacionesP.HoraFin,
                                           _ReservacionesP.CantHoras,
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
        [HttpGet]
        [Route("reservaciones/{nombrelab}")]
        public async Task<ActionResult<IEnumerable<Reservacion>>> ObtenerReservacionesPorLab(string nombrelab)
        {
            var reservaciones = await (from _ReservacionesP in _context.Reservaciones
                                       where _ReservacionesP.NombreLab == nombrelab
                                       select new
                                       {
                                           _ReservacionesP.Fecha,
                                           _ReservacionesP.HoraInicio,
                                           _ReservacionesP.HoraFin,
                                           _ReservacionesP.CantHoras,
                                           _ReservacionesP.CedProf,
                                           _ReservacionesP.CarnetEstudiante,
                                           _ReservacionesP.NombreEstudiante,
                                       }
                                       ).ToListAsync();

            if (reservaciones == null)
            {
                return NotFound();
            }
            return Ok(reservaciones);
        }
        [HttpGet]
        [Route("reservaciones/{nombrelab}/{fecha}")]
        public async Task<ActionResult<IEnumerable<Reservacion>>> ObtenerReservacionesPorLabYFecha(string nombrelab, DateOnly fecha)
        {
            var reservaciones = await (from _ReservacionesP in _context.Reservaciones
                                       where _ReservacionesP.NombreLab == nombrelab && _ReservacionesP.Fecha == fecha
                                       select new
                                       {
                                           _ReservacionesP.HoraInicio,
                                           _ReservacionesP.HoraFin,
                                           _ReservacionesP.CantHoras,
                                           _ReservacionesP.CedProf,
                                           _ReservacionesP.CarnetEstudiante,
                                           _ReservacionesP.NombreEstudiante,
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
