using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SwitchCommander.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    protected BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
