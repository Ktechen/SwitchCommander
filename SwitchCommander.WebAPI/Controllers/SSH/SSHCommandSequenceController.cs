using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSH.Create.Sequence;

namespace SwitchCommander.WebAPI.Controllers.SSH;

public class SSHCommandSequenceController : BaseController
{
    private readonly IValidator<CreateSSHSequenceRequest> _sequenceValidator;
    private readonly ILogger<SSHCommandSequenceController> _logger;

    public SSHCommandSequenceController(IMediator mediator, IValidator<CreateSSHSequenceRequest> sequenceValidator,
        ILogger<SSHCommandSequenceController> logger) : base(mediator)
    {
        _sequenceValidator = sequenceValidator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] CreateSSHSequenceRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return BadRequest("List have not valid guid inside");
        }

        var validationResult = await _sequenceValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            _logger.LogError(errorMessages);
            return BadRequest(errorMessages);
        }


        await _mediator.Send(request, cancellationToken);

        return Ok();
    }
}