using IoT.SmartZone.Service.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.SmartZone.Service.Infrastructure.FluentApi;
public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.ExternalId);
        builder.Property(x => x.ExternalId).HasDefaultValueSql("NEWID()");
    }
}
