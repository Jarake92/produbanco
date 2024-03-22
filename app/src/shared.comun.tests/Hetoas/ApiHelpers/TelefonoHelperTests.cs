using api.telefono.Models;
using api.telefono.Services;
using Moq;
using shared.comun.hetoas.extensions;
using shared.comun.tests.Hetoas.Helpers;

namespace shared.comun.tests.Hetoas.ApiHelpers;

public class TelefonoHelperTests
{
    [Fact]
    public void GetFilteredEntities_ReturnsFilteredEntities()
    {
        // Arrange
        var entities = new List<Telefono>
        {
            new()
            {
                Id = Guid.NewGuid(), Numero = "123456789", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            },
            new()
            {
                Id = Guid.NewGuid(), Numero = "987654321", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            },
            new()
            {
                Id = Guid.NewGuid(), Numero = "123456789", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            }
        }.AsQueryable();
        var entityParameters = new TestQueryStringParameters { Filter = "Numero == \"123456789\"" };

        var mockSortHelper = new Mock<ISortHelper<Telefono>>();
        var mockDataShaper = new Mock<IDataShaper<Telefono>>();
        var helper = new TelefonoHelper(mockSortHelper.Object, mockDataShaper.Object);

        // Act
        var result = helper.GetFilteredEntities(entities, entityParameters);

        // Assert
        var Telefonos = result as Telefono[] ?? result.ToArray();
        Assert.Equal(2, Telefonos.Count());
        Assert.Equal("123456789", Telefonos.ElementAt(0).Numero);
        Assert.Equal("123456789", Telefonos.ElementAt(1).Numero);
    }

    [Fact]
    public void GetFilteredEntities_ReturnsAllEntities()
    {
        // Arrange
        var entities = new List<Telefono>
        {
            new()
            {
                Id = Guid.NewGuid(), Numero = "123456789", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            },
            new()
            {
                Id = Guid.NewGuid(), Numero = "987654321", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            },
            new()
            {
                Id = Guid.NewGuid(), Numero = "123456789", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            }
        }.AsQueryable();
        var entityParameters = new TestQueryStringParameters { Filter = "" };
    
        var mockSortHelper = new Mock<ISortHelper<Telefono>>();
        var mockDataShaper = new Mock<IDataShaper<Telefono>>();
        var helper = new TelefonoHelper(mockSortHelper.Object, mockDataShaper.Object);
    
        // Act
        var result = helper.GetFilteredEntities(entities, entityParameters);
    
        // Assert
        Assert.Equal(3, result.Count());
    }
    
    [Fact]
    public void GetFilteredEntities_ReturnsFilteredEntitiesByProvincia()
    {
        // Arrange
        var entities = new List<Telefono>
        {
            new()
            {
                Id = Guid.NewGuid(), Numero = "123456789", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            },
            new()
            {
                Id = Guid.NewGuid(), Numero = "987654321", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            },
            new()
            {
                Id = Guid.NewGuid(), Numero = "123456789", Operadora = Operadora.Claro, Tipo = TipoTelefono.Celular
            }
        }.AsQueryable();
        var entityParameters = new TestQueryStringParameters { Filter = "Numero like 987654321" };
    
        var mockSortHelper = new Mock<ISortHelper<Telefono>>();
        var mockDataShaper = new Mock<IDataShaper<Telefono>>();
        var helper = new TelefonoHelper(mockSortHelper.Object, mockDataShaper.Object);
    
        // Act
        var result = helper.GetFilteredEntities(entities, entityParameters);
    
        // Assert
        var telefonos = result as Telefono[] ?? result.ToArray();
        Assert.Single(telefonos);
        Assert.Equal("987654321", telefonos.ElementAt(0).Numero);
    }
}