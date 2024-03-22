using api.cliente.Models;
using Microsoft.EntityFrameworkCore;

namespace api.cliente.Database;

public class ClientesDbContext : DbContext
{
    private readonly ClienteDtoFaker _faker = new();

    public DbSet<Cliente> Clientes { get; set; } = null!;

    public ClientesDbContext(DbContextOptions<ClientesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var clientes = _faker.Generar(100);

        modelBuilder.Entity<Cliente>().HasData(clientes);
    }
}