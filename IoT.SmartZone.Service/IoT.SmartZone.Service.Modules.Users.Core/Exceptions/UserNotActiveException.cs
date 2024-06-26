using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Modules.Users.Core.Exceptions;
internal class UserNotActiveException : ModularException
{
    public UserNotActiveException(Guid userId) : base($"User with id: '{userId}' is not active.")
    {
    }
}
