using Microsoft.AspNetCore.Hosting;
using IoT.SmartZone.Service.Shared.Infrastucture;

namespace IoT.SmartZone.Service.Bootsrapper;

public class Program
{
    public static Task Main(string[] args)
        => CreateHostBuilder(args).Build().RunAsync();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .ConfigureModules();
}