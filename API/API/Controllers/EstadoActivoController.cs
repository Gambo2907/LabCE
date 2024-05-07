using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*Controlador del modelo EstadoActivo el cual se encarga de generar todas las consultas que se encuentra en SQL Server
*/

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoActivoController : ControllerBase
    {
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;

        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public EstadoActivoController(LabCEContext context)
        {
            _context = context;
        }

        /*
         * ListaEstadosActivos: Obtiene los estados del activo existentes en la tabla EstadoActivos en la db
         * */

        [HttpGet]
        [Route("lista_estado_activos")]
        public async Task<ActionResult<IEnumerable<EstadoActivo>>> ListaEstadoActivos()
        {
            var estado_activos = await _context.Estado_Activos.ToListAsync();
            return Ok(estado_activos);
        }

        /*
        * ListaEstadosActivos: Obtiene los estados del activo por el id en la tabla EstadoActivos en la db
        * */
        [HttpGet]
        [Route("estado_activos/{id}")]
        public async Task<ActionResult<EstadoActivo>> ObtenerEstadoActivoPorID(int id)
        {
            var estado_activo = await _context.Estado_Activos.FindAsync(id);

            if (estado_activo == null)
            {
                return NotFound();
            }

            return Ok(estado_activo);
        }
    }
}
