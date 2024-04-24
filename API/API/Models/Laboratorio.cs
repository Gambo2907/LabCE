using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Laboratorio
    {
        [MaxLength(50, ErrorMessage ="El campo {0} debe tener maximo {1} caracteres.")]
        [Key]
        public required string Nombre {get; set;}
        public required TimeOnly Hora_Inicio { get; set;}
        public required TimeOnly Hora_Final { get; set;}
        public required int Capacidad { get; set;}
        public required int Computadores { get; set;}
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? Facilidades {get; set;}


    }
}
