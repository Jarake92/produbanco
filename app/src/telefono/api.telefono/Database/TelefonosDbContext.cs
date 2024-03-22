using api.telefono.Models;
using Microsoft.EntityFrameworkCore;

namespace api.telefono.Database;

public class TelefonosDbContext : DbContext
{
    private readonly TelefonoDtoFake _faker = new();

    public DbSet<Telefono> Telefonos { get; set; } = null!;

    public TelefonosDbContext(DbContextOptions<TelefonosDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var telefonos = _faker.Generar(100);

        modelBuilder.Entity<Telefono>().HasData(telefonos);
    }
}