using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.SSH.Update.Config;

[Mapper]
public partial class UpdateSSHCommandConfigurationMapper
{
    public partial SShCommandConfiguration FromRequest(UpdateSSHCommandConfigurationRequest dto);
}