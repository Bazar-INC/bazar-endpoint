
using Core.Entities;
using Infrastructure.Data.EntitiesConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

internal class QuestionAnswerEntityConfiguration : BaseEntityConfiguration<QuestionAnswerEntity>
{
    public override void Configure(EntityTypeBuilder<QuestionAnswerEntity> builder)
    {
        base.Configure(builder);

        builder.HasOne(f => f.Owner).WithMany(o => o.QuestionAnswers).IsRequired();
    }
}
