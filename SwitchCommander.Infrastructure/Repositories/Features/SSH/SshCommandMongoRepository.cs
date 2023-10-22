using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features.SSH;

public class SshCommandMongoRepository : BaseMongoRepository<SSHCommand>, ISshCommandMongoRepository
{
    public SshCommandMongoRepository(MongoDbSshContext sshContext, IMediator mediator) : base(sshContext.SSHCommandCollection,
        mediator)
    {
    }
}