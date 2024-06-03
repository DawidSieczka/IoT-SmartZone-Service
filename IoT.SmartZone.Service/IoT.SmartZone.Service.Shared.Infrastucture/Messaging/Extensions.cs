using IoT.SmartZone.Service.Shared.Abstractions.Messaging;
using IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Brokers;
using IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Contexts;
using IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Dispatchers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging;

public static class Extensions
{
    private const string _sectionName = "messaging";

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddTransient<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IMessageContextProvider, MessageContextProvider>();
        services.AddSingleton<IMessageContextRegistry, MessageContextRegistry>();

        var section = configuration.GetSection(_sectionName);
        var messagingOptions = section.GetOptions<MessagingOptions>();
        services.Configure<MessagingOptions>(section);

        if (messagingOptions.UseAsyncDispatcher)
        {
            services.AddHostedService<AsyncDispatcherJob>();
        }

        return services;
    }
}