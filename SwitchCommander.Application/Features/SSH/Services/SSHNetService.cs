using Renci.SshNet;

namespace SwitchCommander.Application.Features.SSH.Services;

public class SSHNetService 
{

    public async Task OpenConnection(string host, string username, string password)
    {
        using var client = new SshClient(host, username, password);
        client.Connect();
        var result = client.RunCommand("ls");
        var resultAsString = result.Result;
        
        await Task.CompletedTask;
    }

    public Task CloseConnection()
    {
        throw new NotImplementedException();
    }
}