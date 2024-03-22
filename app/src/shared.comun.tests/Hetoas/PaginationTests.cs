using shared.comun.hetoas;

namespace shared.comun.tests.Hetoas;

public class PaginationTests
{
    [Fact]
    public void Equals_ReturnsTrueForEqualObjects()
    {
        // Arrange
        var pagination1 = new Pagination(100, 10, 2, 10, true, true);
        var pagination2 = new Pagination(100, 10, 2, 10, true, true);

        // Act
        var result = pagination1.Equals(pagination2);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void Equals_ReturnsFalseForDifferentObjects()
    {
        // Arrange
        var pagination1 = new Pagination(100, 10, 2, 10, true, true);
        var pagination2 = new Pagination(200, 20, 3, 20, false, false);

        // Act
        var result = pagination1.Equals(pagination2);

        // Assert
        Assert.False(result);
    }
}