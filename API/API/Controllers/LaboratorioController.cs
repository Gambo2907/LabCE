using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {

        private readonly LabCEContext _context;

        public LaboratorioController(LabCEContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crearlab")]
        public async Task<IActionResult>CrearLaboratorio(Laboratorio lab)
        {
            await _context.Laboratorios.AddAsync(lab);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet]
        [Route("lista_labs")]
        public async Task<ActionResult<IEnumerable<Laboratorio>>> ListaLabs()
        {
            var labs = await _context.Laboratorios.ToListAsync();
            return Ok(labs);
        }

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
