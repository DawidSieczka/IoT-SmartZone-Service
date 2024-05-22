using IoT.SmartZone.Service.Application.Operations.Users.Commands.CreateUser;
using IoT.SmartZone.Service.Application.Operations.Users.Commands.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IoT.SmartZone.Service.Api.Controllers;

public class UsersController : BaseApi
{
    private readonly ISender _sender;

    public UsersController(ISender sender) : base(sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserCommand command, CancellationToken ct)
    {
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }
}
