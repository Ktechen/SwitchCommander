using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories.Features.SSH;

public interface ISshCommandMongoRepository : IBaseMongoRepository<SSHCommand>
{
}