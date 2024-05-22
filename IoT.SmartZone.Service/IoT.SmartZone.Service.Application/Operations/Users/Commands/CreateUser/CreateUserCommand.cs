using IoT.SmartZone.Service.Application.Common.Exceptions;
using IoT.SmartZone.Service.Application.Operations.Users.Commands.Responses;
using MediatR;

namespace IoT.SmartZone.Service.Application.Operations.Users.Commands.CreateUser;
public class CreateUserCommand : IRequest<UserDto>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly ICreateUserCommandDataProvider _dataProvider;

    public CreateUserCommandHandler(ICreateUserCommandDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var user = await _dataProvider.CreateUserAsync(request, ct);

        if (user == null)
        {
            throw new InternalException("Something went wrong while creating a new user.");
        }

        await _dataProvider.SaveChangesAsync(ct);

        return new UserDto()
        {
            ExternalId = user.ExternalId,
            Email = user.Email,
            LastName = user.LastName,
            Name = user.Name
        };
    }
}
