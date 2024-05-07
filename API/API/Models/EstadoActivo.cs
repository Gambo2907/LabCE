using System.ComponentModel.DataAnnotations;

/*
 * Modelo de la Tabla EstadoActivo, donde se modelan sus atributos y de que tipo son
 * */

namespace API.Models
{
    public class EstadoActivo
    {
        [Key] public required int Id_Estado {  get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Estado {  get; set; }
    }
}
