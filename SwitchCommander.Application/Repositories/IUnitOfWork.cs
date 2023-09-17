namespace SwitchCommander.Application.Repositories;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    Task CommitAsync(CancellationToken cancellationToken = default);
    void Rollback();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}