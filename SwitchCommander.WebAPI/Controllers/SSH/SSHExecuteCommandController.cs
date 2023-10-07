using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Common.Exceptions;
using SwitchCommander.Application.Features.SSH;

namespace SwitchCommander.WebAPI.Controllers.SSH;

public class SSHExecuteCommandController : BaseController
{
    public SSHExecuteCommandController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExecuteSSHCommandResponse>> Execute([FromBody] ExecuteSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.ServerId, out var serverIdResult)) return BadRequest("ServerId is invalid");
        if (!Guid.TryParse(request.CommandId, out var result)) return BadRequest("CommandId is invalid");

        try
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        catch (Exception e) when (e is BadRequestException or SSHNetException)
        {
            return BadRequest(e.Message);
        }
    }
}