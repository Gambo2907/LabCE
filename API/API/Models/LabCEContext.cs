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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Laboratorio>().HasIndex(c => c.Nombre).IsUnique();

            modelBuilder.Entity<Administrador>().HasIndex(c => c.Correo).IsUnique();

            modelBuilder.Entity<Operador>().HasIndex(c => c.Carnet).IsUnique();
            modelBuilder.Entity<Operador>().HasIndex(c => c.Cedula).IsUnique();
            modelBuilder.Entity<Operador>().HasIndex(c => c.Correo).IsUnique();

            modelBuilder.Entity<Activo>().HasIndex(c => c.Placa).IsUnique();
            modelBuilder.Entity<Activo>().HasOne(a => a.Profesor).WithMany(p => p.Activos).HasForeignKey(a => a.Ced_Prof);
            modelBuilder.Entity<Activo>().HasOne(a => a.Laboratorio).WithMany(p => p.Activos).HasForeignKey(a => a.Nombre_Lab);

            modelBuilder.Entity<Profesor>().HasIndex(c => c.Cedula).IsUnique();
            modelBuilder.Entity<Profesor>().HasIndex(c => c.Correo).IsUnique();

           


        }
    }
}
