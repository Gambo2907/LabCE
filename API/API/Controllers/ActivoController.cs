using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*Controlador del modelo Activo el cual se encarga de generar todas las consultas, además de añadir o
 * eliminar datos de la tabla Activos que se encuentra en SQL Server
*/

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivoController : ControllerBase
    {
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;
        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public ActivoController(LabCEContext context)
        {
            _context = context;
        }

        /*
         * CrearActivo: se encarga de crear un nuevo activo para añadirlo a la tabla activos en la DB existente.
         * 
         */

        [HttpPost]
        [Route("crear_activo")]
        public async Task<IActionResult> CrearActivo(Activo modelo)
        {
            Activo activo = new Activo() {
                Placa = modelo.Placa,
                Tipo = modelo.Tipo,
                Marca = modelo.Placa,
                Fecha_Compra = modelo.Fecha_Compra,
                Req_Aprobador = modelo.Req_Aprobador,
                Id_Estado = modelo.Id_Estado,
                Nombre_Lab = modelo.Nombre_Lab,
                Ced_Prof = modelo.Ced_Prof,
                Aprobado = false
            };
            await _context.Activos.AddAsync(activo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /*
         * ListaActivos: este task devuelve todas las tuplas de la tabla activos
         * 
         */

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

        /*
        * ObtenerActivoPorPlaca: este task devuelve el activo correspondiente a la placa consultada.
        * 
        */

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

        /*
        * ObtenerActivosReqApr: este task devuelve todas las tuplas de la tabla activos que requieren aprobacion 
        * del profesor con la cédula consultada.
        * 
        */

        [HttpGet]
        [Route("lista_activos_req_apr/{cedula}")]
        public async Task<ActionResult<IEnumerable<Activo>>> ObtenerActivosReqApr(int cedula)
        {
            var activos = await (from _A in _context.Activos 
                                 join _P in _context.Prestamos on _A.Placa equals
                                _P.PlacaActivo
                                 where _A.Ced_Prof == cedula && _A.Aprobado == false && _A.Id_Estado == 4
                                 select new
                                 {   
                                     _P.Fecha,
                                     _A.Placa,
                                     _A.Tipo,
                                     _A.Marca,
                                 }
                                 ).ToListAsync(); 

            if (activos == null)
            {
                return NotFound();
            }

            return Ok(activos);
        }

        /*
        * ObtenerActivosDisponibles: este task devuelve todas las tuplas de la tabla activos que estan disponibles
        * para prestamos
        * 
        */

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

        /*
        * ObtenerActivosPrestados: este task devuelve todas las tuplas de la tabla activos que estan prestados
        * 
        */

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
        /*
        * ActualizarActivo: este task busca la tupla con la placa consultada y edita los valores que el usuario elija.
        * 
        */

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

        /*
        * DevolucionActivo: este task se encarga de cambiar el estado del activo de prestado a disponible para prestamos
        * 
        */
        [HttpPut]
        [Route("devolucion_activo")]
        public async Task<IActionResult> DevolucionActivo(string placa)
        {
            var ActivoExistente = await _context.Activos.FindAsync(placa);
            ActivoExistente!.Id_Estado = 1;
            ActivoExistente!.Aprobado = false;

            await _context.SaveChangesAsync();
            return Ok();
        }

        /*
        * AveriaActivo: este task se encarga de cambiar el estado del activo de prestado a averiado
        * 
        */
        [HttpPut]
        [Route("averia_activo")]
        public async Task<IActionResult> AveriaActivo(string placa)
        {
            var ActivoExistente = await _context.Activos.FindAsync(placa);
            ActivoExistente!.Id_Estado = 3;
            ActivoExistente!.Aprobado = false;

            await _context.SaveChangesAsync();
            return Ok();
        }
        /*
        * AprobarPrestamoActivo: este task se encarga de aprobar el prestamo de algun activo.
        * 
        */
        [HttpPut]
        [Route("aprobar_prestamo_activo")]
        public async Task<IActionResult> AprobarPrestamoActivo(string placa)
        {
            var ActivoExistente = await _context.Activos.FindAsync(placa);
            ActivoExistente!.Aprobado = true;
            ActivoExistente!.Id_Estado = 2;
            await _context.SaveChangesAsync();
            return Ok();
        }
        /*
        * EliminarActivo: este task se encarga de eliminar de la db la tupla con la placa consultada.
        * 
        */

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
