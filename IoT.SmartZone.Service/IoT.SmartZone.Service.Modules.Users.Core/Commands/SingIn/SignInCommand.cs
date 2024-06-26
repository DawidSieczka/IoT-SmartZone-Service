using IoT.SmartZone.Service.Modules.Users.Core.Exceptions;
using IoT.SmartZone.Service.Modules.Users.Infrastructure;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Entities;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Events;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Services;
using IoT.SmartZone.Service.Shared.Abstractions;
using IoT.SmartZone.Service.Shared.Abstractions.Auth;
using IoT.SmartZone.Service.Shared.Abstractions.Commands;
using IoT.SmartZone.Service.Shared.Abstractions.DataProvider;
using IoT.SmartZone.Service.Shared.Abstractions.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace IoT.SmartZone.Service.Modules.Users.Core.Commands.SingIn;
internal record SignInCommand([Required][EmailAddress] string Email, [Required] string Password) : ICommand
{
    internal Guid Id { get; init; } = Guid.NewGuid();
}

internal sealed class SignInCommandHandler : ICommandHandler<SignInCommand>
{
    private readonly ISignInCommandDataProvider _dataProvider;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IAuthManager _authManager;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignInCommandHandler> _logger;
    private readonly IUserRequestStorage _userRequestStorage;

    public SignInCommandHandler(
        ISignInCommandDataProvider dataProvider,
        PasswordHasher<User> passwordHasher,
        IAuthManager authManager,
        IMessageBroker messageBroker,
        ILogger<SignInCommandHandler> logger,
        IUserRequestStorage userRequestStorage
        )
    {
        _dataProvider = dataProvider;
        _passwordHasher = passwordHasher;
        _authManager = authManager;
        _messageBroker = messageBroker;
        _logger = logger;
        _userRequestStorage = userRequestStorage;
    }

    public async Task HandleAsync(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _dataProvider.GetUserByEmailAsync(command.Email, cancellationToken)
            .NotNull(()=> throw new InvalidCredentialsException());

        if (user.State != UserState.Active)
        {
            throw new UserNotActiveException(user.Id);
        }

        if (_passwordHasher.VerifyHashedPassword(default, user.Password, command.Password) ==
            PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException();
        }

        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Role.Permissions
        };


        var jwt = _authManager.CreateToken(user.Id, user.Role.Name, claims: claims);
        jwt.Email = user.Email;
        await _messageBroker.PublishAsync(new SignedIn(user.Id), cancellationToken);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        _userRequestStorage.SetToken(command.Id, jwt);
    }
}

internal sealed class SignInCommandDataProvider : ISignInCommandDataProvider
{
    private readonly UsersDbContext _usersDbContext;

    public SignInCommandDataProvider(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken ct = default)
        => await _usersDbContext.Users.FirstOrDefaultAsync(x => x.Email == email, ct);

}

internal interface ISignInCommandDataProvider : IDataProvider
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken ct = default);
}