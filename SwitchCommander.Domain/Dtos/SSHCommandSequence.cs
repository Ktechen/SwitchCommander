using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public class SSHCommandSequence : BaseEntity
{
    public int StepId { get; set; }
    public string SequenceName { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public bool IsCompleted { get; set; }
    public IEnumerator<SSHCommand> SshCommands { get; set; }
}