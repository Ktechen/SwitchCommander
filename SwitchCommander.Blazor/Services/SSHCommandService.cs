namespace SwitchCommander.Blazor.Services;

public class SSHCommandService : ISSHCommandClient
{
    public Task<CreateSSHCommandResponse> CreateAsync(CreateSSHCommandRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CreateSSHCommandResponse> CreateAsync(CreateSSHCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateSSHCommandResponse> UpdateAsync(UpdateSSHCommandRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateSSHCommandResponse> UpdateAsync(UpdateSSHCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ReadSSHCommandResponse> ReadAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ReadSSHCommandResponse> ReadAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteSSHCommandResponse> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteSSHCommandResponse> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}