using MediatR;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Repositories.Features;

public class SwitchMongoRepository : BaseMongoRepository<Switch>, ISwitchMongoRepository
{
    public SwitchMongoRepository(MongoDbContext context, IMediator mediator) : base(context.SwitchCollection, mediator)
    {
    }
}