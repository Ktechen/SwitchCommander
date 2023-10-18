using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Common.Exceptions;
using SwitchCommander.Application.Features.SSH;

namespace SwitchCommander.WebAPI.Controllers.SSH;

public class SSHExecuteCommandController : BaseController
{
    private ILogger<SSHExecuteCommandController> _logger;
    
    public SSHExecuteCommandController(IMediator mediator, ILogger<SSHExecuteCommandController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
            _logger.LogInformation("Execute a command" + response);
            return Ok(response);
        }
        catch (Exception e) when (e is BadRequestException or SSHNetException)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
}