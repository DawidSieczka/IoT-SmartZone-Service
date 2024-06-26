
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using IoT.SmartZone.Service.Shared.Infrastucture;
using Microsoft.AspNetCore.Identity;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Entities;
using IoT.SmartZone.Service.Modules.Users.Infrastructure;
using IoT.SmartZone.Service.Shared.Infrastucture.Postgres;
using IoT.SmartZone.Service.Shared.Infrastucture.Messaging.Outbox;
using IoT.SmartZone.Service.Shared.Infrastucture.DataProvider;

[assembly: InternalsVisibleTo("IoT.SmartZone.Service.Modules.Users.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace IoT.SmartZone.Service.Modules.Users.Core;
internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
        services.AddSingleton(registrationOptions);

        services
            //.AddSingleton<IUserRequestStorage, UserRequestStorage>()
            //.AddScoped<IRoleRepository, RoleRepository>()
            //.AddScoped<IUserRepository, UserRepository>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddPostgres<UsersDbContext>()
            .AddOutbox<UsersDbContext>()
            .AddUnitOfWork<UsersUnitOfWork>()
            .AddInitializer<UsersInitializer>();

        return services;
    }
}
