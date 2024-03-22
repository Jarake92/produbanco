using api.cliente.Models;
using api.cliente.Services;
using Moq;
using shared.comun.hetoas.extensions;
using shared.comun.tests.Hetoas.Helpers;

namespace shared.comun.tests.Hetoas.ApiHelpers;

public class ClienteHelperTests
{
    [Fact]
    public void GetFilteredEntities_ReturnsFilteredEntities()
    {
        // Arrange
        var entities = new List<Cliente>
        {
            new() { Id = Guid.NewGuid(), Name = "John", LastName = "Doe" },
            new() { Id = Guid.NewGuid(), Name = "Alice", LastName = "Smith" },
            new() { Id = Guid.NewGuid(), Name = "Bob", LastName = "Johnson" }
        }.AsQueryable();
        var entityParameters = new TestQueryStringParameters { Filter = "LastName==\"Smith\"" };

        var mockSortHelper = new Mock<ISortHelper<Cliente>>();
        var mockDataShaper = new Mock<IDataShaper<Cliente>>();
        var helper = new ClienteHelper(mockSortHelper.Object, mockDataShaper.Object);

        // Act
        var result = helper.GetFilteredEntities(entities, entityParameters);

        // Assert
        var collection = result as Cliente[] ?? result.ToArray();
        Assert.Single(collection);
        Assert.Equal("Alice", collection.ElementAt(0).Name);
        Assert.Equal("Smith", collection.ElementAt(0).LastName);
    }
    
    [Fact]
    public void GetFilteredEntities_ReturnsAllEntities()
    {
        // Arrange
        var entities = new List<Cliente>
        {
            new() { Id = Guid.NewGuid(), Name = "John", LastName = "Doe" },
            new() { Id = Guid.NewGuid(), Name = "Alice", LastName = "Smith" },
            new() { Id = Guid.NewGuid(), Name = "Bob", LastName = "Johnson" }
        }.AsQueryable();
        var entityParameters = new TestQueryStringParameters { Filter = "" };

        var mockSortHelper = new Mock<ISortHelper<Cliente>>();
        var mockDataShaper = new Mock<IDataShaper<Cliente>>();
        var helper = new ClienteHelper(mockSortHelper.Object, mockDataShaper.Object);

        // Act
        var result = helper.GetFilteredEntities(entities, entityParameters);

        // Assert
        Assert.Equal(3, result.Count());
    }
}