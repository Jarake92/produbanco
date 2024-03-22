using shared.comun.hetoas.extensions;

namespace shared.comun.tests.Hetoas.ExtensionsTests;

public class PagedListTests
{
    [Fact]
    public void ToPagedList_ReturnsCorrectPage()
    {
        // Arrange
        var source = Enumerable.Range(1, 10);
        const int pageNumber = 2;
        const int pageSize = 3;

        // Act
        var result = PagedList<int>.ToPagedList(source, pageNumber, pageSize);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(4, result[0]);
        Assert.Equal(5, result[1]);
        Assert.Equal(6, result[2]);
        Assert.Equal(10, result.TotalCount);
        Assert.Equal(3, result.PageSize);
        Assert.Equal(2, result.CurrentPage);
        Assert.Equal(4, result.TotalPages);
    }
    
    [Fact]
    public void ToPagedList_ReturnsSingleItemList()
    {
        // Arrange
        const int item = 42;

        // Act
        var result = PagedList<int>.ToPagedList(item);

        // Assert
        Assert.Single(result);
        Assert.Equal(item, result[0]);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal(1, result.TotalPages);
    }
}