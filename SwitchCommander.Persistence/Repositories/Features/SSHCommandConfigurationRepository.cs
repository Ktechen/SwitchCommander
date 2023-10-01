using MediatR;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features;

public class SSHCommandConfigurationRepository : BaseRepository<SShCommandConfiguration>,
    ISSHCommandConfigurationRepository
{
    public SSHCommandConfigurationRepository(MongoDbContext context, IMediator mediator) : base(
        context.SSHCommandConfigurationCollection, mediator)
    {
    }

    public async Task<SShCommandConfiguration?> GetDefaultConfig(CancellationToken cancellationToken)
    {
        var result = await FindAsync(x => true, cancellationToken);
        return result.FirstOrDefault();
    }
}