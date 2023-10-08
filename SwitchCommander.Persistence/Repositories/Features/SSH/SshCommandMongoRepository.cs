using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features.SSH;


public class SshCommandMongoRepository : BaseMongoRepository<SSHCommand>, ISshCommandMongoRepository
{
    public SshCommandMongoRepository(MongoDbContext context, IMediator mediator) : base(context.SSHCommandCollection,
        mediator)
    {
    }
}