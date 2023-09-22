using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.UserFeatures.CreateUser.Records;
using SwitchCommander.Application.Features.UserFeatures.ReadUser.Records;

namespace SwitchCommander.WebAPI.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateUserRequest> _validator;

    public UserController(IMediator mediator, IValidator<CreateUserRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CreateUserResponse>> GetById(string id,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(new ReadUserRequest(result), cancellationToken);
        if (response.Id.Equals(Guid.Empty)) return NotFound("User Not found");
        return Ok(response);
    }
}