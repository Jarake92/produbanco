namespace shared.comun.hetoas.extensions;

public interface ISortHelper<T>
{
    IEnumerable<T> ApplySort(IEnumerable<T> entities, string orderByQueryString);
}