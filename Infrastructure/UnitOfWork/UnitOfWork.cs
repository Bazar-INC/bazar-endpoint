using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstract;
using Infrastructure.UnitOfWork.Abstract;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    public IRepository<CodeEntity> Codes { get; } 
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Codes = new Repository<CodeEntity>(_context);
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
