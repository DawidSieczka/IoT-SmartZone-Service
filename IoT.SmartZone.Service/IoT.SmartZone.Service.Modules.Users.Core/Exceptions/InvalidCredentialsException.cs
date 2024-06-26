using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

namespace IoT.SmartZone.Service.Modules.Users.Core.Exceptions;
internal class InvalidCredentialsException : ModularException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}
