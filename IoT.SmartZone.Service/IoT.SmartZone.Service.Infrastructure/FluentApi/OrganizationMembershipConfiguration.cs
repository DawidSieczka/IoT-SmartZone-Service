using IoT.SmartZone.Service.Domain.Entities;
using IoT.SmartZone.Service.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.SmartZone.Service.Infrastructure.FluentApi;
public class OrganizationMembershipConfiguration : IEntityTypeConfiguration<OrganizationMembership>
{
    public void Configure(EntityTypeBuilder<OrganizationMembership> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany(x => x.OrganizationMembership);

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.Memberships);

        builder.Property(x => x.membershipType)
            .IsRequired()
            .HasDefaultValue(MembershipType.Member);
    }
}
