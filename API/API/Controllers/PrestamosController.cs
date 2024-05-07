using API.EmailSender;
using API.Messages;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
/*
 *PrestamosController: se encarga de generar todas las consultas, además de añadir datos 
 *en la tabla Prestamos que se encuentra en SQL Server 
 */
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;
        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public PrestamosController(LabCEContext context)
        {
            _context = context;
        }
        /*
         *CrearPrestamoEstudiante: se encarga de crear una nueva tupla en la tabla prestamos 
         */

        [HttpPost]
        [Route("crear_prestamo_estudiante")]
        public async Task<IActionResult> CrearPrestamoEstudiante(Prestamo modelo)
        {
            Prestamo prestamo = new Prestamo()
            {
                ID = 0,
                Fecha = DateOnly.Parse(DateTime.Today.ToString("yyyy-MM-dd")),
                Hora = TimeOnly.Parse(DateTime.Today.ToString("HH:mm:ss")),
                PlacaActivo = modelo.PlacaActivo,
                CedProf = null,
                CarnetOP = modelo.CarnetOP,
                NombreEstudiante = modelo.NombreEstudiante,
                AP1Estudiante = modelo.AP1Estudiante,
                AP2Estudiante = modelo.AP2Estudiante,
                CorreoEstudiante = modelo.CorreoEstudiante,

            };
            var ActivoExistente = await _context.Activos.FindAsync(prestamo.PlacaActivo);
            if(ActivoExistente.Req_Aprobador == false)
            {
                ActivoExistente!.Id_Estado = 2;
            }
            else
            {
                ActivoExistente!.Id_Estado = 4;

            }
            await _context.Prestamos.AddAsync(prestamo);
            await _context.SaveChangesAsync();
            return Ok();

        }
        /*
         *CrearPrestamoProfesor: se encarga de crear una nueva tupla en la tabla prestamos 
         */

        [HttpPost]
        [Route("crear_prestamo_profesor")]
        public async Task<IActionResult> CrearPrestamoProfesor(Prestamo modelo)
        {
            Prestamo prestamo = new Prestamo()
            {
                ID = 0,
                Fecha = DateOnly.Parse(DateTime.Today.ToString("yyyy-MM-dd")),
                Hora = TimeOnly.Parse(DateTime.Today.ToString("HH:mm:ss")),
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
        /*
         *ObtenerPrestamoEstudiantes: se encarga de obtener todas las tuplas de prestamos de estudiantes 
         */
        [HttpGet]
        [Route("prestamos_estudiantes")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> ObtenerPrestamosEstudiantes()
        {
            var prestamos = await (from _PrestamosE in _context.Prestamos
                                   join _A in _context.Activos on _PrestamosE.PlacaActivo equals _A.Placa
                                       where _PrestamosE.CedProf == null && _A.Id_Estado == 2
                                       select new
                                       {
                                           _PrestamosE.Fecha,
                                           _PrestamosE.Hora,
                                           _A.Placa,
                                           _A.Tipo,
                                           _A.Marca,
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
        /*
         *ObtenerPrestamoProfesores: se encarga de obtener todas las tuplas de prestamos de profesores 
         */
        [HttpGet]
        [Route("prestamos_profesores")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> ObtenerPrestamosProfesores()
        {
            var prestamos = await (from _PrestamosP in _context.Prestamos
                                   join _Pr in _context.Profesores on _PrestamosP.CedProf equals
                                   _Pr.Cedula
                                   join _A in _context.Activos on _PrestamosP.PlacaActivo equals _A.Placa
                                   where _A.Id_Estado == 2
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

        /*
         *ObtenerPrestamoProfesor: se encarga de obtener todas las tuplas de prestamos de profesores con el valor
         *cedula
         */

        [HttpGet]
        [Route("prestamos_profesores/{cedula}")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> ObtenerPrestamosProfesor(int cedula)
        {
            var prestamos = await (from _PrestamosP in _context.Prestamos
                                   join _Pr in _context.Profesores on _PrestamosP.CedProf equals
                                   _Pr.Cedula
                                   join _A in _context.Activos on _PrestamosP.PlacaActivo equals _A.Placa
                                   where _PrestamosP.CedProf == cedula && _A.Id_Estado == 2
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

        /*
         * ObtenerPrestamosSinAprobacion: Obtiene los prestamos que no han sido aprobados por el profesor correspondiente 
         */
        [HttpGet]
        [Route("prestamos_sin_aprobacion")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> ObtenerPrestamosSinAprobacion()
        {
            var prestamos = await (from _P in _context.Prestamos
                                   join _A in _context.Activos on _P.PlacaActivo equals _A.Placa
                                   where _A.Id_Estado == 4
                                   select new
                                   {
                                       _P.Fecha,
                                       _P.Hora,
                                       _A.Placa,
                                       _A.Marca,
                                       _A.Tipo

                                   }).ToListAsync();

            if (prestamos == null)
            {
                return NotFound();
            }
            return Ok(prestamos);
        }
    }
}
