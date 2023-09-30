using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.UserManagement.ReadUser;

[Mapper]
public partial class ReadUserMapper
{
    public partial User FromRequest(ReadUserRequest dto);
    public partial ReadUserResponse ToResponse(User dto);
}