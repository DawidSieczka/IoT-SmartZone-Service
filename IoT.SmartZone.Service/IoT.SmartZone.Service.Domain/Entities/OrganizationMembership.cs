using IoT.SmartZone.Service.Domain.Entities.Common;
using IoT.SmartZone.Service.Domain.Enums;

namespace IoT.SmartZone.Service.Domain.Entities;
public class OrganizationMembership : BaseEntity
{
    public virtual Organization Organization { get; set; }
    public int OrganizationId { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
    public MembershipType membershipType { get; set; }

}
