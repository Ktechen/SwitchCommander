using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Repositories.Features.SSH;

public interface ISshServerMongoRepository : IBaseMongoRepository<SSHServer>
{
}