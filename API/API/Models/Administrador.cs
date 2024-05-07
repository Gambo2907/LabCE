using System.ComponentModel.DataAnnotations;

/*
 * Modelo de la Tabla administrador, donde se modelan sus atributos y de que tipo son
 * */

namespace API.Models
{
    public class Administrador
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Correo { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Password { get; set; }
    }
}
