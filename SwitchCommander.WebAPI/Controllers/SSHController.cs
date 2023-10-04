using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH;
using SwitchCommander.Application.Features.SSH.CreateSSHCommand;
using SwitchCommander.Application.Features.SSH.DeleteSSHCommand;
using SwitchCommander.Application.Features.SSH.ReadSSHCommand;
using SwitchCommander.Application.Features.SSH.UpdateSSHCommand;
using SwitchCommander.Application.Features.User.CreateUser;

namespace SwitchCommander.WebAPI.Controllers;

public class SSHController : BaseController, ISSHController
{
    private readonly IMediator _mediator;
    private readonly IValidator<UpdateSSHCommandConfigurationRequest> _validatorUpdateSshCommandConfigurationRequest;
    private readonly IValidator<CreateSSHServerRequest> _validatorCreateSSHServerRequest;
    
    public SSHController(IMediator mediator, IValidator<UpdateSSHCommandConfigurationRequest> validatorUpdateSshCommandConfigurationRequest, IValidator<CreateSSHServerRequest> validatorCreateSshServerRequest)
    {
        _mediator = mediator;
        _validatorUpdateSshCommandConfigurationRequest = validatorUpdateSshCommandConfigurationRequest;
        _validatorCreateSSHServerRequest = validatorCreateSshServerRequest;
    }
    
    
    [HttpPost("server")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateSSHServerResponse>> Create(CreateSSHServerRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validatorCreateSSHServerRequest.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            return BadRequest(errorMessages);
        }

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateUserResponse>> UpdateConfiguration(
        UpdateSSHCommandConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validatorUpdateSshCommandConfigurationRequest.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            return BadRequest(errorMessages);
        }

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    public Task<ActionResult<CreateSSHServerResponse>> CreateServer(CreateSSHServerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<ReadSSHServerResponse>> ReadServer(ReadSSHServerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<DeleteSSHServerResponse>> DeleteServer(DeleteSSHServerResponse request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<UpdateSSHServerResponse>> UpdateServer(CreateSSHServerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<CreateSSHCommandResponse>> CreateServer(CreateSSHCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<ReadSSHCommandResponse>> ReadServer(ReadSSHCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<DeleteSSHCommandResponse>> DeleteServer(DeleteSSHCommandResponse request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<UpdateSSHCommandResponse>> UpdateServer(UpdateSSHCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<ExecuteSSHCommandResponse>> ExecuteSSHCommand(ExecuteSSHCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<UpdateSSHCommandConfigurationResponse>> UpdateSSHCommandConfiguration(UpdateSSHCommandConfigurationRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}