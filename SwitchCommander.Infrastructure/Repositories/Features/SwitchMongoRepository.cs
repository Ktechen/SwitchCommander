using MediatR;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features;

public class SwitchMongoRepository : BaseMongoRepository<Switch>, ISwitchMongoRepository
{
    public SwitchMongoRepository(MongoDbSshContext sshContext, IMediator mediator) : base(sshContext.SwitchCollection, mediator)
    {
    }
}