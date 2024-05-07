using API.EmailSender;
using API.Encriptacion;
using API.Messages;
using API.Models;
using API.RandPassword;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*
 *ReservacionController:  
 */
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionController : ControllerBase
    {
        //Constructor de la clase encargarda de encriptar y desencriptar passwords
        EncryptMD5 encrypt = new EncryptMD5();
        //Constructor de la clase encargada de enviar correos 
        EmailSend email = new EmailSend();
        //Constructor de la clase encargada de generar passwords aleatorias 
        PasswordGen passwordGen = new PasswordGen();
        //Constructor de la clase encargada de generar un mensaje para las reservaciones
        Message message = new Message();
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;
        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public ReservacionController(LabCEContext context)
        {
            _context = context;
        }

        /*
         *CrearReservacionEstudiante: Se encarga de crear una nueva reservacion de lab por parte de un estudiante 
         */
        [HttpPost]
        [Route("crear_reservacion_estudiante")]
        public async Task<IActionResult> CrearReservacionEstudiante(Reservacion modelo)
        {
            Reservacion reservacion = new Reservacion()
            {
                ID = 0,
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
        /*
         *CrearReservacionProfesor: Se encarga de crear una nueva reservacion de lab por parte de un profesor
         */
        [HttpPost]
        [Route("crear_reservacion_profesor")]
        public async Task<IActionResult> CrearReservacionProfesor(Reservacion modelo)
        {
            Reservacion reservacion = new Reservacion()
            {
                ID = 0,
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

            var profesor = await (from _ReservacionesP in _context.Reservaciones
                                  join _Profesores in _context.Profesores on _ReservacionesP.CedProf equals
                                  _Profesores.Cedula
                                  where _Profesores.Cedula == reservacion.CedProf
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
            await email.SendEmailAsync(receptor, asunto, mensaje);
            return Ok();

        }
        /*
         *ObtenerReservacionEstudiantes: Se encarga de obtener todas las tuplas de reservacion de estudiantes 
         */
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
        /*
         *ObtenerReservacionEstudiantesNombreLabYFecha: Se encarga de obtener todas las tuplas de 
         *reservacion de estudiantes con los valores de nombrelab y fecha
         */
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
        /*
         *ObtenerReservacionesProfesores: Se encarga de obtener todas las tuplas de reservaciones hechas por profesores 
         */
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
        /*
         *ObtenerReservacionProfesoresNombreLabYFecha: Se encarga de obtener todas las tuplas de 
         *reservacion de profesores con los valores de nombrelab y fecha
         */
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
        /*
         * ObtenerReservacionesPorLab: se encarga de obtener todas las reservaciones por nombre de laboratorio
         * */
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
        /*
         *ObtenerReservacionesPorLabYFecha: Se encarga de obtener todas las tuplas de 
         *reservacion con los valores de nombrelab y fecha
         */
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
