using Core.Entities;
using Infrastructure.Data.EntitiesConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

internal class QuestionEntityConfiguration : BaseEntityConfiguration<QuestionEntity>
{
    public override void Configure(EntityTypeBuilder<QuestionEntity> builder)
    {
        base.Configure(builder);

        builder.HasOne(f => f.Owner).WithMany(o => o.Questions).IsRequired();
        builder.HasOne(f => f.Product).WithMany(p => p.Questions);
    }
}
