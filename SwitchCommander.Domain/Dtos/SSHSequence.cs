using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public class SSHSequence : BaseEntity
{
    public string SequenceName { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public DateTimeOffset LastExecute { get; set; }
    public List<SSHCommand> SshCommands { get; set; }
}