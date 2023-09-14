using MediatR;
using SwitchCommander.Application.Repositories;

namespace SwitchCommander.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly CreateUserMapper _mapper;

    public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, CreateUserMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.ToUserDto(request);
        await _userRepository.Create(user);
        await _unitOfWork.Save(cancellationToken);
        return _mapper.ToCreateUserResponse(user);
    }
}