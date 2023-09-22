using Riok.Mapperly.Abstractions;
using SwitchCommander.Application.Features.UserFeatures.ReadUser.Records;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.UserFeatures.ReadUser;

[Mapper]
public partial class ReadUserMapper
{
    public partial User ToUserDto(ReadUserRequest dto);
    public partial User ToUserDto(ReadUserResponse dto);
    public partial ReadUserResponse ToReadUserResponse(User dto);
}