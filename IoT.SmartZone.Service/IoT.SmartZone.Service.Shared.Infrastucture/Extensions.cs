using System.Reflection;
using IoT.SmartZone.Service.Shared.Abstractions;
using IoT.SmartZone.Service.Shared.Abstractions.Dispatchers;
using IoT.SmartZone.Service.Shared.Abstractions.Modules;
using IoT.SmartZone.Service.Shared.Abstractions.Storage;
using IoT.SmartZone.Service.Shared.Abstractions.Time;
using IoT.SmartZone.Service.Shared.Infrastucture.Api;
using IoT.SmartZone.Service.Shared.Infrastucture.Auth;
using IoT.SmartZone.Service.Shared.Infrastucture.Commands;
using IoT.SmartZone.Service.Shared.Infrastucture.Contexts;
using IoT.SmartZone.Service.Shared.Infrastucture.Dispatchers;
using IoT.SmartZone.Service.Shared.Infrastucture.Events;
using IoT.SmartZone.Service.Shared.Infrastucture.Exceptions;
using IoT.SmartZone.Service.Shared.Infrastucture.Kernel;
using IoT.SmartZone.Service.Shared.Infrastucture.Logging;
using IoT.SmartZone.Service.Shared.Infrastucture.Messaging;
using IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Outbox;
using IoT.SmartZone.Service.Shared.Infrastucture.Modules;
using IoT.SmartZone.Service.Shared.Infrastucture.Queries;
using IoT.SmartZone.Service.Shared.Infrastucture.Security;
using IoT.SmartZone.Service.Shared.Infrastucture.Serialization;
using IoT.SmartZone.Service.Shared.Infrastucture.Storage;
using IoT.SmartZone.Service.Shared.Infrastucture.Time;
using IoT.SmartZone.Service.Shared.Infrastucture.DataProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using IoT.SmartZone.Service.Shared.Infrastucture.Postgres;
using IoT.SmartZone.Service.Shared.Infrastucture.Services;

namespace IoT.SmartZone.Service.Shared.Infrastucture;

public static class Extensions
{
    private const string _appSectionName = "app";
    private const string _correlationIdKey = "correlation-id";

    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
        => services.AddTransient<IInitializer, T>();

    public static IHostBuilder ConfigureModules(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach (var settings in GetSettings("*"))
                {
                    cfg.AddJsonFile(settings);
                }

                foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
                {
                    cfg.AddJsonFile(settings);
                }

                IEnumerable<string> GetSettings(string pattern)
                    => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath,
                        $"module.{pattern}.json", SearchOption.AllDirectories);
            });

    public static IServiceCollection AddModularInfrastructure(this IServiceCollection services,
        IConfiguration configuration, IList<Assembly> assemblies, IList<IModule> modules)
    {
        var disabledModules = new List<string>();
        foreach (var (key, value) in configuration.AsEnumerable())
        {
            if (!key.Contains(":module:enabled"))
            {
                continue;
            }

            if (!bool.Parse(value))
            {
                disabledModules.Add(key.Split(":")[0]);
            }
        }

        services.AddCorsPolicy(configuration);
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Modular API",
                Version = "v1"
            });
        });

        var appOptionsSection = configuration.GetSection(_appSectionName);
        services.Configure<AppOptions>(appOptionsSection);

        var appOptions = configuration.GetAppOptions();
        var appInfo = new AppInfo(appOptions.Name, appOptions.Version);
        services.AddSingleton(appInfo);

        services.AddHandlerDataProviders();
        services.AddMemoryCache();
        services.AddHttpClient();
        services.AddSingleton<IRequestStorage, RequestStorage>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddModuleInfo(modules);
        services.AddModuleRequests(assemblies);
        services.AddAuth(configuration, modules);
        services.AddErrorHandling();
        services.AddContext();
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        services.AddEvents(assemblies);
        services.AddDomainEvents(assemblies);
        services.AddMessaging(configuration);
        services.AddSecurity(configuration);
        services.AddOutbox(configuration);
        services.AddPostgres();
        services.AddHostedService<DbContextAppInitializer>();
        services.AddSingleton<IClock, UtcClock>();
        services.AddSingleton<IDispatcher, InMemoryDispatcher>();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;
    }

    public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });
        app.UseCors("cors");
        app.UseCorrelationId();
        app.UseErrorHandling();
        app.UseSwagger();
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl("/swagger/v1/swagger.json");
            reDoc.DocumentTitle = "Modular API";
        });
        app.UseAuth();
        app.UseContext();
        app.UseLogging();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }

    public static AppOptions GetAppOptions(this IConfiguration configuration)
        => configuration.GetOptions<AppOptions>(_appSectionName);

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => configuration.GetSection(sectionName).GetOptions<T>();

    public static T GetOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }
    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static string GetModuleName(this object value)
        => value?.GetType().GetModuleName() ?? string.Empty;

    public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace.Contains(namespacePart)
            ? type.Namespace.Split(".")[splitIndex].ToLowerInvariant()
            : string.Empty;
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        => app.Use((ctx, next) =>
        {
            ctx.Items.Add(_correlationIdKey, Guid.NewGuid());
            return next();
        });

    public static Guid? TryGetCorrelationId(this HttpContext context)
        => context.Items.TryGetValue(_correlationIdKey, out var id) ? (Guid)id : null;
}