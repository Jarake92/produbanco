using shared.comun.hetoas.extensions;
using System.Linq.Dynamic.Core;

namespace shared.comun.hetoas;

public abstract class EntityHelper<T> : IEntityHelper<T> where T : class
{
    private readonly ISortHelper<T> _sortHelper;
    private readonly IDataShaper<T> _dataShaper;

    protected EntityHelper(ISortHelper<T> sortHelper, IDataShaper<T> dataShaper)
    {
        _sortHelper = sortHelper;
        _dataShaper = dataShaper;
    }

    public virtual IEnumerable<T> GetFilteredEntities(IQueryable<T> entities, QueryStringParameters entityParameters)
    {
        if (!entities.Any() || string.IsNullOrWhiteSpace(entityParameters.Filter))
            return entities;

        if (!entityParameters.Filter.Contains("=="))
            return entities.Where(entityParameters.Filter);

        return entities.Where(entityParameters.Filter).ToList();
    }

    public Pagination GetPagination(PagedList<ShapedEntity> shapedEntities)
    {
        return new Pagination(
            shapedEntities.TotalCount,
            shapedEntities.PageSize,
            shapedEntities.CurrentPage,
            shapedEntities.TotalPages,
            shapedEntities.HasNext,
            shapedEntities.HasPrevious
        );
    }
    
    public ShapedEntity GetShapedEntity(T entity, QueryStringParameters entityParameters)
    {
        return _dataShaper.ShapeData(entity, entityParameters.Fields);
    }

    public PagedList<ShapedEntity> GetShapedEntities(IEnumerable<T> entities, QueryStringParameters entityParameters)
    {
        if (string.IsNullOrWhiteSpace(entityParameters.OrderBy))
            return GetPagedList(entities, entityParameters);

        var sortedEntities = _sortHelper.ApplySort(entities, entityParameters.OrderBy);

        return GetPagedList(sortedEntities, entityParameters);
    }

    private PagedList<ShapedEntity> GetPagedList(IEnumerable<T> source, QueryStringParameters entityParameters)
    {
        var shapedEntities = _dataShaper.ShapeData(source, entityParameters.Fields);

        return PagedList<ShapedEntity>.ToPagedList(shapedEntities,
            entityParameters.PageNumber,
            entityParameters.PageSize);
    }
}