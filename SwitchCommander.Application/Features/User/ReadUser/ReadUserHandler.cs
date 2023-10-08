using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Common.Exceptions;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.User.ReadUser;

public sealed record ReadUserRequest(Guid Id) : IRequest<ReadUserResponse>;

public sealed record ReadUserResponse(Guid Id, string Username, string Email);

public sealed class ReadUserHandler : IRequestHandler<ReadUserRequest, ReadUserResponse>
{
    private readonly ILogger<ReadUserHandler> _logger;
    private readonly ReadUserMapper _mapper;
    private readonly IUserMongoRepository _userMongoRepository;

    public ReadUserHandler(ReadUserMapper mapper, IUserMongoRepository userMongoRepository, ILogger<ReadUserHandler> logger)
    {
        _mapper = mapper;
        _userMongoRepository = userMongoRepository;
        _logger = logger;
    }

    public async Task<ReadUserResponse> Handle(ReadUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.FromRequest(request);
        var result = await _userMongoRepository.FindByIdAsync(request.Id, cancellationToken);
        _logger.LogInformation("Read user from Db " + user.Email);
        return result is null
            ? throw new BadRequestException("User not found")
            : _mapper.ToResponse(result);
    }
}