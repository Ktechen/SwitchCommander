using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Server;

[Mapper]
public partial class ReadSSHServerMapper
{
    public partial ReadSSHServerResponse ToResponse(SSHServer dto);
    public partial IEnumerable<ReadAllSSHServerResponse> ToResponse(IEnumerable<SSHServer> dtos);
}