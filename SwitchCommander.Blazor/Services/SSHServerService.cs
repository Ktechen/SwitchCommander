namespace SwitchCommander.Blazor.Services;

public class SSHServerService : ISSHServerClient
{
    public Task<CreateSSHServerResponse> CreateAsync(CreateSSHServerRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CreateSSHServerResponse> CreateAsync(CreateSSHServerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateSSHServerResponse> UpdateAsync(UpdateSSHServerRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateSSHServerResponse> UpdateAsync(UpdateSSHServerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ReadSSHServerResponse> ReadAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ReadSSHServerResponse> ReadAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteSSHServerResponse> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteSSHServerResponse> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateSSHServerResponse> Update2Async(UpdateSSHServerPasswordRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateSSHServerResponse> Update2Async(UpdateSSHServerPasswordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}