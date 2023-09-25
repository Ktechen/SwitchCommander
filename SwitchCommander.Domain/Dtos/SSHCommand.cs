using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public class SSHCommand : BaseEntity
{
    public string? Hash { get; set; }
    public string? Command { get; set; }
}