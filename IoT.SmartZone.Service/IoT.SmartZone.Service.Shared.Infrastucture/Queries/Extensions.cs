using System.Collections.Generic;
using System.Reflection;
using IoT.SmartZone.Service.Shared.Abstractions.Queries;
using IoT.SmartZone.Service.Shared.Infrastucture;
using IoT.SmartZone.Service.Shared.Infrastucture.Queries.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddPagedQueryDecorator(this IServiceCollection services)
    {
        services.TryDecorate(typeof(IQueryHandler<,>), typeof(PagedQueryHandlerDecorator<,>));

        return services;
    }
}