using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.SSH.Update.Command;

[Mapper]
public partial class UpdateSSHCommandMapper
{
    public partial SSHCommand FromRequest(UpdateSSHCommandRequest dto);
}