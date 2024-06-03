using System;
using IoT.SmartZone.Service.Shared.Abstractions.Contexts;

namespace IoT.SmartZone.Service.Shared.Abstractions.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}