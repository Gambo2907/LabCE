using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Profesor
    {
        [Key]public required int Cedula { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Correo { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Password { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Nombre { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Ap1 { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Ap2 { get; set; }

        public required DateOnly Nacimiento { get; set; }
        public required int Edad { get; set; }

        public virtual ICollection<Activo>? Activos { get; set; }
    }
}
