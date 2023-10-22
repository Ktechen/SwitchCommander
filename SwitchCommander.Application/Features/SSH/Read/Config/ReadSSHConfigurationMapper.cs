using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Config;

[Mapper]
public partial class ReadSSHConfigurationMapper
{
    public partial IEnumerable<ReadSSHCommandConfigurationResponse> ToResponse(
        IEnumerable<SShCommandConfiguration> sShCommandConfigurations);
}