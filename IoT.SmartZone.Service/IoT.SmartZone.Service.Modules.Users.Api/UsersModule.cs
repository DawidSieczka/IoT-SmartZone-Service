using IoT.SmartZone.Service.Shared.Abstractions.Modules;
using IoT.SmartZone.Service.Modules.Users.Core;
using IoT.SmartZone.Service.Shared.Infrastucture.Modules;
using IoT.SmartZone.Service.Shared.Abstractions.Queries;
using IoT.SmartZone.Service.Modules.Users.Core.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;


namespace IoT.SmartZone.Service.Modules.Users.Api;

internal class UsersModule : IModule
{
    public string Name { get; } = "Users";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "users"
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests()
            .Subscribe<GetUserByEmail, UserDetailsDto>("users/get",
                (query, serviceProvider, cancellationToken) =>
                    serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
    }
}
