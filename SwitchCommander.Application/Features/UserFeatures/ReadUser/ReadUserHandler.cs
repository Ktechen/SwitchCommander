using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Features.UserFeatures.ReadUser.Records;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.UserFeatures.ReadUser;

public sealed class ReadUserHandler : IRequestHandler<ReadUserRequest, ReadUserResponse>
{
    private readonly ReadUserMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ReadUserHandler> _logger;

    public ReadUserHandler(ReadUserMapper mapper, IUserRepository userRepository, ILogger<ReadUserHandler> logger)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ReadUserResponse> Handle(ReadUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.ToUserDto(request);
        var result = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        _logger.LogInformation("Read user from Db " + user.Email);
        return result is null
            ? new ReadUserResponse(Guid.Empty, string.Empty, string.Empty, string.Empty)
            : _mapper.ToReadUserResponse(result);
    }
}