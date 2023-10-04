using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public class SSHCommand : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Command { get; set; }
}