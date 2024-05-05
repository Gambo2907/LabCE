using API.Encriptacion;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly LabCEContext _context;
        public AdministradorController(LabCEContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("obtener_administrador")]
        public async Task<ActionResult<EstadoActivo>> ObtenerAdministrador(string correo)
        {
            var admin = await _context.Estado_Activos.FindAsync(correo);

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }
    }
}
