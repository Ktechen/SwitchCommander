using MediatR;

namespace SwitchCommander.WebAPI.Controllers;

public class SSHConfigController : BaseController
{
    public SSHConfigController(IMediator mediator) : base(mediator)
    {
    }
}