using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features.SSH;

public class SshSequenceMongoRepository : BaseMongoRepository<SSHSequence>, ISshSequenceMongoRepository
{
    public SshSequenceMongoRepository(MongoDbSshContext sshContext, IMediator mediator) : base(
        sshContext.SSHCommandSequenceCollection, mediator)
    {
    }
}