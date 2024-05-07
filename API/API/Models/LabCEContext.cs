using Microsoft.EntityFrameworkCore;

/*
 * Contexto de la base de datos, aquí con Entity Framework se pueden realizar las consultas, añadir datos a las tablas,
 * eliminar datos de las tablas, etc, con la ayuda de los modelos y los controladores, esto modela las tablas que se
 * encuentran en la base de datos existente.
 * */

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
