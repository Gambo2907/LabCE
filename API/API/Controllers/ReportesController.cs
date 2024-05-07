using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*
 *ReportesController: Se encarga de generar los reportes y guardarlos en la db 
 */
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;
        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public ReportesController(LabCEContext context)
        {
            _context = context;
        }
        /*
         *RegistrarReporte: Se encarga de registrar el reporte correspondiente al usuario 
         */
        [HttpPost]
        [Route("registrar_reporte")]
        public async Task<IActionResult> RegistrarReporte(Reporte modelo)
        {
            Reporte reporte = new Reporte()
            {
                ID = 0,
                Fecha_Trabajo = DateOnly.Parse(DateTime.Today.ToString("yyyy-MM-dd")),
                Hora_Inicio = modelo.Hora_Inicio,
                Hora_Final = modelo.Hora_Final,
                Horas_Totales = modelo.Hora_Final.Hour - modelo.Hora_Inicio.Hour,
                Carnet_Op = modelo.Carnet_Op,
            };
            await _context.Reportes.AddAsync(reporte);
            await _context.SaveChangesAsync();

            return Ok();
        }
        /*
         *ListaReportes: Obtiene todos los reportes que han hecho todos los usuarios. 
         */
        [HttpGet]
        [Route("lista_reportes")]
        public async Task<ActionResult<IEnumerable<Reporte>>> ListaReportes()
        {
            var reportes = await(from _Reportes in _context.Reportes
                                 join _Operadores in _context.Operadores on _Reportes.Carnet_Op equals
                                 _Operadores.Carnet
                                 select new
                                 {
                                     _Operadores.Ap1,
                                     _Operadores.Ap2,
                                     _Operadores.Nombre,
                                     _Reportes.Hora_Inicio,
                                     _Reportes.Hora_Final,
                                     _Reportes.Horas_Totales
                                 }).ToListAsync();
            if(reportes == null)
            {
                return NotFound();
            }
            return Ok(reportes);
        }
        /*
         *ListaReportes: Obtiene todos los reportes que ha hecho el usuario con el carnet digitado. 
         */
        [HttpGet]
        [Route("lista_reportes/{carnet}")]
        public async Task<ActionResult<IEnumerable<Reporte>>> ListaReportesOperador(int carnet)
        {
            var reportes = await (from _Reportes in _context.Reportes
                                  join _Operadores in _context.Operadores on _Reportes.Carnet_Op equals
                                  _Operadores.Carnet
                                  where _Reportes.Carnet_Op == carnet
                                  select new
                                  {
                                      _Operadores.Ap1,
                                      _Operadores.Ap2,
                                      _Operadores.Nombre,
                                      _Reportes.Hora_Inicio,
                                      _Reportes.Hora_Final,
                                      _Reportes.Horas_Totales
                                  }).ToListAsync();
            if (reportes == null)
            {
                return NotFound();
            }
            return Ok(reportes);
        }
    }
}
