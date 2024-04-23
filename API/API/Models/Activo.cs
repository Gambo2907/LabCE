using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Activo
    {
        [Key]public required int Placa {  get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Tipo { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Marca { get; set; }
        public DateOnly? Fecha_Compra {  get; set; }
        [MaxLength(5, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Req_Aprobador { get; set; }

        [MaxLength(25, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Estado { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? Nombre_Lab { get; set; }
        public virtual Laboratorio? Laboratorio { get; set; }
        public int? Ced_Prof {  get; set; }
        public virtual Profesor? Profesor { get; set; }
        
      

    }
}
