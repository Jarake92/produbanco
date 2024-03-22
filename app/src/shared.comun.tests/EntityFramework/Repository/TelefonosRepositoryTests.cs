using api.telefono.Database;
using api.telefono.Models;
using api.telefono.Services;
using Microsoft.EntityFrameworkCore;

namespace shared.comun.tests.EntityFramework.Repository;

public class TelefonosRepositoryTests
{
    [Fact]
    public async Task GetTelefonoById_ValidId_ReturnsTelefono()
    {
        var options = new DbContextOptionsBuilder<TelefonosDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var telefonoId = Guid.NewGuid();
        await using (var context = new TelefonosDbContext(options))
        {
            var expectedTelefono = new Telefono
            {
                Id = telefonoId,
                Numero = "123456789",
                Operadora = Operadora.Claro,
                Tipo = TipoTelefono.Celular
            };

            context.Telefonos.Add(expectedTelefono);
            await context.SaveChangesAsync();
        }

        await using (var context = new TelefonosDbContext(options))
        {
            var repository = new TelefonoRepository(context);

            // Act
            var result = await repository.GetTelefonoById(telefonoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("123456789", result?.Numero);
        }
    }

    [Fact]
    public async Task GetTelefonoById_InvalidId_ReturnsNull()
    {
        var options = new DbContextOptionsBuilder<TelefonosDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var telefonoId = Guid.NewGuid();
        await using (var context = new TelefonosDbContext(options))
        {
            var expectedTelefono = new Telefono
            {
                Id = telefonoId,
                Numero = "123456789",
                Operadora = Operadora.Claro,
                Tipo = TipoTelefono.Celular
            };

            context.Telefonos.Add(expectedTelefono);
            await context.SaveChangesAsync();
        }

        await using (var context = new TelefonosDbContext(options))
        {
            var repository = new TelefonoRepository(context);

            // Act
            var result = await repository.GetTelefonoById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
    
    [Fact]
    public async Task AddTelefono_ValidTelefono_ReturnsTelefono()
    {
        var options = new DbContextOptionsBuilder<TelefonosDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        await using var context = new TelefonosDbContext(options);
        var repository = new TelefonoRepository(context);

        // Act
        var result = await repository.AddTelefono(new Telefono
        {
            Numero = "123456789",
            Operadora = Operadora.Claro,
            Tipo = TipoTelefono.Celular
        });

        // Assert
        Assert.NotNull(result);
        Assert.Equal("123456789", result.Numero);
    }
    
    [Fact]
    public async Task UpdateTelefono_ValidTelefono_ReturnsTelefono()
    {
        var options = new DbContextOptionsBuilder<TelefonosDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var telefonoId = Guid.NewGuid();
        await using (var context = new TelefonosDbContext(options))
        {
            var expectedTelefono = new Telefono
            {
                Id = telefonoId,
                Numero =    "123456789",
                Operadora = Operadora.Claro,
                Tipo = TipoTelefono.Celular
            };

            context.Telefonos.Add(expectedTelefono);
            await context.SaveChangesAsync();
        }

        await using (var context = new TelefonosDbContext(options))
        {
            var repository = new TelefonoRepository(context);

            // Act
            var result = await repository.UpdateTelefono(new Telefono
            {
                Id = telefonoId,
                Numero = "Modified",
                Operadora = Operadora.Claro,
                Tipo = TipoTelefono.Celular
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Modified", result.Numero);
        }
    }
    
    [Fact]
    public async Task DeleteTelefono_ValidId_ReturnsTrue()
    {
        var options = new DbContextOptionsBuilder<TelefonosDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var telefonoId = Guid.NewGuid();
        await using (var context = new TelefonosDbContext(options))
        {
            var expectedTelefono = new Telefono
            {
                Id = telefonoId,
                Numero = "123456789",
                Operadora = Operadora.Claro,
                Tipo = TipoTelefono.Celular
            };

            context.Telefonos.Add(expectedTelefono);
            await context.SaveChangesAsync();
        }

        await using (var context = new TelefonosDbContext(options))
        {
            var repository = new TelefonoRepository(context);

            // Act
            var result = await repository.EliminarTelefono(telefonoId);

            // Assert
            Assert.True(result);
        }
    }
}