using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstract;
using Infrastructure.UnitOfWork.Abstract;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private IRepository<CodeEntity>? codes;

    public IRepository<CodeEntity> Codes
    {
        get
        {
            if (codes is null)
            {
                codes = new Repository<CodeEntity>(_context);
            }
            return codes;
        }
    }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
