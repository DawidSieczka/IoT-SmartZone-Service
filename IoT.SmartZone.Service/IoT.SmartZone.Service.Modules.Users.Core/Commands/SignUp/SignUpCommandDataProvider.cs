using IoT.SmartZone.Service.Modules.Users.Infrastructure;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Entities;
using IoT.SmartZone.Service.Shared.Abstractions.DataProvider;
using Microsoft.EntityFrameworkCore;

namespace IoT.SmartZone.Service.Modules.Users.Core.Commands.SignUp;

internal interface ISignUpCommandDataProvider : IDataProvider
{
    /// <summary>
    /// Add a new user to the database.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddUserAsync(User user, CancellationToken ct);

    /// <summary>
    /// Get role by name.
    /// </summary>
    /// <param name="roleName">Role name to search for.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The role with the specified name.</returns>
    Task<Role?> GetRoleAsync(string roleName, CancellationToken ct);

    /// <summary>
    /// Saves the changes made to the database.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveChangesAsync(CancellationToken ct);

    /// <summary>
    /// Check if user with given email exists.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>True if the user exists, false otherwise.</returns>
    Task<bool> UserExistsAsync(string email, CancellationToken ct = default);
}

internal sealed class SignUpCommandDataProvider : ISignUpCommandDataProvider
{
    private readonly UsersDbContext _usersDbContext;

    public SignUpCommandDataProvider(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<bool> UserExistsAsync(string email, CancellationToken ct = default) 
        => await _usersDbContext.Users.AnyAsync(x => x.Email == email, ct);

    public async Task<Role?> GetRoleAsync(string roleName, CancellationToken ct) 
        => await _usersDbContext.Roles.FirstOrDefaultAsync(x=>x.Name == roleName,ct);

    public async Task AddUserAsync(User user,CancellationToken ct)
        => await _usersDbContext.Users.AddAsync(user, ct);

    public async Task SaveChangesAsync(CancellationToken ct)
        => await _usersDbContext.SaveChangesAsync(ct);
}
