using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories.Features.SSH;

public interface ISSHServerRepository : IBaseRepository<SSHServer>
{
    public Task<string> ExecuteCommand(SSHServer server, SSHCommand sshCommand);
}