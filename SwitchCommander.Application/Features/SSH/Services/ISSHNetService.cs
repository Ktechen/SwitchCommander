using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Services;

public interface ISSHNetService
{
    public Task<string> RunCommand(SSHServer server, SSHCommand sshCommand);
}