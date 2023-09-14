using SwitchCommander.Domain.Common;

namespace SwitchCommander.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T?> Get(Guid id, CancellationToken cancellationToken);
    Task<List<T>> GetAll(CancellationToken cancellationToken);
}