using Riok.Mapperly.Abstractions;

namespace SwitchCommander.Application.Features.User.CreateUser;

[Mapper]
public partial class CreateUserMapper
{
    public partial Domain.Dtos.User FromRequest(CreateUserRequest dto);
    public partial CreateUserResponse ToResponse(Domain.Dtos.User dto);
}