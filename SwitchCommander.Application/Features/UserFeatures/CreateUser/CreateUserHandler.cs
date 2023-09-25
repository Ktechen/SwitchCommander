using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.UserFeatures.CreateUser;

public sealed record CreateUserRequest(string? Email, string? Name, string? NameTwo) : IRequest<CreateUserResponse>;

public sealed record CreateUserResponse(Guid id, string? Email, string? Name, string? NameTwo);

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
        var user = _mapper.ToUserDto(request);
        await _userRepository.AddAsync(user, cancellationToken);
        return _mapper.ToCreateUserResponse(user);
    }
}