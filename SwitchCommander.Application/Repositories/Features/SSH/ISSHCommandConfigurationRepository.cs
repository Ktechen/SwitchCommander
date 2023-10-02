using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories.Features.SSH;

public interface ISSHCommandConfigurationRepository : IBaseRepository<SShCommandConfiguration>
{
    Task<SShCommandConfiguration?> GetDefaultConfig(CancellationToken cancellationToken);
}