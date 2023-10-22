using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos.SSH;

public class SSHServer : BaseEntity
{
    public string Hostname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}