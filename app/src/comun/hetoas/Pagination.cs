namespace shared.comun.hetoas;

public class Pagination
{
    public int TotalCount { get; }
    public int PageSize { get; }
    public int CurrentPage { get; }
    public int TotalPages { get; }
    public bool HasNext { get; }
    public bool HasPrevious { get; }

    public Pagination(int totalCount, int pageSize, int currentPage, int totalPages, bool hasNext, bool hasPrevious)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        HasNext = hasNext;
        HasPrevious = hasPrevious;
    }

    public override bool Equals(object? obj)
    {
        return obj is Pagination other &&
               TotalCount == other.TotalCount &&
               PageSize == other.PageSize &&
               CurrentPage == other.CurrentPage &&
               TotalPages == other.TotalPages &&
               HasNext == other.HasNext &&
               HasPrevious == other.HasPrevious;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TotalCount, PageSize, CurrentPage, TotalPages, HasNext, HasPrevious);
    }
}