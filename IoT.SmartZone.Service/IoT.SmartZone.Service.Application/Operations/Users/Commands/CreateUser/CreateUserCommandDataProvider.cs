using IoT.SmartZone.Service.Infrastructure;
using IoT.SmartZone.Service.Domain.Entities;

namespace IoT.SmartZone.Service.Application.Operations.Users.Commands.CreateUser;

public interface ICreateUserCommandDataProvider : IDataProvider
{
    Task<User> CreateUserAsync(CreateUserCommand command, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}

public class CreateUserCommandDataProvider : ICreateUserCommandDataProvider
{
    private readonly AppDbContext _dbContext;

    public CreateUserCommandDataProvider(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateUserAsync(CreateUserCommand command, CancellationToken ct = default)
    {
        var entry = await _dbContext.Users.AddAsync(new User()
        {
            Name = command.Name,
            LastName = command.LastName,
            Email = command.Email
        }, ct);

        return entry.Entity;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await _dbContext.SaveChangesAsync(ct);
}
