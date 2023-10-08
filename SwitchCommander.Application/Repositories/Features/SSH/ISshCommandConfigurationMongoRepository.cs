using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories.Features.SSH;

public interface ISshCommandConfigurationMongoRepository : IBaseMongoRepository<SShCommandConfiguration>
{
    Task<SShCommandConfiguration?> GetDefaultConfig(CancellationToken cancellationToken);
}