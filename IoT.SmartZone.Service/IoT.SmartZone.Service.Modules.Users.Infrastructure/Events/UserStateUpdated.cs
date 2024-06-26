using IoT.SmartZone.Service.Shared.Abstractions.Events;

namespace IoT.SmartZone.Service.Modules.Users.Infrastructure.Events;

internal record UserStateUpdated(Guid UserId, string State) : IEvent;