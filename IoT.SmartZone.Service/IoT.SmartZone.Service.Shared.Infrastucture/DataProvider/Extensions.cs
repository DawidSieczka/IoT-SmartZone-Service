using IoT.SmartZone.Service.Shared.Abstractions.DataProvider;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IoT.SmartZone.Service.Shared.Infrastucture.DataProvider;
public static class Extensions
{
    public static void AddHandlerDataProviders(this IServiceCollection services)
    {
        var dataProviderName = nameof(IDataProvider);

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            if (assembly == null)
                throw new Exception($"Assembly with {dataProviderName} not found.");
            var a = assembly.GetTypes();
            var types = assembly.GetTypes().Where(x => !x.IsInterface && x.GetInterface(dataProviderName) != null);
            var c = types.ToList();

            foreach (var type in types)
            {
                var interfaceName = $"I{type.Name}";
                var interfaceType = type.GetInterface(interfaceName);
                if (interfaceType == null)
                    throw new Exception($"Interface {interfaceName} not found.");

                services.AddScoped(interfaceType, type);
            }
        }
    }
}
