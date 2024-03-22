using shared.comun.hetoas.extensions;

namespace shared.comun.hetoas;

public interface IEntityHelper<T> where T : class
{
    IEnumerable<T> GetFilteredEntities(IQueryable<T> entities, QueryStringParameters entityParameters);
    Pagination GetPagination(PagedList<ShapedEntity> shapedEntities);
    ShapedEntity GetShapedEntity(T entity, QueryStringParameters entityParameters);
    PagedList<ShapedEntity> GetShapedEntities(IEnumerable<T> entities, QueryStringParameters entityParameters);
}