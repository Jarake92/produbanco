using api.direccion.Database;
using api.direccion.Models;
using api.direccion.Services;
using Microsoft.EntityFrameworkCore;

namespace shared.comun.tests.EntityFramework.Repository;

public class DireccionesRepositoryTests
{
    [Fact]
    public async Task GetDireccionById_ValidId_ReturnsDireccion()
    {
        var options = new DbContextOptionsBuilder<DireccionesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var direccionId = Guid.NewGuid();
        await using (var context = new DireccionesDbContext(options))
        {
            var expectedDireccion = new Direccion
            {
                Id = direccionId,
                Provincia = "Lorem",
                Canton = "Ipsum",
                CallePrincipal = "Dolor",
            };

            context.Direcciones.Add(expectedDireccion);
            await context.SaveChangesAsync();
        }

        await using (var context = new DireccionesDbContext(options))
        {
            var repository = new DireccionRepository(context);

            // Act
            var result = await repository.GetDireccionById(direccionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Lorem", result?.Provincia);
        }
    }

    [Fact]
    public async Task GetDireccionById_InvalidId_ReturnsNull()
    {
        var options = new DbContextOptionsBuilder<DireccionesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var direccionId = Guid.NewGuid();
        await using (var context = new DireccionesDbContext(options))
        {
            var expectedDireccion = new Direccion
            {
                Id = direccionId,
                Provincia = "Lorem",
                Canton = "Ipsum",
                CallePrincipal = "Dolor",
            };

            context.Direcciones.Add(expectedDireccion);
            await context.SaveChangesAsync();
        }

        await using (var context = new DireccionesDbContext(options))
        {
            var repository = new DireccionRepository(context);

            // Act
            var result = await repository.GetDireccionById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
    
    [Fact]
    public async Task AddDireccion_ValidDireccion_ReturnsDireccion()
    {
        var options = new DbContextOptionsBuilder<DireccionesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        await using var context = new DireccionesDbContext(options);
        var repository = new DireccionRepository(context);

        // Act
        var result = await repository.AddDireccion(new Direccion
        {
            Provincia = "Lorem",
            Canton = "Ipsum",
            CallePrincipal = "Dolor",
        });

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Lorem", result.Provincia);
    }
    
    [Fact]
    public async Task UpdateDireccion_ValidDireccion_ReturnsDireccion()
    {
        var options = new DbContextOptionsBuilder<DireccionesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var direccionId = Guid.NewGuid();
        await using (var context = new DireccionesDbContext(options))
        {
            var expectedDireccion = new Direccion
            {
                Id = direccionId,
                Provincia = "Lorem",
                Canton = "Ipsum",
                CallePrincipal = "Dolor",
            };

            context.Direcciones.Add(expectedDireccion);
            await context.SaveChangesAsync();
        }

        await using (var context = new DireccionesDbContext(options))
        {
            var repository = new DireccionRepository(context);

            // Act
            var result = await repository.UpdateDireccion(new Direccion
            {
                Id = direccionId,
                Provincia = "Modified",
                Canton = "Ipsum",
                CallePrincipal = "Dolor",
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Modified", result.Provincia);
        }
    }
    
    [Fact]
    public async Task DeleteDireccion_ValidId_ReturnsTrue()
    {
        var options = new DbContextOptionsBuilder<DireccionesDbContext>()
            .UseInMemoryDatabase("Test_DataBase")
            .Options;

        var direccionId = Guid.NewGuid();
        await using (var context = new DireccionesDbContext(options))
        {
            var expectedDireccion = new Direccion
            {
                Id = direccionId,
                Provincia = "Lorem",
                Canton = "Ipsum",
                CallePrincipal = "Dolor",
            };

            context.Direcciones.Add(expectedDireccion);
            await context.SaveChangesAsync();
        }

        await using (var context = new DireccionesDbContext(options))
        {
            var repository = new DireccionRepository(context);

            // Act
            var result = await repository.DeleteDireccion(direccionId);

            // Assert
            Assert.True(result);
        }
    }
}