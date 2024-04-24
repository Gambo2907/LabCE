using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Activo
    {
        [Key]public required string Placa {  get; set; }
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Tipo { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Marca { get; set; }
        public DateOnly? Fecha_Compra {  get; set; }
        [MaxLength(5, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Req_Aprobador { get; set; }

        public required int Id_Estado { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? Nombre_Lab { get; set; }
        public int? Ced_Prof {  get; set; }
        
      

    }
}
