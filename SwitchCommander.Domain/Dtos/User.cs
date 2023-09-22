using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public sealed class User : BaseEntity
{

    public string? Email { get; set; }
    public string? Name { get; set; }

    public override string ToString()
    {
        return base.ToString();
    }
}