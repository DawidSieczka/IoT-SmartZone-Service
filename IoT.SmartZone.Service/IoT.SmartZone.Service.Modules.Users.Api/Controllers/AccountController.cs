using IoT.SmartZone.Service.Modules.Users.Core.Commands.SignUp;
using IoT.SmartZone.Service.Modules.Users.Core.Queries;
using IoT.SmartZone.Service.Shared.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IoT.SmartZone.Service.Modules.Users.Api.Controllers;

internal class AccountController : ApiController
{
    private const string _accessTokenCookie = "__access-token";
    private readonly IDispatcher _dispatcher;
    //private readonly IContext _context;
    //private readonly IUserRequestStorage _userRequestStorage;
    //private readonly CookieOptions _cookieOptions;

    public AccountController(IDispatcher dispatcher) 
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync(SignUpCommand command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    //[HttpPost("sign-in")]
    //[SwaggerOperation("Sign in")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<UserDetailsDto>> SignInAsync(SignInCommand command)
    //{
    //    await _dispatcher.SendAsync(command);
    //    var jwt = _userRequestStorage.GetToken(command.Id);
    //    var user = await _dispatcher.QueryAsync(new GetUser { UserId = jwt.UserId });
    //    AddCookie(AccessTokenCookie, jwt.AccessToken);
    //    return Ok(user);
    //}

}