﻿using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstract;
using Infrastructure.UnitOfWork.Abstract;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    public IRepository<CodeEntity> Codes { get; } 
    public IRepository<ProductEntity> Products { get; }
    public IRepository<CategoryEntity> Categories { get; }
    public IRepository<FilterNameEntity> FilterNames { get; }
    public IRepository<FilterValueEntity> FilterValues { get; }
    public IRepository<FeedbackEntity> Feedbacks { get; }
    public IRepository<FeedbackAnswerEntity> FeedbackAnswers { get; }
    public IRepository<QuestionEntity> Questions { get; }
    public IRepository<QuestionAnswerEntity> QuestionAnswers { get; }
    public IRepository<TownEntity> Towns { get; }
    public IRepository<ImageEntity> Images { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Codes = new Repository<CodeEntity>(_context);
        Products = new Repository<ProductEntity>(_context);
        Categories = new Repository<CategoryEntity>(_context);
        FilterNames = new Repository<FilterNameEntity>(_context);
        FilterValues = new Repository<FilterValueEntity>(_context);
        Feedbacks = new Repository<FeedbackEntity>(_context);
        FeedbackAnswers = new Repository<FeedbackAnswerEntity>(_context);
        Questions = new Repository<QuestionEntity>(_context);
        QuestionAnswers = new Repository<QuestionAnswerEntity>(_context);
        Towns = new Repository<TownEntity>(_context);
        Images = new Repository<ImageEntity>(_context);
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
