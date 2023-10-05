using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.User.CreateUser;
using SwitchCommander.Application.Features.User.ReadUser;

namespace SwitchCommander.WebAPI.Controllers;

public class UserController : BaseController
{
    private readonly IValidator<CreateUserRequest> _validator;

    public UserController(IMediator mediator, IValidator<CreateUserRequest> validator) : base(mediator)
    {
        _validator = validator;
    }

    /// <summary>
    ///     Create a user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     <see cref="CreateUserResponse" />
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateUserResponse>> Create(
        CreateUserRequest request,
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

    /// <summary>
    ///     Read user by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     <see cref="ReadUserResponse" />
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadUserResponse>> GetById(
        string id,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var result)) return BadRequest("Id is invalid");
        var response = await _mediator.Send(new ReadUserRequest(result), cancellationToken);
        if (response.Id.Equals(Guid.Empty)) return NotFound("User Not found");
        return Ok(response);
    }
}