using IoT.SmartZone.Service.Shared.Abstractions.Dispatchers;
using IoT.SmartZone.Service.Shared.Infrastucture.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.SmartZone.Service.Modules.Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesDefaultContentType]
public abstract class ApiController : ControllerBase
{
    
}
