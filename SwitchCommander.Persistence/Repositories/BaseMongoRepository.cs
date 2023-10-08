using System.Linq.Expressions;
using MediatR;
using MongoDB.Driver;
using SwitchCommander.Application.Repositories;
using SwitchCommander.Domain.Common;

namespace SwitchCommander.Persistence.Repositories;

public abstract class BaseMongoRepository<T> : IDisposable, IBaseMongoRepository<T> where T : BaseEntity
{
    private readonly IMediator _mediator;
    private readonly IClientSessionHandle _session;
    protected readonly IMongoCollection<T> Collection;

    protected BaseMongoRepository(IMongoCollection<T> collection, IMediator mediator)
    {
        Collection = collection;
        _session = collection.Database.Client.StartSession();
        _mediator = mediator;
    }

    public async Task Commit()
    {
        await _session.CommitTransactionAsync();
    }

    public async Task Rollback()
    {
        await _session.AbortTransactionAsync();
    }

    public async Task<T?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
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

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await Collection.InsertManyAsync(entities, cancellationToken: cancellationToken);
    }

    public async Task<bool> ReplaceAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.DateUpdated = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var result = await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
        return result.IsAcknowledged && result.ModifiedCount == 1;
    }

    public async Task<bool> UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, CancellationToken cancellationToken = default)
    {
        var result = await Collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.IsAcknowledged && result.ModifiedCount == 1;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await Collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
        return result.IsAcknowledged && result.DeletedCount == 1;
    }

    public void Dispose()
    {
        _session.Dispose();
    }

    private async Task DispatchDomainEventsAsync(BaseEntity entity)
    {
        var domainEvents = entity.DomainEvents.ToList();
        entity.DomainEvents.Clear();
        foreach (var domainEvent in domainEvents) await _mediator.Publish(domainEvent, CancellationToken.None);
    }
}