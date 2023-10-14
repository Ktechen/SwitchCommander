namespace SwitchCommander.Blazor.Services;

public class SSHServerService : ISSHServerClient
{
    private ISSHServerClient _isshServerClientImplementation;

    public SSHServerService(ISSHServerClient isshServerClientImplementation)
    {
        _isshServerClientImplementation = isshServerClientImplementation;
    }

    public Task<CreateSSHServerResponse> CreateAsync(CreateSSHServerRequest request)
    {
        return _isshServerClientImplementation.CreateAsync(request);
    }

    public Task<CreateSSHServerResponse> CreateAsync(CreateSSHServerRequest request, CancellationToken cancellationToken)
    {
        return _isshServerClientImplementation.CreateAsync(request, cancellationToken);
    }

    public Task<UpdateSSHServerResponse> UpdateAsync(UpdateSSHServerRequest request)
    {
        return _isshServerClientImplementation.UpdateAsync(request);
    }

    public Task<UpdateSSHServerResponse> UpdateAsync(UpdateSSHServerRequest request, CancellationToken cancellationToken)
    {
        return _isshServerClientImplementation.UpdateAsync(request, cancellationToken);
    }

    public Task<ReadSSHServerResponse> ReadAsync(string id)
    {
        return _isshServerClientImplementation.ReadAsync(id);
    }

    public Task<ReadSSHServerResponse> ReadAsync(string id, CancellationToken cancellationToken)
    {
        return _isshServerClientImplementation.ReadAsync(id, cancellationToken);
    }

    public Task<DeleteSSHServerResponse> DeleteAsync(string id)
    {
        return _isshServerClientImplementation.DeleteAsync(id);
    }

    public Task<DeleteSSHServerResponse> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _isshServerClientImplementation.DeleteAsync(id, cancellationToken);
    }

    public Task<UpdateSSHServerResponse> Update2Async(UpdateSSHServerPasswordRequest request)
    {
        return _isshServerClientImplementation.Update2Async(request);
    }

    public Task<UpdateSSHServerResponse> Update2Async(UpdateSSHServerPasswordRequest request, CancellationToken cancellationToken)
    {
        return _isshServerClientImplementation.Update2Async(request, cancellationToken);
    }
}