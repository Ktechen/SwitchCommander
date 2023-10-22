using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public class SShCommandConfiguration : BaseEntity
{
    public int CommandMinimumLength { get; set; } = 2;
    public int CommandMaximumLength { get; set; } = 200;
    public int DescriptionMinimumLength { get; set; } = 3;
    public int DescriptionMaximumLength { get; set; } = 1000;
}