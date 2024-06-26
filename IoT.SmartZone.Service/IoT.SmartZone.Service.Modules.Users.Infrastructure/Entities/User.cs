using IoT.SmartZone.Service.Shared.Abstractions.Kernel.ValueObjects;

namespace IoT.SmartZone.Service.Modules.Users.Infrastructure.Entities;

public class User
{
    public Guid Id { get; set; }
    public Email Email { get; set; }
    public string Password { get; set; }
    public virtual Role Role { get; set; }
    public string RoleId { get; set; }
    public UserState State { get; set; }
    public DateTime CreatedAt { get; set; }
}
