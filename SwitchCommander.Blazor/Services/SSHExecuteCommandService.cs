namespace SwitchCommander.Blazor.Services;

public class SSHExecuteCommandService : ISSHExecuteCommandClient
{
    public Task<ExecuteSSHCommandResponse> ExecuteAsync(ExecuteSSHCommandRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ExecuteSSHCommandResponse> ExecuteAsync(ExecuteSSHCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}