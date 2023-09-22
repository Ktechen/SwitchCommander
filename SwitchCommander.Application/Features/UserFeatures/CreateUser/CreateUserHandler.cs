using MediatR;
using SwitchCommander.Application.Features.UserFeatures.CreateUser.Records;
using SwitchCommander.Application.Repositories;

namespace SwitchCommander.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly CreateUserMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository, CreateUserMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.ToUserDto(request);
        await _userRepository.AddAsync(user, cancellationToken);
        return _mapper.ToCreateUserResponse(user);
    }
}