using api.cliente.Database;
using api.cliente.Models;
using api.cliente.Services;
using Microsoft.EntityFrameworkCore;

namespace shared.comun.tests.EntityFramework.Repository;

public class ClientesRepositoryTests
{
    [Fact]
    public async Task GetClienteById_ValidId_ReturnsCliente()
    {
        var options = new DbContextOptionsBuilder<ClientesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var clienteId = Guid.NewGuid();
        await using (var context = new ClientesDbContext(options))
        {
            var expectedCliente = new Cliente
            {
                Id = clienteId,
                Name = "John",
                LastName = "Doe",
                DateBirth = DateTime.Now.AddYears(-18)
            };

            context.Clientes.Add(expectedCliente);
            await context.SaveChangesAsync();
        }

        await using (var context = new ClientesDbContext(options))
        {
            var repository = new ClientesRepository(context);

            // Act
            var result = await repository.GetClienteById(clienteId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result?.Name);
        }
    }

    [Fact]
    public async Task GetClienteById_InvalidId_ReturnsNull()
    {
        var options = new DbContextOptionsBuilder<ClientesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var clienteId = Guid.NewGuid();
        await using (var context = new ClientesDbContext(options))
        {
            var expectedCliente = new Cliente
            {
                Id = clienteId,
                Name = "John",
                LastName = "Doe",
                DateBirth = DateTime.Now.AddYears(-18)
            };

            context.Clientes.Add(expectedCliente);
            await context.SaveChangesAsync();
        }

        await using (var context = new ClientesDbContext(options))
        {
            var repository = new ClientesRepository(context);

            // Act
            var result = await repository.GetClienteById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
    
    [Fact]
    public async Task AddCliente_ValidCliente_ReturnsCliente()
    {
        var options = new DbContextOptionsBuilder<ClientesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        await using var context = new ClientesDbContext(options);
        var repository = new ClientesRepository(context);

        // Act
        var result = await repository.AddCliente(new Cliente
        {
            Name = "John",
            LastName = "Doe",
            DateBirth = DateTime.Now.AddYears(-18)
        });

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.Name);
    }
    
    [Fact]
    public async Task UpdateCliente_ValidCliente_ReturnsCliente()
    {
        var options = new DbContextOptionsBuilder<ClientesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var clienteId = Guid.NewGuid();
        await using (var context = new ClientesDbContext(options))
        {
            var expectedCliente = new Cliente
            {
                Id = clienteId,
                Name = "John",
                LastName = "Doe",
                DateBirth = DateTime.Now.AddYears(-18)
            };

            context.Clientes.Add(expectedCliente);
            await context.SaveChangesAsync();
        }

        await using (var context = new ClientesDbContext(options))
        {
            var repository = new ClientesRepository(context);

            // Act
            var result = await repository.UpdateCliente(new Cliente
            {
                Id = clienteId,
                Name = "Jane",
                LastName = "Doe",
                DateBirth = DateTime.Now.AddYears(-18)
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane", result.Name);
        }
    }
    
    [Fact]
    public async Task DeleteCliente_ValidId_ReturnsTrue()
    {
        var options = new DbContextOptionsBuilder<ClientesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var clienteId = Guid.NewGuid();
        await using (var context = new ClientesDbContext(options))
        {
            var expectedCliente = new Cliente
            {
                Id = clienteId,
                Name = "John",
                LastName = "Doe",
                DateBirth = DateTime.Now.AddYears(-18)
            };

            context.Clientes.Add(expectedCliente);
            await context.SaveChangesAsync();
        }

        await using (var context = new ClientesDbContext(options))
        {
            var repository = new ClientesRepository(context);

            // Act
            var result = await repository.DeleteCliente(clienteId);

            // Assert
            Assert.True(result);
        }
    }
}