using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using IoT.SmartZone.Service.Shared.Infrastucture;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IoT.SmartZone.Service.Modules.Users.Core")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace IoT.SmartZone.Service.Modules.Users.Infrastructure;

internal sealed class UsersInitializer : IInitializer
{
    private readonly HashSet<string> _permissions = new()
    {
        "users",
        "admins"
    };

    private readonly UsersDbContext _dbContext;
    private readonly ILogger<UsersInitializer> _logger;

    public UsersInitializer(UsersDbContext dbContext, ILogger<UsersInitializer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InitAsync()
    {
        if (await _dbContext.Roles.AnyAsync())
        {
            return;
        }

        await AddRolesAsync();
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRolesAsync()
    {
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = "admin",
            Permissions = _permissions
        });
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = "user",
            Permissions = new List<string>()
        });

        _logger.LogInformation("Initialized roles.");
    }
}