using Riok.Mapperly.Abstractions;
using SwitchCommander.Application.Features.UserFeatures.CreateUser.Records;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.UserFeatures.CreateUser;

[Mapper]
public partial class CreateUserMapper
{
    public partial UserDto ToUserDto(CreateUserRequest dto);
    public partial UserDto ToUserDto(CreateUserResponse dto);
    public partial CreateUserResponse ToCreateUserResponse(UserDto dto);
}