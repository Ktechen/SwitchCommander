using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.Create;
using SwitchCommander.Application.Features.SSH.Create.Server;
using SwitchCommander.Application.Features.SSH.Delete;
using SwitchCommander.Application.Features.SSH.Delete.Server;
using SwitchCommander.Application.Features.SSH.Read;
using SwitchCommander.Application.Features.SSH.Read.Server;
using SwitchCommander.Application.Features.SSH.Update.Server;

namespace SwitchCommander.WebAPI.Controllers.User;

public class SSHServerController : BaseController
{
    private readonly IValidator<UpdateSSHServerRequest> _validatorUpdateSSHServerValidator;
    private readonly IValidator<CreateSSHServerRequest> _validatorCreateSSHServerValidator;

    public SSHServerController(IMediator mediator, IValidator<UpdateSSHServerRequest> validatorUpdateSshServerValidator, IValidator<CreateSSHServerRequest> validatorCreateSshServerValidator) : base(mediator)
    {
        _validatorUpdateSSHServerValidator = validatorUpdateSshServerValidator;
        _validatorCreateSSHServerValidator = validatorCreateSshServerValidator;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateSSHServerResponse>> Create([FromBody]CreateSSHServerRequest request, CancellationToken cancellationToken)
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadSSHServerResponse>> Read(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(new ReadSSHServerRequest(result), cancellationToken);
        if (response.Id is null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateSSHServerResponse>> Update([FromBody] UpdateSSHServerRequest request, CancellationToken cancellationToken)
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
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteSSHServerResponse>> Delete(string id, CancellationToken cancellationToken)
    {        
        if (!Guid.TryParse(id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(new DeleteSSHServerRequest(result), cancellationToken);
        return Ok(response);
    }
    
}