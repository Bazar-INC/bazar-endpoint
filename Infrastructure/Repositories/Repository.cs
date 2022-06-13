using Core.Entities.Abstract;
using Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

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

    public IQueryable<TEntity> Get()
    {
        return _context.Set<TEntity>();
    }
}

