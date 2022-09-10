using Core.Entities;
using Infrastructure.Repositories.Abstract;

namespace Infrastructure.UnitOfWork.Abstract;

public interface IUnitOfWork
{
    IRepository<CodeEntity> Codes { get; }
    IRepository<ProductEntity> Products { get; }
    IRepository<CategoryEntity> Categories { get; }
    IRepository<FilterNameEntity> FilterNames { get; }
    IRepository<FilterValueEntity> FilterValues { get; }
    IRepository<FeedbackEntity> Feedbacks { get; }
    IRepository<FeedbackAnswerEntity> FeedbackAnswers { get; }
    IRepository<QuestionEntity> Questions { get; }
    IRepository<QuestionAnswerEntity> QuestionAnswers { get; }
    IRepository<TownEntity> Towns { get; }
    Task<int> SaveChangesAsync();
}
