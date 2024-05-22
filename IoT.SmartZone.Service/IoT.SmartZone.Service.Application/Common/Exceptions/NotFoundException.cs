using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Application.Common.Exceptions;
public class NotFoundException : CustomException
{
    public override int StatusCode { get; set; } = 404;

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(int id) : base($"Object of id: '{id}' not found")
    {
    }

    public NotFoundException(Guid id) : base($"Object of id: '{id}' not found")
    {
    }

    public NotFoundException(int id, Type type) : base($"Object of id: '{id}' and type of: {type} not found")
    {
    }

    public NotFoundException(Guid id, Type type) : base($"Object of id: '{id}' and type of: {type} not found")
    {
    }


    public NotFoundException(string objectName, Guid externalId) : base(
        $"{objectName} of externalId: {externalId} not found!")
    {
    }
}
