using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.UserManagement.ReadUser;

public sealed record ReadUserRequest(Guid Id) : IRequest<ReadUserResponse>;

public sealed record ReadUserResponse(Guid Id, string Name, string Email, string? NameTwo);

public sealed class ReadUserHandler : IRequestHandler<ReadUserRequest, ReadUserResponse>
{
    private readonly ILogger<ReadUserHandler> _logger;
    private readonly ReadUserMapper _mapper;
    private readonly IUserRepository _userRepository;

    public ReadUserHandler(ReadUserMapper mapper, IUserRepository userRepository, ILogger<ReadUserHandler> logger)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ReadUserResponse> Handle(ReadUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.FromRequest(request);
        var result = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        _logger.LogInformation("Read user from Db " + user.Email);
        return result is null
            ? new ReadUserResponse(Guid.Empty, string.Empty, string.Empty, string.Empty)
            : _mapper.ToResponse(result);
    }
}