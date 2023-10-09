using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features.SSH;

public class SshServerMongoRepository : BaseMongoRepository<SSHServer>, ISshServerMongoRepository
{
    public SshServerMongoRepository(MongoDbContext context, IMediator mediator) : base(
        context.SSHServerCollection, mediator)
    {
    }
}