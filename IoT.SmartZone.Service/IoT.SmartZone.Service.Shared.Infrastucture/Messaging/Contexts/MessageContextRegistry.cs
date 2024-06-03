using System;
using IoT.SmartZone.Service.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Caching.Memory;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Contexts;

public class MessageContextRegistry : IMessageContextRegistry
{
    private readonly IMemoryCache _cache;

    public MessageContextRegistry(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set(IMessage message, IMessageContext context)
        => _cache.Set(message, context, new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(1)
        });
}