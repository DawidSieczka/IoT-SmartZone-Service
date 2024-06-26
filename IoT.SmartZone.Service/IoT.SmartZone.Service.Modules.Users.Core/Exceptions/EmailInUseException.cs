using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

namespace IoT.SmartZone.Service.Modules.Users.Core.Exceptions;

internal class EmailInUseException : ModularException
{
    public EmailInUseException() : base("Email is already in use.")
    {
    }
}