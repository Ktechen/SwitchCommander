using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.UserFeatures.ReadUser;

[Mapper]
public partial class ReadUserMapper
{
    public partial User FromRequest(ReadUserRequest dto);
    public partial ReadUserResponse ToResponse(User dto);
}