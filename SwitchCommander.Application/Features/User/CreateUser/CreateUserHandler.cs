using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.User.CreateUser;

public sealed record CreateUserRequest(string? Email, string? Username) : IRequest<CreateUserResponse>;

public sealed record CreateUserResponse(Guid? Id, string? Email, string? Username);

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly CreateUserMapper _mapper;
    private readonly IUserMongoRepository _userMongoRepository;

    public CreateUserHandler(IUserMongoRepository userMongoRepository, CreateUserMapper mapper,
        ILogger<CreateUserHandler> logger)
    {
        _userMongoRepository = userMongoRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.FromRequest(request);
        user.QueueDomainEvent(new CreateUserDomainEvent(user));
        await _userMongoRepository.AddAsync(user, cancellationToken);
        return _mapper.ToResponse(user);
    }
}