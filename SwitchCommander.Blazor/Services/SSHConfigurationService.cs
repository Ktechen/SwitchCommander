namespace SwitchCommander.Blazor.Services;

public class SSHConfigurationService : ISSHConfigurationClient
{
    public Task<ICollection<UpdateSSHCommandConfigurationResponse>> ReadAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<UpdateSSHCommandConfigurationResponse>> ReadAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateSSHCommandConfigurationResponse> UpdateSSHCommandConfigurationAsync(UpdateSSHCommandConfigurationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateSSHCommandConfigurationResponse> UpdateSSHCommandConfigurationAsync(UpdateSSHCommandConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}