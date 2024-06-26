using Castle.Core.Logging;
using IoT.SmartZone.Service.Modules.Users.Core.Exceptions;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Entities;
using IoT.SmartZone.Service.Modules.Users.Infrastructure.Events;
using IoT.SmartZone.Service.Shared.Abstractions.Commands;
using IoT.SmartZone.Service.Shared.Abstractions.Messaging;
using IoT.SmartZone.Service.Shared.Abstractions.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace IoT.SmartZone.Service.Modules.Users.Core.Commands.SignUp;
internal record class SignUpCommand(string Email, string Password, string Role) : ICommand
{
    internal Guid UserId { get; init; } = Guid.NewGuid();
}

internal sealed class SignUpCommandHandler : ICommandHandler<SignUpCommand>
{
    private readonly RegistrationOptions _registrationOptions;
    private readonly ISignUpCommandDataProvider _dataProvider;
    private readonly IClock _clock;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignUpCommandHandler> _logger;

    public SignUpCommandHandler(
        RegistrationOptions registrationOptions,
        ISignUpCommandDataProvider dataProvider,
        IClock clock,
        IPasswordHasher<User> passwordHasher,
        IMessageBroker messageBroker,
        ILogger<SignUpCommandHandler> logger)
    {
        _registrationOptions = registrationOptions;
        _dataProvider = dataProvider;
        _clock = clock;
        _passwordHasher = passwordHasher;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(SignUpCommand command, CancellationToken ct = default)
    {
        if (!_registrationOptions.Enabled)
            throw new SignUpDisabledException();

        var email = command.Email.ToLowerInvariant();
        var userExists = await _dataProvider.UserExistsAsync(email, ct);
        if (userExists)
            throw new EmailInUseException();

        var roleName = string.IsNullOrWhiteSpace(command.Role) ? Role.Default : command.Role.ToLowerInvariant();

        var role = await _dataProvider.GetRoleAsync(roleName, ct);
        if (role is null)
            throw new RoleNotFoundException(roleName);

        var now = _clock.CurrentDate();
        var password = _passwordHasher.HashPassword(default, command.Password);
        var user = new User()
        {
            Id = command.UserId,
            Email = email,
            Password = password,
            Role = role,
            CreatedAt = now,
            State = UserState.Active,
        };
        await _dataProvider.AddUserAsync(user, ct);
        await _messageBroker.PublishAsync(new SignedUp(user.Id, email, role.Name), ct);
        await _dataProvider.SaveChangesAsync(ct);

        _logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
    }
}
