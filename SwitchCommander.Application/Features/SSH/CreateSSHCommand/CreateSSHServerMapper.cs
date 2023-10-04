using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.CreateSSHCommand;

[Mapper]
public partial class CreateSSHServerMapper
{
    public partial SSHServer FromRequest(CreateSSHServerRequest dto);
    public partial CreateSSHServerResponse ToResponse(SSHServer dto);
}