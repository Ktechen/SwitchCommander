using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.UserFeatures.CreateUser.Records;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

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
    public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        /*var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
            return BadRequest(errorMessages);
        }*/
        
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}