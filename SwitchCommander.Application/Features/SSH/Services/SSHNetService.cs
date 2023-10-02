using Renci.SshNet;
using SwitchCommander.Application.Common.Exceptions;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Services;

public class SSHNetService : ISSHNetService
{
    public async Task<string> RunCommand(SSHServer server, SSHCommand sshCommand)
    {
        using var client = new SshClient(server.Hostname, server.Username, server.Password);
        using var runCommand = client.RunCommand(sshCommand.Command);
        client.Disconnect();
        if (runCommand.Error.Length != 0) throw new SSHNetException(runCommand.Error);
        return await Task.FromResult(runCommand.Result);
    }
}