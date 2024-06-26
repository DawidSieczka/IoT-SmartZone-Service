using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;

namespace IoT.SmartZone.Service.Modules.Users.Core.Exceptions;
internal class SignUpDisabledException : ModularException
{
    public SignUpDisabledException() : base("Sign up is disabled.")
    {

    }
}
