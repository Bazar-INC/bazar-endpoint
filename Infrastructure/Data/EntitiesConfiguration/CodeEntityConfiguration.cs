using Core.Entities;
using Infrastructure.Data.EntitiesConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

internal class CodeEntityConfiguration : BaseEntityConfiguration<CodeEntity>
{
    public override void Configure(EntityTypeBuilder<CodeEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Code).IsRequired().HasMaxLength(64);
        builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(64);
    }
}
