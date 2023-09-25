using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.UserFeatures.CreateUser;

[Mapper]
public partial class CreateUserMapper
{
    public partial User ToUserDto(CreateUserRequest dto);
    public partial User ToUserDto(CreateUserResponse dto);
    public partial CreateUserResponse ToCreateUserResponse(User dto);
}