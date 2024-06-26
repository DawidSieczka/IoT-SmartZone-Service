using IoT.SmartZone.Service.Shared.Abstractions.Auth;

namespace IoT.SmartZone.Service.Modules.Users.Infrastructure.Services;

internal interface IUserRequestStorage
{
    void SetToken(Guid commandId, JsonWebToken jwt);
    JsonWebToken GetToken(Guid commandId);
}