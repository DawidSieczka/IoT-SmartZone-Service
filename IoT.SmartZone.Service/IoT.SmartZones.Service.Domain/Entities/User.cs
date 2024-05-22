using IoT.SmartZones.Domain.Entities.Common;

namespace IoT.SmartZones.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
