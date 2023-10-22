using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features.SSH;

public class SshCommandConfigurationMongoRepository : BaseMongoRepository<SShCommandConfiguration>,
    ISshCommandConfigurationMongoRepository
{
    public SshCommandConfigurationMongoRepository(MongoDbSshContext sshContext, IMediator mediator) : base(
        sshContext.SSHCommandConfigurationCollection, mediator)
    {
    }

    public async Task<SShCommandConfiguration?> GetDefaultConfig(CancellationToken cancellationToken)
    {
        var result = await FindAsync(x => true, cancellationToken);
        return result.FirstOrDefault();
    }
}