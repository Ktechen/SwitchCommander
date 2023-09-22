using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features;

public class SwitchRepository : BaseRepository<Switch>, ISwitchRepository
{
    public SwitchRepository(MongoDbContext context) : base(context.SwitchCollection)
    {
    }
}