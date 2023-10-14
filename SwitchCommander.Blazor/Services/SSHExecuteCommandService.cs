namespace SwitchCommander.Blazor.Services;

public class SSHExecuteCommandService : ISSHExecuteCommandClient
{
    private ISSHExecuteCommandClient _isshExecuteCommandClientImplementation;

    public SSHExecuteCommandService(ISSHExecuteCommandClient isshExecuteCommandClientImplementation)
    {
        _isshExecuteCommandClientImplementation = isshExecuteCommandClientImplementation;
    }

    public Task<ExecuteSSHCommandResponse> ExecuteAsync(ExecuteSSHCommandRequest request)
    {
        return _isshExecuteCommandClientImplementation.ExecuteAsync(request);
    }

    public Task<ExecuteSSHCommandResponse> ExecuteAsync(ExecuteSSHCommandRequest request, CancellationToken cancellationToken)
    {
        return _isshExecuteCommandClientImplementation.ExecuteAsync(request, cancellationToken);
    }
}