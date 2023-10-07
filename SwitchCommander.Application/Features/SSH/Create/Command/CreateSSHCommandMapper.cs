﻿using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Create.Command;

[Mapper]
public partial class CreateSSHCommandMapper
{
    public partial SSHCommand FromRequest(CreateSSHCommandRequest dto);
    public partial CreateSSHCommandResponse ToResponse(SSHCommand dto);
}