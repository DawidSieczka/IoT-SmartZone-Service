using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Domain.Entities.Common;

public class BaseEntity
{
    public int Id { get; set; }

    public Guid ExternalId { get; set; }
}