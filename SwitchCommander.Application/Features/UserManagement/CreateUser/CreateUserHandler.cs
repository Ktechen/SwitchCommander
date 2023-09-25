using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Features.UserManagement.DomainEvents;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Common;

namespace SwitchCommander.Application.Features.UserManagement.CreateUser;

public sealed record CreateUserRequest(string? Email, string? Name, string? NameTwo) : IRequest<CreateUserResponse>;

public sealed record CreateUserResponse(Guid? id, string? Email, string? Name, string? NameTwo);

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly CreateUserMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository, CreateUserMapper mapper, ILogger<CreateUserHandler> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.FromRequest(request);
        user.QueueDomainEvent(new CreateUserDomainEvent(user));
        await _userRepository.AddAsync(user, cancellationToken);
        return _mapper.ToResponse(user);
    }
}