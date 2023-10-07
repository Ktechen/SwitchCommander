using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.Update.Config;

namespace SwitchCommander.WebAPI.Controllers.SSH;

public class SSHConfigController : BaseController
{
    private readonly IValidator<UpdateSSHCommandConfigurationRequest> _validatorUpdateSshCommandConfigurationRequest;
    
    public SSHConfigController(IMediator mediator, IValidator<UpdateSSHCommandConfigurationRequest> validatorUpdateSshCommandConfigurationRequest) : base(mediator)
    {
        _validatorUpdateSshCommandConfigurationRequest = validatorUpdateSshCommandConfigurationRequest;
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<UpdateSSHCommandConfigurationResponse>> UpdateSSHCommandConfiguration([FromBody] UpdateSSHCommandConfigurationRequest request, CancellationToken cancellationToken)
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