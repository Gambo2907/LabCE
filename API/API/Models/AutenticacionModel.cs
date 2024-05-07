/*
 * Modelo de la autenticación, con esto el usuario va a ser capaz de ingresar su password para autenticarse
 * cuando lo requiera.
 * */

namespace API.Models
{
    public class AutenticacionModel
    {
        public string? Password {  get; set; }
    }
}
