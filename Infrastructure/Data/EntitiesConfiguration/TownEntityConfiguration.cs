
using Core.Entities;
using Infrastructure.Data.EntitiesConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

internal class TownEntityConfiguration : BaseEntityConfiguration<TownEntity>
{
    public override void Configure(EntityTypeBuilder<TownEntity> builder)
    {
        base.Configure(builder);

        builder.Property(t => t.Name).IsRequired();
    }
}
