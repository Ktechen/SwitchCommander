using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.Create.Command;
using SwitchCommander.Application.Features.SSH.Delete.Command;
using SwitchCommander.Application.Features.SSH.Read.Command;
using SwitchCommander.Application.Features.SSH.Update.Command;

namespace SwitchCommander.WebAPI.Controllers.v1.SSH;

public class SSHCommandController : BaseController
{
    private readonly IValidator<CreateSSHCommandRequest> _validatorCreateSSHCommandRequest;

    public SSHCommandController(
        IMediator mediator,
        IValidator<CreateSSHCommandRequest> validatorCreateSshCommandRequest
    ) : base(mediator)
    {
        _validatorCreateSSHCommandRequest = validatorCreateSshCommandRequest;
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateSSHCommandResponse>> Create([FromBody] CreateSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validatorCreateSSHCommandRequest.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            return BadRequest(errorMessages);
        }

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [Authorize(Policy = "StandardUser")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadSSHCommandResponse>> Read(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(new ReadSSHCommandRequest(result), cancellationToken);
        if (response.Id is null) return BadRequest(response);

        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerator<ReadAllSSHCommandResponse>>> ReadAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ReadAllSSHCommandRequest(), cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateSSHCommandResponse>> Update(UpdateSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteSSHCommandResponse>> Delete(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(new DeleteSSHCommandRequest(result), cancellationToken);
        return Ok(response);
    }
}