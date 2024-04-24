using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivoController : ControllerBase
    {
        private readonly LabCEContext _context;
        public ActivoController(LabCEContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear_activo")]
        public async Task<IActionResult> CrearActivo(Activo activo)
        {
            await _context.Activos.AddAsync(activo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista_activos")]
        public async Task<ActionResult<IEnumerable<Activo>>> ListaActivos()
        {
            var activos = await _context.Activos.ToListAsync();
            return Ok(activos);
        }

        [HttpGet]
        [Route("lista_activos/{placa}")]
        public async Task<ActionResult<Activo>> ObtenerActivoPorPlaca(int placa)
        {
            var activo = await _context.Activos.FindAsync(placa);

            if (activo == null)
            {
                return NotFound(); // Devuelve 404 si el laboratorio no se encuentra
            }

            return Ok(activo);
        }

        [HttpPut]
        [Route("actualizar_activo")]
        public async Task<IActionResult> ActualizarActivo(int placa, Activo activo)
        {
            var ActivoExistente = await _context.Activos.FindAsync(placa);
            ActivoExistente!.Placa = activo.Placa;
            ActivoExistente!.Tipo = activo.Tipo;
            ActivoExistente!.Marca = activo.Marca;
            ActivoExistente.Fecha_Compra = activo.Fecha_Compra;
            ActivoExistente!.Req_Aprobador= activo.Req_Aprobador;
            ActivoExistente!.Id_Estado = activo.Id_Estado;
            ActivoExistente.Nombre_Lab = activo.Nombre_Lab;
            ActivoExistente.Ced_Prof = activo.Ced_Prof;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("eliminar_activo")]
        public async Task<IActionResult> EliminarActivo(int placa)
        {
            var activo_borrado = await _context.Activos.FindAsync(placa);
            _context.Activos.Remove(activo_borrado!);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
