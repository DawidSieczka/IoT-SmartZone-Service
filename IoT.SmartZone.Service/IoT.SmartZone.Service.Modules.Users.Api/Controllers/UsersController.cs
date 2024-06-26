using IoT.SmartZone.Service.Modules.Users.Core.Commands.SignUp;
using IoT.SmartZone.Service.Modules.Users.Core.Queries;
using IoT.SmartZone.Service.Shared.Abstractions.Contexts;
using IoT.SmartZone.Service.Shared.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IoT.SmartZone.Service.Modules.Users.Api.Controllers;

[Authorize(_policy)]
public class UsersController : ApiController
{
    private const string _policy = "users";
    private readonly IDispatcher _dispatcher;

    public UsersController(IDispatcher dispatcher) 
    {
        _dispatcher = dispatcher;
    }

    [HttpGet()]
    [SwaggerOperation("Get user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserDetailsDto>> GetAsync()
        => Ok(new UserDetailsDto()
        {
            CreatedAt = DateTime.UtcNow,
            Email = "daw@sie.pl",
            UserId = Guid.NewGuid(),
            Permissions = new List<string>() { "All" },
            Role = "Admin",
            State = ""

        });
}
