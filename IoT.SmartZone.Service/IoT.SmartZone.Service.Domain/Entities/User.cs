using IoT.SmartZone.Service.Domain.Entities.Common;

namespace IoT.SmartZone.Service.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public virtual IEnumerable<OrganizationMembership> OrganizationMembership { get; set; } = new List<OrganizationMembership>();
}
