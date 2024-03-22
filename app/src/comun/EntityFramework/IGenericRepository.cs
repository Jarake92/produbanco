using System.Linq.Expressions;
using shared.comun.hetoas;

namespace shared.comun.EntityFramework;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task DeleteMany(IEnumerable<T> entities);
}