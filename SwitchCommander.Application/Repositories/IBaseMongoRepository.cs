using System.Linq.Expressions;
using MongoDB.Driver;
using SwitchCommander.Domain.Common;

namespace SwitchCommander.Application.Repositories;

public interface IBaseMongoRepository<T> where T : BaseEntity
{
    public Task Commit();

    public Task Rollback();

    public Task<T?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<T>> ReadAllAsync(CancellationToken cancellationToken = default);

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    public Task AddAsync(T entity, CancellationToken cancellationToken = default);

    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    public Task<bool> ReplaceAsync(T entity, CancellationToken cancellationToken = default);

    public Task<bool> UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
        CancellationToken cancellationToken = default);

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}