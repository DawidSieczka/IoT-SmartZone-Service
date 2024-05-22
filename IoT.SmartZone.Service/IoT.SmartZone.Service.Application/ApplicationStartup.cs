using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IoT.SmartZone.Service.Application;
public static class ApplicationStartup
{
    public static void AddApplicationSetup(this IServiceCollection services)
    {
        var dataProviderName = nameof(IDataProvider);

        var assembly = Assembly.GetAssembly(typeof(IDataProvider));
        if (assembly == null)
            throw new Exception($"Assembly with {dataProviderName} not found.");

        var types = assembly.GetTypes().Where(x=> !x.IsInterface && x.GetInterface(dataProviderName) != null);
        foreach(var type in types)
        {
            var interfaceName = $"I{type.Name}";
            var interfaceType = type.GetInterface(interfaceName);
            if (interfaceType == null)
                throw new Exception($"Interface {interfaceName} not found.");
            
            services.AddScoped(interfaceType, type);
        }
    }
}
