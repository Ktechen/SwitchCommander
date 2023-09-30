using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwitchCommander.Application.Features.SSHManagement.CreateSSHCommand;
using SwitchCommander.Application.Features.UserManagement.CreateUser;

namespace SwitchCommander.WebAPI.Controllers;

public class SSHController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateUserRequest> _validator;

    public SSHController(IMediator mediator, IValidator<CreateUserRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }


    public Task<ActionResult<CreateSSHCommandResponse>> Create(CreateSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<CreateSSHCommandResponse>> GetById(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}