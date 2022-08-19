
using Core.Entities;
using Core.Entities.Abstract;
using Infrastructure.Data.EntitiesConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

internal class FeedbackEntityConfiguration : BaseEntityConfiguration<FeedbackEntity>
{
    public override void Configure(EntityTypeBuilder<FeedbackEntity> builder)
    {
        base.Configure(builder);

        builder.HasOne(f => f.Owner).WithMany(o => o.Feedbacks);
    }
}
