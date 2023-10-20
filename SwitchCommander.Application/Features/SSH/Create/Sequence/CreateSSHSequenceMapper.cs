using Riok.Mapperly.Abstractions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Create.Sequence;

[Mapper]
public partial class CreateSSHSequenceMapper
{
    public partial SSHSequence FromRequest(CreateSSHSequenceRequest dto);
    public partial SSHSequence FromDto(CreateSSHSequenceDto dto);
}