using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.ReadSSHCommand;

[Mapper]
public partial class ReadSSHCommandMapper
{
    public partial ReadSSHCommandResponse ToResponse(SSHCommand dto);
}