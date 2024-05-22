using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IoT.SmartZone.Service.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseApi : ControllerBase
{
    public BaseApi(ISender sender)
    {
        Sender = sender;
    }

    public ISender Sender { get; }
}
