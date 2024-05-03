using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class EstadoActivo
    {
        [Key] public required int Id_Estado {  get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Estado {  get; set; }
    }
}
