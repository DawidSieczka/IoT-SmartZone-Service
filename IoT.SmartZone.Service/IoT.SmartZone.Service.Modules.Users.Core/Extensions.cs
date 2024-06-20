
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using IoT.SmartZone.Service.Shared.Infrastucture;

[assembly: InternalsVisibleTo("IoT.SmartZone.Service.Modules.Users.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace IoT.SmartZone.Service.Modules.Users.Core;
internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
        services.AddSingleton(registrationOptions);

        //return services
        //    .AddSingleton<IUserRequestStorage, UserRequestStorage>()
        //    .AddScoped<IRoleRepository, RoleRepository>()
        //    .AddScoped<IUserRepository, UserRepository>()
        //    .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
        //    .AddPostgres<UsersDbContext>()
        //    .AddOutbox<UsersDbContext>()
        //    .AddUnitOfWork<UsersUnitOfWork>()
        //    .AddInitializer<UsersInitializer>();

        return services;
    }
}
