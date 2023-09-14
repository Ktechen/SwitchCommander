using Microsoft.EntityFrameworkCore;
using SwitchCommander.Application.Repositories;
using SwitchCommander.Domain.Common;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DataContext Context;

    public BaseRepository(DataContext context)
    {
        Context = context;
    }
    
    public Task Create(T entity)
    {
        Context.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(T entity)
    {
        Context.Update(entity);
        return Task.CompletedTask;
    }

    public Task Delete(T entity)
    {
        entity.DateCreated = DateTimeOffset.UtcNow;
        Context.Update(entity);
        return Task.CompletedTask;
    }

    public Task<T?> Get(Guid id, CancellationToken cancellationToken)
    {
        return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return Context.Set<T>().ToListAsync(cancellationToken);
    }
}