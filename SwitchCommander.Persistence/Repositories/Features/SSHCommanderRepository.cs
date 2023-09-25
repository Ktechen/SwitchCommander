using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features;

public class SSHCommanderRepository : BaseRepository<SSHCommand>, ISSHCommanderRepository
{
    public SSHCommanderRepository(MongoDbContext context) : base(context.SSHCommandCollection)
    {
    }
}