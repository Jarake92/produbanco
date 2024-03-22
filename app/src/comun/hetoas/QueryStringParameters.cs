namespace shared.comun.hetoas;

public abstract class QueryStringParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    public int PageNumber { get; set; } = 1;
    public string OrderBy { get; set; } = string.Empty;
    public string Fields { get; set; } = string.Empty;
    public string Filter { get; set; } = string.Empty;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}