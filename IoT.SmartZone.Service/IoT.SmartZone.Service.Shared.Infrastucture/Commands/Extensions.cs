﻿using System.Collections.Generic;
using System.Reflection;
using IoT.SmartZone.Service.Shared.Infrastucture;
using Microsoft.Extensions.DependencyInjection;
using Modular.Abstractions.Commands;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}