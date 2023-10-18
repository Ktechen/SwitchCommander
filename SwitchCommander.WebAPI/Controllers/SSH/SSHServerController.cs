using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.Create.Server;
using SwitchCommander.Application.Features.SSH.Delete.Server;
using SwitchCommander.Application.Features.SSH.Read.Server;
using SwitchCommander.Application.Features.SSH.Update.Server;

namespace SwitchCommander.WebAPI.Controllers.SSH;

public class SSHServerController : BaseController
{
    private readonly IValidator<CreateSSHServerRequest> _validatorCreateSSHServerValidator;
    private readonly IValidator<UpdateSSHServerPasswordRequest> _validatorUpdateSSHServerPasswordValidator;
    private readonly IValidator<UpdateSSHServerRequest> _validatorUpdateSSHServerValidator;

    public SSHServerController(IMediator mediator,
        IValidator<UpdateSSHServerRequest> validatorUpdateSshServerValidator,
        IValidator<CreateSSHServerRequest> validatorCreateSshServerValidator,
        IValidator<UpdateSSHServerPasswordRequest> validatorUpdateSshServerPasswordValidator) : base(mediator)
    {
        _validatorUpdateSSHServerValidator = validatorUpdateSshServerValidator;
        _validatorCreateSSHServerValidator = validatorCreateSshServerValidator;
        _validatorUpdateSSHServerPasswordValidator = validatorUpdateSshServerPasswordValidator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateSSHServerResponse>> Create([FromBody] CreateSSHServerRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validatorCreateSSHServerValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            return BadRequest(errorMessages);
        }

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadSSHServerResponse>> Read(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(new ReadSSHServerRequest(result), cancellationToken);
        if (response.Id is null) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadSSHServerResponse>> ReadAll(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new ReadAllSSHServerRequest(), cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateSSHServerResponse>> Update([FromBody] UpdateSSHServerRequest request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var result)) return BadRequest("Id is invalid");
        var validationResult = await _validatorUpdateSSHServerValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            return BadRequest(errorMessages);
        }

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("password")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateSSHServerResponse>> Update([FromBody] UpdateSSHServerPasswordRequest request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var result)) return BadRequest("Id is invalid");

        var validationResult =
            await _validatorUpdateSSHServerPasswordValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            return BadRequest(errorMessages);
        }

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteSSHServerResponse>> Delete(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(new DeleteSSHServerRequest(result), cancellationToken);
        return Ok(response);
    }
}