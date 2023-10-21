using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.Create.Sequence;

namespace SwitchCommander.WebAPI.Controllers.SSH;

public class SSHImportController : BaseController
{
    public SSHImportController(IMediator mediator) : base(mediator)
    {
    }
    

}