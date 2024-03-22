using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace shared.comun.hetoas.extensions;

public class SortHelper<T> : ISortHelper<T>
{
    public IEnumerable<T> ApplySort(IEnumerable<T> entities, string orderByQueryString)
    {
        var applySort = entities as T[] ?? entities.ToArray();
        if (!applySort.Any())
            return applySort;

        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return applySort;
        }

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi =>
                pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var descending = param.EndsWith(" desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{objectProperty.Name} {descending}, ");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

        return applySort.AsQueryable().OrderBy(orderQuery);
    }
}