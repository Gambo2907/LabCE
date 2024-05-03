using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Reporte
    {
        [Key] public required int ID { get; set; }
        public required DateOnly Fecha_Trabajo { get; set; }
        public required TimeOnly Hora_Inicio { get; set; }
        public required TimeOnly Hora_Final {  get; set; }
        public required int Horas_Totales {  get; set; }
        public required int Carnet_Op {  get; set; }
    }
}
