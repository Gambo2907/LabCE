using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Activo
    {
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Key]public required string Placa {  get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Tipo { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Marca { get; set; }
        public DateOnly? Fecha_Compra {  get; set; }
        public required Boolean Req_Aprobador { get; set; }

        public required int Id_Estado { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? Nombre_Lab { get; set; }
        public int? Ced_Prof {  get; set; }
        public Boolean Aprobado {  get; set; }
        
      

    }
}
