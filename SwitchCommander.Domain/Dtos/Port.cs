using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public class Port : BaseEntity
{
    public int Count { get; set; }
    public string Type { get; set; }
}
