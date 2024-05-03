using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Prestamo
    {
        [Key] public required int ID { get; set; }
        public required DateOnly Fecha { get; set; }
        public required TimeOnly Hora { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string PlacaActivo { get; set; }
        public int? CedProf {  get; set; }
        public int? CarnetOP {  get; set; }
        public string? NombreEstudiante { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? AP1Estudiante { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? AP2Estudiante { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? CorreoEstudiante { get; set; }
    }
}
