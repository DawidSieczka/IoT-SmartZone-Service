using System.Collections.Generic;
using System.Reflection;
using IoT.SmartZone.Service.Shared.Abstractions.Events;
using IoT.SmartZone.Service.Shared.Infrastucture;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Events;

public static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}