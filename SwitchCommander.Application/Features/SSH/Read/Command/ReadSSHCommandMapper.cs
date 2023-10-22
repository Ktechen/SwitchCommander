using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Command;

[Mapper]
public partial class ReadSSHCommandMapper
{
    public partial ReadSSHCommandResponse ToResponse(SSHCommand dto);
    public partial IEnumerable<ReadAllSSHCommandResponse> ToResponse(IEnumerable<SSHCommand> dto);
}