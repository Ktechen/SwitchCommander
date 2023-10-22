using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.SSH.Create.Server;

[Mapper]
public partial class CreateSSHServerMapper
{
    public partial SSHServer FromRequest(CreateSSHServerRequest dto);
    public partial CreateSSHServerResponse ToResponse(SSHServer dto);
}