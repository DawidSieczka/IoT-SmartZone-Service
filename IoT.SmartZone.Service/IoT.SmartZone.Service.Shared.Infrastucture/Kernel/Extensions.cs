using System.Collections.Generic;
using System.Reflection;
using IoT.SmartZone.Service.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Kernel;

public static class Extensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}