using api.direccion.Models;
using Microsoft.EntityFrameworkCore;

namespace api.direccion.Database;

public class DireccionesDbContext : DbContext
{
    private readonly DireccionDtoFaker _faker = new();

    public DbSet<Direccion> Direcciones { get; set; } = null!;

    public DireccionesDbContext(DbContextOptions<DireccionesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var direcciones = _faker.Generar(100);

        modelBuilder.Entity<Direccion>().HasData(direcciones);
    }
}