using IoT.SmartZone.Service.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Modules.Users.Core.Exceptions;
internal class RoleNotFoundException : ModularException
{
    public RoleNotFoundException() : base("Role not found.")
    {
        
    }
    public RoleNotFoundException(string roleName) : base($"Role with name: {roleName} not found.")
    {

    }
}
