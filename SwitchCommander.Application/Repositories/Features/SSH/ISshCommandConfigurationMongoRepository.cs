using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Repositories.Features.SSH;

public interface ISshCommandConfigurationMongoRepository : IBaseMongoRepository<SShCommandConfiguration>
{
    Task<SShCommandConfiguration?> GetDefaultConfig(CancellationToken cancellationToken);
}