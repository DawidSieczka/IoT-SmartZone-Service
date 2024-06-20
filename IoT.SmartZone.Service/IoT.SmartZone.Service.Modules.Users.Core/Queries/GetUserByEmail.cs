using IoT.SmartZone.Service.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Modules.Users.Core.Queries;
internal class GetUserByEmail : IQuery<UserDetailsDto>
{
    public string Email { get; set; }
}