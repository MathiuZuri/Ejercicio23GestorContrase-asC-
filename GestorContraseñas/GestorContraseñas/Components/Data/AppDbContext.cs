using GestorContraseñas.Components.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorContraseñas.Components.data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Representa la tabla de credenciales
    public DbSet<Credencial> Credenciales => Set<Credencial>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // SonarQube recomienda definir restricciones explícitas
        modelBuilder.Entity<Credencial>().HasKey(c => c.Id);
        modelBuilder.Entity<Credencial>().Property(c => c.Servicio).IsRequired().HasMaxLength(100);
        base.OnModelCreating(modelBuilder);
    }
}