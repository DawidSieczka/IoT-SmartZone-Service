using IoT.SmartZones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.SmartZone.Service.Infrastructure.FluentApi;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.ExternalId);
        builder.Property(x => x.ExternalId).HasDefaultValueSql("NEWID()");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(UserConstraints.NameLength);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(UserConstraints.LastNameLength);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(UserConstraints.EmailLength);
    }
}

public static class UserConstraints
{
    public const int NameLength = 20;
    public const int LastNameLength = 20;
    public const int EmailLength = 30;

}
