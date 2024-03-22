using shared.comun.hetoas.extensions;
using shared.comun.tests.Hetoas.Helpers;

namespace shared.comun.tests.Hetoas.ExtensionsTests;

public class SortHelperTests
{
    [Fact]
    public void ApplySort_OrdersEntitiesAscendingByGivenProperty()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new() { Id = Guid.NewGuid(), Name = "John" },
            new() { Id = Guid.NewGuid(), Name = "Alice" },
            new() { Id = Guid.NewGuid(), Name = "Bob" }
        };
        var sortHelper = new SortHelper<TestEntity>();

        // Act
        var result = sortHelper.ApplySort(entities, "Name");

        // Assert
        var testEntities = result as TestEntity[] ?? result.ToArray();
        Assert.Equal("Alice", testEntities[0].Name);
        Assert.Equal("Bob", testEntities[1].Name);
        Assert.Equal("John", testEntities[2].Name);
    }
    
    [Fact]
    public void ApplySort_OrdersEntitiesDescendingByGivenProperty()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new() { Id = Guid.NewGuid(), Name = "John" },
            new() { Id = Guid.NewGuid(), Name = "Alice" },
            new() { Id = Guid.NewGuid(), Name = "Bob" }
        };
        var sortHelper = new SortHelper<TestEntity>();

        // Act
        var result = sortHelper.ApplySort(entities, "Name desc");

        // Assert
        var testEntities = result as TestEntity[] ?? result.ToArray();
        Assert.Equal("John", testEntities[0].Name);
        Assert.Equal("Bob", testEntities[1].Name);
        Assert.Equal("Alice", testEntities[2].Name);
    }
}