using Core.Entities.Abstract;

namespace Infrastructure.Repositories.Abstract;

public interface IRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> Get();
    Task<TEntity?> FindAsync(Guid id);
    Task<TEntity> InsertAsync(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
}