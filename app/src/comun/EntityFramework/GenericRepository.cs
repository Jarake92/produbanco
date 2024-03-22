using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using shared.comun.hetoas;

namespace shared.comun.EntityFramework;

public abstract class GenericRepository<T, TContext> : IGenericRepository<T>
    where T : class, IEntity
    where TContext : DbContext
{
    private readonly TContext _context;

    protected GenericRepository(TContext context)
    {
        _context = context;
    }

    public IQueryable<T> FindAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>()
            .Where(expression)
            .AsNoTracking();
    }

    public async Task Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteMany(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}