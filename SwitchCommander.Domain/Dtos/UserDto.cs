using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public sealed class UserDto : BaseEntity
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}