namespace SwitchCommander.Blazor.Services;

public class SSHCommandService : ISSHCommandClient
{
    private ISSHCommandClient _isshCommandClientImplementation;

    public SSHCommandService(ISSHCommandClient isshCommandClientImplementation)
    {
        _isshCommandClientImplementation = isshCommandClientImplementation;
    }

    public Task<CreateSSHCommandResponse> CreateAsync(CreateSSHCommandRequest request)
    {
        return _isshCommandClientImplementation.CreateAsync(request);
    }

    public Task<CreateSSHCommandResponse> CreateAsync(CreateSSHCommandRequest request, CancellationToken cancellationToken)
    {
        return _isshCommandClientImplementation.CreateAsync(request, cancellationToken);
    }

    public Task<UpdateSSHCommandResponse> UpdateAsync(UpdateSSHCommandRequest request)
    {
        return _isshCommandClientImplementation.UpdateAsync(request);
    }

    public Task<UpdateSSHCommandResponse> UpdateAsync(UpdateSSHCommandRequest request, CancellationToken cancellationToken)
    {
        return _isshCommandClientImplementation.UpdateAsync(request, cancellationToken);
    }

    public Task<ReadSSHCommandResponse> ReadAsync(string id)
    {
        return _isshCommandClientImplementation.ReadAsync(id);
    }

    public Task<ReadSSHCommandResponse> ReadAsync(string id, CancellationToken cancellationToken)
    {
        return _isshCommandClientImplementation.ReadAsync(id, cancellationToken);
    }

    public Task<DeleteSSHCommandResponse> DeleteAsync(string id)
    {
        return _isshCommandClientImplementation.DeleteAsync(id);
    }

    public Task<DeleteSSHCommandResponse> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _isshCommandClientImplementation.DeleteAsync(id, cancellationToken);
    }
}