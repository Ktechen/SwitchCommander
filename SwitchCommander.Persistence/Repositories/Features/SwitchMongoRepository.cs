using MediatR;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features;

public class SwitchMongoRepository : BaseMongoRepository<Switch>, ISwitchMongoRepository
{
    public SwitchMongoRepository(MongoDbContext context, IMediator mediator) : base(context.SwitchCollection, mediator)
    {
    }
}