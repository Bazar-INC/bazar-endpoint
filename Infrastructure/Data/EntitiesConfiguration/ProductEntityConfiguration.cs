using Core.Entities;
using Infrastructure.Data.EntitiesConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

internal class ProductEntityConfiguration : BaseEntityConfiguration<ProductEntity>
{
    public override void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).HasMaxLength(256);

        builder.HasOne(p => p.Category).WithMany(c => c.Products);
    }
}
