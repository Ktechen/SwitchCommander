using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories.Features;

public interface ISSHCommandConfigurationRepository : IBaseRepository<SShCommandConfiguration>
{
    Task<SShCommandConfiguration?> GetDefaultConfig(CancellationToken cancellationToken);
}