using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.SSH.Create.Sequence;

[Mapper]
public partial class CreateSSHSequenceMapper
{
    public partial SSHSequence FromRequest(CreateSSHSequenceRequest dto);
    public partial SSHSequence FromDto(CreateSSHSequenceDto dto);
}