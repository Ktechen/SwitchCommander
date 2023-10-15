using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features.SSH;

public class SshCommandMongoRepository : BaseMongoRepository<SSHCommand>, ISshCommandMongoRepository
{
    public SshCommandMongoRepository(MongoDbContext context, IMediator mediator) : base(context.SSHCommandCollection,
        mediator)
    {
    }
}