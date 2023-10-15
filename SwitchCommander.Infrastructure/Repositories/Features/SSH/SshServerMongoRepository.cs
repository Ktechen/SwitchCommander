using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features.SSH;

public class SshServerMongoRepository : BaseMongoRepository<SSHServer>, ISshServerMongoRepository
{
    public SshServerMongoRepository(MongoDbContext context, IMediator mediator) : base(
        context.SSHServerCollection, mediator)
    {
    }
}