using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Read.Config;

[Mapper]
public partial class ReadSSHConfigurationMapper
{
    public partial IEnumerable<ReadSSHCommandConfigurationResponse> ToResponse(
        IEnumerable<SShCommandConfiguration> sShCommandConfigurations);
}