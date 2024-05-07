using API.Encriptacion;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/*Controlador del modelo Administrador el cual se encarga de generar todas las consultas 
 * de la tabla Administradores que se encuentra en SQL Server
*/

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;

        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public AdministradorController(LabCEContext context)
        {
            _context = context;
        }

        /*
        * ObtenerAdministrador: se encarga de obtener la tupla correspondiente al correo consultado en la
        * tabla Administradores de la db.
        * 
        */
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
