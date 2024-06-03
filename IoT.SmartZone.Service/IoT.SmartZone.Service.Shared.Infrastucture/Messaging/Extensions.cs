using IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Brokers;
using IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Contexts;
using IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Dispatchers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modular.Abstractions.Messaging;
using Modular.Infrastructure;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Messaging;

public static class Extensions
{
    private const string SectionName = "messaging";

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddTransient<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IMessageContextProvider, MessageContextProvider>();
        services.AddSingleton<IMessageContextRegistry, MessageContextRegistry>();

        var section = configuration.GetSection(SectionName);
        var messagingOptions = section.GetOptions<MessagingOptions>();
        services.Configure<MessagingOptions>(section);

        if (messagingOptions.UseAsyncDispatcher)
        {
            services.AddHostedService<AsyncDispatcherJob>();
        }

        return services;
    }
}