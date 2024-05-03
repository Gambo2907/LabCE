using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoActivoController : ControllerBase
    {
        private readonly LabCEContext _context;
        public EstadoActivoController(LabCEContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("lista_estado_activos")]
        public async Task<ActionResult<IEnumerable<EstadoActivo>>> ListaEstadoActivos()
        {
            var estado_activos = await _context.Estado_Activos.ToListAsync();
            return Ok(estado_activos);
        }
        [HttpGet]
        [Route("lista_estado_activos/{id}")]
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
