﻿using IoT.SmartZone.Service.Shared.Abstractions.Messaging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Contexts;

public interface IMessageContextRegistry
{
    void Set(IMessage message, IMessageContext context);
}