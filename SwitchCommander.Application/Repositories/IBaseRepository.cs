using System.Linq.Expressions;
using SwitchCommander.Domain.Common;

namespace SwitchCommander.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    public Task Commit();

    public Task Rollback();
    
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    public Task AddAsync(T entity, CancellationToken cancellationToken = default);
    
    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}