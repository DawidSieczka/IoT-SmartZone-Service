using IoT.SmartZone.Service.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Caching.Memory;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Contexts;

public class MessageContextProvider : IMessageContextProvider
{
    private readonly IMemoryCache _cache;

    public MessageContextProvider(IMemoryCache cache)
    {
        _cache = cache;
    }

    public IMessageContext Get(IMessage message) => _cache.Get<IMessageContext>(message);
}