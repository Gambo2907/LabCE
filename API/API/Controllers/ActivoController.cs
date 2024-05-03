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
            var activos = await (from _A in _context.Activos
                                 join _Ea in _context.Estado_Activos on _A.Id_Estado equals
                                 _Ea.Id_Estado
                                 select new
                                 {
                                     _A.Placa,
                                     _A.Tipo,
                                     _A.Marca,
                                     _A.Fecha_Compra,
                                     _A.Req_Aprobador,
                                     _Ea.Estado,
                                     _A.Nombre_Lab,
                                     _A.Ced_Prof
                                 }
                                 ).ToListAsync();
            return Ok(activos);
        }

        [HttpGet]
        [Route("lista_activos/{placa}")]
        public async Task<ActionResult<Activo>> ObtenerActivoPorPlaca(string placa)
        {
            var activo = await (from _A in _context.Activos
                                join _Ea in _context.Estado_Activos on _A.Id_Estado equals
                                _Ea.Id_Estado
                                where _A.Placa == placa
                                select new
                                {
                                    _A.Placa,
                                    _A.Tipo,
                                    _A.Marca,
                                    _A.Fecha_Compra,
                                    _A.Req_Aprobador,
                                    _Ea.Estado,
                                    _A.Nombre_Lab,
                                    _A.Ced_Prof
                                }
                                 ).FirstAsync();

            if (activo == null)
            {
                return NotFound(); 
            }

            return Ok(activo);
        }

        [HttpGet]
        [Route("lista_activos_req_apr/{cedula}")]
        public async Task<ActionResult<IEnumerable<Activo>>> ObtenerActivosReqApr(int cedula)
        {
            var activos = await (from _A in _context.Activos

                                 where _A.Ced_Prof == cedula
                                 join _Ea in _context.Estado_Activos on _A.Id_Estado equals
                                _Ea.Id_Estado
                                 select new
                                 {
                                     _A.Placa,
                                     _A.Tipo,
                                     _A.Marca,
                                     _A.Fecha_Compra,
                                     _Ea.Estado,
                                 }
                                 ).ToListAsync(); ;

            if (activos == null)
            {
                return NotFound();
            }

            return Ok(activos);
        }

        [HttpGet]
        [Route("lista_activos_disponibles")]
        public async Task<ActionResult<IEnumerable<Activo>>> ObtenerActivosDisponibles()
        {
            var activos = await (from _A in _context.Activos
                                 where _A.Id_Estado == 1
                                 select new
                                 {
                                     _A.Placa,
                                     _A.Tipo,
                                     _A.Marca,
                                     _A.Fecha_Compra,
                                     _A.Req_Aprobador,
                                     _A.Nombre_Lab,
                                     _A.Ced_Prof
                                 }
                                 ).ToListAsync();

            if (activos == null)
            {
                return NotFound();
            }

            return Ok(activos);
        }

        [HttpGet]
        [Route("lista_activos_prestados")]
        public async Task<ActionResult<IEnumerable<Activo>>> ObtenerActivosPrestados()
        {
            var activos = await (from _A in _context.Activos
                                 where _A.Id_Estado == 2
                                 select new
                                 {
                                     _A.Placa,
                                     _A.Tipo,
                                     _A.Marca,
                                     _A.Fecha_Compra,
                                     _A.Req_Aprobador,
                                     _A.Nombre_Lab,
                                     _A.Ced_Prof
                                 }
                                 ).ToListAsync();

            if (activos == null)
            {
                return NotFound();
            }

            return Ok(activos);
        }

        [HttpPut]
        [Route("actualizar_activo")]
        public async Task<IActionResult> ActualizarActivo(string placa, Activo activo)
        {
            var ActivoExistente = await _context.Activos.FindAsync(placa);
            ActivoExistente!.Placa = activo.Placa;
            ActivoExistente!.Tipo = activo.Tipo;
            ActivoExistente!.Marca = activo.Marca;
            ActivoExistente.Fecha_Compra = activo.Fecha_Compra;
            ActivoExistente!.Req_Aprobador = activo.Req_Aprobador;
            ActivoExistente!.Id_Estado = activo.Id_Estado;
            ActivoExistente.Nombre_Lab = activo.Nombre_Lab;
            ActivoExistente.Ced_Prof = activo.Ced_Prof;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("devolucion_activo")]
        public async Task<IActionResult> DevolucionActivo(string placa)
        {
            var ActivoExistente = await _context.Activos.FindAsync(placa);
            ActivoExistente!.Id_Estado = 1;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("averia_activo")]
        public async Task<IActionResult> AveriaActivo(string placa)
        {
            var ActivoExistente = await _context.Activos.FindAsync(placa);
            ActivoExistente!.Id_Estado = 3;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar_activo")]
        public async Task<IActionResult> EliminarActivo(string placa)
        {
            var activo_borrado = await _context.Activos.FindAsync(placa);
            _context.Activos.Remove(activo_borrado!);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
