using Core.Entities;
using Infrastructure.Repositories.Abstract;

namespace Infrastructure.UnitOfWork.Abstract;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();

    IRepository<CodeEntity> Codes { get; }
    IRepository<ProductEntity> Products { get; }
    IRepository<CategoryEntity> Categories { get; }
}
