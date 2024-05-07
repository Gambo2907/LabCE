using API.Encriptacion;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
/*
 *AutenticacionController: encargada de realizar todas las consultas necesarias para la autenticacion
 *de operadores y profesores en la web app
 */
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AutenticacionController : ControllerBase
    {
        //Constructor de la encriptacion de datos MD5
        EncryptMD5 encrypt = new EncryptMD5();

        //Obtiene el contexto para así poder mostrar y añadir datos a la DB
        private readonly LabCEContext _context;

        /*
         *Constructor de la clase con un contexto de base de datos 
         */
        public AutenticacionController(LabCEContext context)
        {
            _context = context;
        }

        /*
         *AutenticacionOperador: se encarga de autenticar el operador por medio de su password,
         *el operador digita su password, la encripta, y la busca en el atributo password de la tabla
         *Operadores en la db para así comprobar si existe o no el operador.
         */
        [HttpPost]
        [Route("autenticacion_operador")]
        public async Task<IActionResult> AutenticacionOperador(AutenticacionModel modelo)
        {

            Operador? operador = await _context.Operadores.Where(o => o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if (operador == null)
            {
                return NotFound();
            }
            return Ok(operador);
        }

        /*
         *AutenticacionProfesor: se encarga de autenticar al profesor por medio de su password,
         *el profesor digita su password, la encripta, y la busca en el atributo password de la tabla
         * Profesores en la db para así comprobar si existe o no el operador.
         */
        [HttpPost]
        [Route("autenticacion_profesor")]
        public async Task<IActionResult> AutenticacionProfesor(AutenticacionModel modelo)
        {

            Profesor? profesor = await _context.Profesores.Where(o => o.Password == encrypt.Encrypt(modelo.Password)).FirstOrDefaultAsync();
            if (profesor == null)
            {
                return NotFound();
            }
            return Ok(profesor);
        }
    }
}
