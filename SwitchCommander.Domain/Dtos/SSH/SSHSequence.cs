using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos.SSH;

public class SSHSequence : BaseEntity
{
    public string SequenceName { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public DateTimeOffset LastExecute { get; set; }
    public IEnumerable<SSHCommand> SshCommands { get; set; }
}