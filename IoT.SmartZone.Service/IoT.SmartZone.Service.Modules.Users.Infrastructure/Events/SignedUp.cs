using IoT.SmartZone.Service.Shared.Abstractions.Events;

namespace IoT.SmartZone.Service.Modules.Users.Infrastructure.Events;

internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;