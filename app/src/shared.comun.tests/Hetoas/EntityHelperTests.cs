using Moq;
using shared.comun.hetoas.extensions;
using shared.comun.tests.Hetoas.Helpers;

namespace shared.comun.tests.Hetoas;

public class EntityHelperTests
{
    private readonly Mock<ISortHelper<TestEntity>> _testSortHelperMock = new();
    private readonly Mock<IDataShaper<TestEntity>> _testDataShaperMock = new();

    private readonly List<TestEntity> _testEntities = new()
    {
        new TestEntity { Id = Guid.NewGuid(), Name = "John", Age = 30 },
        new TestEntity { Id = Guid.NewGuid(), Name = "Alice", Age = 25 },
        new TestEntity { Id = Guid.NewGuid(), Name = "Bob", Age = 40 }
    };

    [Fact]
    public void GetFilteredEntities_ReturnsAllEntitiesWhenFilterIsNull()
    {
        // Arrange
        var entityParameters = new TestQueryStringParameters();

        _testSortHelperMock.Setup(x => x.ApplySort(It.IsAny<IQueryable<TestEntity>>(), It.IsAny<string>()))
            .Returns(_testEntities);

        var entityHelper = new TestEntityHelper(_testSortHelperMock.Object, _testDataShaperMock.Object);

        // Act
        var result = entityHelper.GetFilteredEntities(_testEntities.AsQueryable(), entityParameters);

        // Assert
        var testEntities = result as TestEntity[] ?? result.ToArray();
        Assert.Equal(3, testEntities.Length);
        Assert.Equal("John", testEntities.ElementAt(0).Name);
        Assert.Equal("Alice", testEntities.ElementAt(1).Name);
        Assert.Equal("Bob", testEntities.ElementAt(2).Name);
    }

    [Fact]
    public void GetFilteredEntities_ReturnsFilteredEntities()
    {
        // Arrange
        var entityParameters = new TestQueryStringParameters { Filter = "Name == \"Alice\"" };

        _testSortHelperMock.Setup(x => x.ApplySort(It.IsAny<IQueryable<TestEntity>>(), It.IsAny<string>()))
            .Returns(_testEntities);

        var entityHelper = new TestEntityHelper(_testSortHelperMock.Object, _testDataShaperMock.Object);

        // Act
        var result = entityHelper.GetFilteredEntities(_testEntities.AsQueryable(), entityParameters);

        // Assert
        var testEntities = result as TestEntity[] ?? result.ToArray();
        Assert.Single(testEntities);
        Assert.Equal("Alice", testEntities.ElementAt(0).Name);
    }
    
    [Fact]
    public void GetShapedEntities_ReturnsShapedEntities()
    {
        // Arrange
        var entityParameters = new TestQueryStringParameters { Fields = "Name,Age" };

        _testSortHelperMock.Setup(x => x.ApplySort(It.IsAny<IQueryable<TestEntity>>(), It.IsAny<string>()))
            .Returns(_testEntities);
        
        var entityHelper = new TestEntityHelper(_testSortHelperMock.Object, new DataShaper<TestEntity>());

        // Act
        var result = entityHelper.GetShapedEntities(_testEntities.AsQueryable(), entityParameters);

        // Assert
        var shapedEntities = result.ToArray();
        Assert.Equal(3, shapedEntities.Length);
        Assert.Equal("John", shapedEntities.ElementAt(0).Entity.Values.ElementAt(0));
        Assert.Equal(30, shapedEntities.ElementAt(0).Entity.Values.ElementAt(1));
        Assert.Equal("Alice", shapedEntities.ElementAt(1).Entity.Values.ElementAt(0));
        Assert.Equal(25, shapedEntities.ElementAt(1).Entity.Values.ElementAt(1));
        Assert.Equal("Bob", shapedEntities.ElementAt(2).Entity.Values.ElementAt(0));
        Assert.Equal(40, shapedEntities.ElementAt(2).Entity.Values.ElementAt(1));
    }
}