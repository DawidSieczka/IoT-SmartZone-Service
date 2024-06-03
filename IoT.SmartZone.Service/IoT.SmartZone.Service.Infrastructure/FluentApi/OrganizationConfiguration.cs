using IoT.SmartZone.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.SmartZone.Service.Infrastructure.FluentApi;
public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(x=>x.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasMany(x => x.Memberships)
            .WithOne(x => x.Organization);
    }

}

public static class OrganizationConstraints
{
    public const int NameLength = 30;
}
