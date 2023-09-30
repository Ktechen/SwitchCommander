using Riok.Mapperly.Abstractions;

namespace SwitchCommander.Application.Features.User.ReadUser;

[Mapper]
public partial class ReadUserMapper
{
    public partial Domain.Dtos.User FromRequest(ReadUserRequest dto);
    public partial ReadUserResponse ToResponse(Domain.Dtos.User dto);
}