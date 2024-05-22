using IoT.SmartZone.Service.Api.Options;

namespace IoT.SmartZone.Service.Api.Extensions;

public static class OptionsExtensions
{
    public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));
    }
}
