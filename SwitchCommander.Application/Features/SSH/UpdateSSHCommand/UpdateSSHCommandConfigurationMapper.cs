﻿using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

[Mapper]
public partial class UpdateSSHCommandConfigurationMapper
{
    public partial SShCommandConfiguration FromRequest(UpdateSSHCommandConfigurationRequest dto);
}