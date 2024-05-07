using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*Controlador del modelo Laboratorio el cual se encarga de generar todas las consultas que se encuentra en SQL Server
*/
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;

        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public LaboratorioController(LabCEContext context)
        {
            _context = context;
        }
        /*
         * CrearLaboratorio: se encarga de añadir una tupla a la tabla Laboratorios en la db 
         */

        [HttpPost]
        [Route("crearlab")]
        public async Task<IActionResult>CrearLaboratorio(Laboratorio lab)
        {
            await _context.Laboratorios.AddAsync(lab);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /*
        * ListaLabs: se encarga de obtener todas las tuplas de la tabla Laboratorios en la db
        */
        [HttpGet]
        [Route("lista_labs")]
        public async Task<ActionResult<IEnumerable<Laboratorio>>> ListaLabs()
        {
            var labs = await (from _L in _context.Laboratorios
                                    select new
                                    {
                                        _L.Nombre,
                                        _L.Hora_Inicio,
                                        _L.Hora_Final,
                                        _L.Capacidad,
                                        _L.Computadores,
                                        _L.Facilidades
                                    }).ToListAsync(); 
            return Ok(labs);
        }
        /*
       * ListaLabs: se encarga de obtener la tupla con el nombre digitado de la tabla Laboratorios en la db
       */
        [HttpGet]
        [Route("lista_labs/{nombre}")]
        public async Task<ActionResult<Laboratorio>> ObtenerLabPorId(string nombre)
        {
            var lab = await _context.Laboratorios.FindAsync(nombre);

            if (lab == null)
            {
                return NotFound(); // Devuelve 404 si el laboratorio no se encuentra
            }

            return Ok(lab);
        }

        /*
       * ListaLabs: se encarga de editar la tupla con el nombre digitado de la tabla Laboratorios en la db
       */

        [HttpPut]
        [Route("actualizarlab")]
        public async Task<IActionResult>ActualizarLab(string nombre, Laboratorio lab)
        {
            var labExistente = await _context.Laboratorios.FindAsync(nombre);
            labExistente!.Nombre = lab.Nombre;
            labExistente!.Hora_Inicio = lab.Hora_Inicio;
            labExistente!.Hora_Final = lab.Hora_Final;
            labExistente!.Capacidad = lab.Capacidad;
            labExistente!.Computadores = lab.Computadores;
            labExistente.Facilidades = lab.Facilidades;

            await _context.SaveChangesAsync();
            return Ok();
        }

        /*
       * ListaLabs: se encarga de eliminar la tupla con el nombre digitado de la tabla Laboratorios en la db
       */

        [HttpDelete]
        [Route("eliminarlab")]
        public async Task<IActionResult>EliminarLab(string nombre)
        {
            var lab_borrado = await _context.Laboratorios.FindAsync(nombre);
            _context.Laboratorios.Remove(lab_borrado!);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
