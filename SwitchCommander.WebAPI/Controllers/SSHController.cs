using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.CreateSSHCommand;
using SwitchCommander.Application.Features.SSH.UpdateSSHCommand;
using SwitchCommander.Application.Features.User.CreateUser;

namespace SwitchCommander.WebAPI.Controllers;

public class SSHController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IValidator<UpdateSSHCommandConfigurationRequest> _validatorConfiguration;

    public SSHController(IMediator mediator, IValidator<UpdateSSHCommandConfigurationRequest> validatorConfiguration)
    {
        _mediator = mediator;
        _validatorConfiguration = validatorConfiguration;
    }


    public Task<ActionResult<CreateSSHCommandResponse>> Create(CreateSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateUserResponse>> UpdateConfiguration(
        UpdateSSHCommandConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validatorConfiguration.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            return BadRequest(errorMessages);
        }

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}