using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Domain.Dtos.SSH;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features.SSH;

public class SshServerMongoRepository : BaseMongoRepository<SSHServer>, ISshServerMongoRepository
{
    public SshServerMongoRepository(MongoDbSshContext sshContext, IMediator mediator) : base(
        sshContext.SSHServerCollection, mediator)
    {
    }
}