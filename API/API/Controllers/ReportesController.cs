using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly LabCEContext _context;
        public ReportesController(LabCEContext context)
        {
            _context = context;
        }
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
