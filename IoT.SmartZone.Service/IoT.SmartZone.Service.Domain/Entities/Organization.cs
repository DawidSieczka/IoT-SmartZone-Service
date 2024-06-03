using IoT.SmartZone.Service.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Domain.Entities;

public class Organization : BaseEntity
{
    public string Name { get; set; }
    public virtual IEnumerable<OrganizationMembership> Memberships { get; set; }
}