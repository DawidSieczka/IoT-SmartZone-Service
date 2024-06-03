using Modular.Abstractions.Messaging;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Dispatchers;

public record MessageEnvelope(IMessage Message, IMessageContext MessageContext);