using Core.Entities.Abstract;
using Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected ApplicationDbContext _context { get; }

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity?> FindAsync(Guid id)
    {
        return await _context.FindAsync<TEntity>(id);
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        return (await _context.Set<TEntity>().AddAsync(entity)).Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        return _context.Update(entity).Entity;
    }

    public virtual void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }
        else
        {
            return query;
        }
    }
}

