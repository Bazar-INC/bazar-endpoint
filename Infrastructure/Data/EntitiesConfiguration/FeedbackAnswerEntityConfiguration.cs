
using Core.Entities;
using Infrastructure.Data.EntitiesConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

internal class FeedbackAnswerEntityConfiguration : BaseEntityConfiguration<FeedbackAnswerEntity>
{
    public override void Configure(EntityTypeBuilder<FeedbackAnswerEntity> builder)
    {
        base.Configure(builder);

        builder.HasOne(f => f.Owner).WithMany(o => o.FeedbackAnswers).IsRequired();
    }
}
