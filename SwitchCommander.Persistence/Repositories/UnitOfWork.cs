using MongoDB.Driver;
using SwitchCommander.Application.Repositories;

namespace SwitchCommander.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMongoClient _mongoClient;
    private IClientSessionHandle _session;

    public UnitOfWork(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
    }

    public void BeginTransaction()
    {
        if (_session != null) return;
        _session = _mongoClient.StartSession();
        _session.StartTransaction();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_session == null) throw new InvalidOperationException("Transaction has not been started.");

        await _session.CommitTransactionAsync(cancellationToken);
        _session.Dispose();
        _session = null;
    }

    public void Rollback()
    {
        _session?.AbortTransaction();
        _session?.Dispose();
        _session = null;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // No need to do anything here for MongoDB, as it's a NoSQL database.
        // In a relational database, you would commit the transaction here.
        // MongoDB does not support explicit transactions for single document operations.
    }

    public void Dispose()
    {
        _session?.Dispose();
    }
}