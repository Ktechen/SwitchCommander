using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Update.Server;

[Mapper]
public partial class UpdateSSHServerMapper
{
    public partial SSHServer FromRequest(UpdateSSHServerRequest dto);
}