using IoT.SmartZone.Service.Shared.Abstractions.Modules;
using IoT.SmartZone.Service.Shared.Infrastucture.Modules;
using IoT.SmartZone.Service.Shared.Infrastucture;
using IoT.SmartZone.Service.Shared.Infrastucture.Contracts;
using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;
using IoT.SmartZone.Service.Modules.Users.Infrastructure;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoT.SmartZone.Service.Bootsrapper;

public class Startup
{
    private readonly IList<Assembly> _assemblies;
    private readonly IList<IModule> _modules;
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _assemblies = ModuleLoader.LoadAssemblies(configuration, "IoT.SmartZone.Service.Modules.");
        _modules = ModuleLoader.LoadModules(_assemblies);
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddModularInfrastructure(_configuration, _assemblies, _modules);
        services.AddContracts();

        foreach (var module in _modules)
        {
            module.Register(services);
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        logger.LogInformation($"Modules: {string.Join(", ", _modules.Select(x => x.Name))}");
        app.UseModularInfrastructure();
        foreach (var module in _modules)
        {
            module.Use(app);
        }

        app.ValidateContracts(_assemblies);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync("IoT SmartZone Service Api"));
            endpoints.MapModuleInfo();
            
        });

        app.UseSwagger();
        
        _assemblies.Clear();
        _modules.Clear();
    }
}