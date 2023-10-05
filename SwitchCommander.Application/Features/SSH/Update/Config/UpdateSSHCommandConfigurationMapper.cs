using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Update.Config;

[Mapper]
public partial class UpdateSSHCommandConfigurationMapper
{
    public partial SShCommandConfiguration FromRequest(UpdateSSHCommandConfigurationRequest dto);
}