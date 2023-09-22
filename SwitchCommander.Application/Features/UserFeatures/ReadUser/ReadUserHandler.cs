using MediatR;
using SwitchCommander.Application.Features.UserFeatures.ReadUser.Records;
using SwitchCommander.Application.Repositories;

namespace SwitchCommander.Application.Features.UserFeatures.ReadUser;

public sealed class ReadUserHandler : IRequestHandler<ReadUserRequest, ReadUserResponse>
{
    private readonly ReadUserMapper _mapper;
    private readonly IUserRepository _userRepository;

    public ReadUserHandler(ReadUserMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ReadUserResponse> Handle(ReadUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.ToUserDto(request);
        var result = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        return result is null ? 
            new ReadUserResponse(Guid.Empty, string.Empty, string.Empty) : 
            _mapper.ToReadUserResponse(result);
    }
}