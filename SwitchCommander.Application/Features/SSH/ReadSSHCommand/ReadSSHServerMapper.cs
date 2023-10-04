﻿using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.ReadSSHCommand;

[Mapper]
public partial class ReadSSHServerMapper 
{
    public partial ReadSSHServerResponse ToResponse(SSHServer dto);
}