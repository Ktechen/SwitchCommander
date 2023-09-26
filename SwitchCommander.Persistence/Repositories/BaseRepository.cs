using System.Linq.Expressions;
using MediatR;
using MongoDB.Driver;
using SwitchCommander.Application.Repositories;
using SwitchCommander.Domain.Common;

namespace SwitchCommander.Persistence.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly IMediator _mediator;
    protected readonly IMongoCollection<T> Collection;

    protected BaseRepository(IMongoCollection<T> collection, IMediator mediator)
    {
        Collection = collection;
        _mediator = mediator;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        await DispatchDomainEventsAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Collection.Find(predicate).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        await DispatchDomainEventsAsync(entity);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
    }

    private async Task DispatchDomainEventsAsync(BaseEntity entity)
    {
        var domainEvents = entity.DomainEvents.ToList();
        entity.DomainEvents.Clear();
        foreach (var domainEvent in domainEvents) await _mediator.Publish(domainEvent, CancellationToken.None);
    }
}