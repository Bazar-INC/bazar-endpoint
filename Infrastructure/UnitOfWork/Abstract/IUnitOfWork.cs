namespace Infrastructure.UnitOfWork.Abstract;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
