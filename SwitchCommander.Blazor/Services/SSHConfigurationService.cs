namespace SwitchCommander.Blazor.Services;

public class SSHConfigurationService : ISSHConfigurationClient
{
    private ISSHConfigurationClient _isshConfigurationClientImplementation;

    public SSHConfigurationService(ISSHConfigurationClient isshConfigurationClientImplementation)
    {
        _isshConfigurationClientImplementation = isshConfigurationClientImplementation;
    }

    public Task<ICollection<UpdateSSHCommandConfigurationResponse>> ReadAllAsync()
    {
        return _isshConfigurationClientImplementation.ReadAllAsync();
    }

    public Task<ICollection<UpdateSSHCommandConfigurationResponse>> ReadAllAsync(CancellationToken cancellationToken)
    {
        return _isshConfigurationClientImplementation.ReadAllAsync(cancellationToken);
    }

    public Task<UpdateSSHCommandConfigurationResponse> UpdateSSHCommandConfigurationAsync(UpdateSSHCommandConfigurationRequest request)
    {
        return _isshConfigurationClientImplementation.UpdateSSHCommandConfigurationAsync(request);
    }

    public Task<UpdateSSHCommandConfigurationResponse> UpdateSSHCommandConfigurationAsync(UpdateSSHCommandConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        return _isshConfigurationClientImplementation.UpdateSSHCommandConfigurationAsync(request, cancellationToken);
    }
}