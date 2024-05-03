using Microsoft.EntityFrameworkCore;
namespace API.Models
{
    public class LabCEContext : DbContext
    {
        public LabCEContext(DbContextOptions<LabCEContext> options) : base(options)
        {
        }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Activo> Activos { get; set; }
        public DbSet<Profesor> Profesores {  get; set; }
        public DbSet<EstadoActivo> Estado_Activos {  get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<Reporte> Reportes { get; set; }

       
    }
}
