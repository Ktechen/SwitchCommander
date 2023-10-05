﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

namespace SwitchCommander.WebAPI.Controllers;

public class SSHController : BaseController
{
    private readonly IValidator<UpdateSSHCommandConfigurationRequest> _validatorUpdateSshCommandConfigurationRequest;
    
    public SSHController(
        IMediator mediator, 
        IValidator<UpdateSSHCommandConfigurationRequest> validatorUpdateSshCommandConfigurationRequest) : base(mediator)
    {
        _validatorUpdateSshCommandConfigurationRequest = validatorUpdateSshCommandConfigurationRequest;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<UpdateSSHCommandConfigurationResponse>> UpdateSSHCommandConfiguration(UpdateSSHCommandConfigurationRequest request, CancellationToken cancellationToken)
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
}