using api.direccion.Models;
using api.direccion.Services;
using Moq;
using shared.comun.hetoas.extensions;
using shared.comun.tests.Hetoas.Helpers;

namespace shared.comun.tests.Hetoas.ApiHelpers;

public class DireccionHelperTests
{
    [Fact]
    public void GetFilteredEntities_ReturnsFilteredEntities()
    {
        // Arrange
        var entities = new List<Direccion>
        {
            new() { Id = Guid.NewGuid(), CallePrincipal = "Main St", Canton = "New York", Provincia = "New York" },
            new() { Id = Guid.NewGuid(), CallePrincipal = "Broadway", Canton = "New York", Provincia = "New York" },
            new() { Id = Guid.NewGuid(), CallePrincipal = "Main St", Canton = "Los Angeles", Provincia = "California" }
        }.AsQueryable();
        var entityParameters = new TestQueryStringParameters { Filter = "CallePrincipal == \"Main St\"" };

        var mockSortHelper = new Mock<ISortHelper<Direccion>>();
        var mockDataShaper = new Mock<IDataShaper<Direccion>>();
        var helper = new DireccionHelper(mockDataShaper.Object, mockSortHelper.Object);

        // Act
        var result = helper.GetFilteredEntities(entities, entityParameters);

        // Assert
        var direccions = result as Direccion[] ?? result.ToArray();
        Assert.Equal(2, direccions.Count());
        Assert.Equal("Main St", direccions.ElementAt(0).CallePrincipal);
        Assert.Equal("Main St", direccions.ElementAt(1).CallePrincipal);
    }

    [Fact]
    public void GetFilteredEntities_ReturnsAllEntities()
    {
        // Arrange
        var entities = new List<Direccion>
        {
            new() { Id = Guid.NewGuid(), CallePrincipal = "Main St", Canton = "New York", Provincia = "New York" },
            new() { Id = Guid.NewGuid(), CallePrincipal = "Broadway", Canton = "New York", Provincia = "New York" },
            new() { Id = Guid.NewGuid(), CallePrincipal = "Main St", Canton = "Los Angeles", Provincia = "California" }
        }.AsQueryable();
        var entityParameters = new TestQueryStringParameters { Filter = "" };

        var mockSortHelper = new Mock<ISortHelper<Direccion>>();
        var mockDataShaper = new Mock<IDataShaper<Direccion>>();
        var helper = new DireccionHelper(mockDataShaper.Object, mockSortHelper.Object);

        // Act
        var result = helper.GetFilteredEntities(entities, entityParameters);

        // Assert
        Assert.Equal(3, result.Count());
    }
    
    [Fact]
    public void GetFilteredEntities_ReturnsFilteredEntitiesByProvincia()
    {
        // Arrange
        var entities = new List<Direccion>
        {
            new() { Id = Guid.NewGuid(), CallePrincipal = "Main St", Canton = "New York", Provincia = "New York" },
            new() { Id = Guid.NewGuid(), CallePrincipal = "Broadway", Canton = "New York", Provincia = "New York" },
            new() { Id = Guid.NewGuid(), CallePrincipal = "Main St", Canton = "Los Angeles", Provincia = "California" }
        }.AsQueryable();
        var entityParameters = new TestQueryStringParameters { Filter = "Canton like New York" };

        var mockSortHelper = new Mock<ISortHelper<Direccion>>();
        var mockDataShaper = new Mock<IDataShaper<Direccion>>();
        var helper = new DireccionHelper(mockDataShaper.Object, mockSortHelper.Object);

        // Act
        var result = helper.GetFilteredEntities(entities, entityParameters);

        // Assert
        var direccions = result as Direccion[] ?? result.ToArray();
        Assert.Equal(2, direccions.Count());
        Assert.Equal("New York", direccions.ElementAt(0).Provincia);
        Assert.Equal("New York", direccions.ElementAt(1).Provincia);
    }
}