using System;
using IoT.SmartZone.Service.Shared.Abstractions.Contexts;
using IoT.SmartZone.Service.Shared.Abstractions.Messaging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Contexts;

public class MessageContext : IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }

    public MessageContext(Guid messageId, IContext context)
    {
        MessageId = messageId;
        Context = context;
    }
}