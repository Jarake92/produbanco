using shared.comun.hetoas;
using shared.comun.hetoas.extensions;
using shared.comun.tests.Hetoas.Helpers;

namespace shared.comun.tests.Hetoas.ExtensionsTests;

public class DataShaperTests
{
    [Fact]
    public void ShapeData_ReturnsOnlyRequestedProperties()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new() { Id = Guid.NewGuid(), Name = "John", Age = 30 },
            new() { Id = Guid.NewGuid(), Name = "Alice", Age = 25 },
            new() { Id = Guid.NewGuid(), Name = "Bob", Age = 40 }
        };
        var shaper = new DataShaper<TestEntity>();

        // Act
        var result = shaper.ShapeData(entities, "Name,Age");

        // Assert
        var shapedEntities = result as ShapedEntity[] ?? result.ToArray();
        Assert.Equal(3, shapedEntities.Length);
        Assert.Equal("John", shapedEntities.ElementAt(0).Entity.Values.ElementAt(0));
        Assert.Equal(30, shapedEntities.ElementAt(0).Entity.Values.ElementAt(1));
        Assert.Equal("Alice", shapedEntities.ElementAt(1).Entity.Values.ElementAt(0));
        Assert.Equal(25, shapedEntities.ElementAt(1).Entity.Values.ElementAt(1));
        Assert.Equal("Bob", shapedEntities.ElementAt(2).Entity.Values.ElementAt(0));
        Assert.Equal(40, shapedEntities.ElementAt(2).Entity.Values.ElementAt(1));
    }
}