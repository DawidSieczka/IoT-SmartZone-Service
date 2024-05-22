using IoT.SmartZone.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IoT.SmartZone.Service.Api.Extensions;

public static class DbSetupExtensions
{
    public static void SetupDatabase(this IServiceCollection services, string sqlDbConnectionString)
    {
        services.AddDbContext<AppDbContext>(b => b
            .UseLazyLoadingProxies()
            .UseSqlServer(sqlDbConnectionString, options => options.CommandTimeout(120)
            .EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null)
        ));

        using var appDbContext = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
        appDbContext.Database.Migrate();
    }
}