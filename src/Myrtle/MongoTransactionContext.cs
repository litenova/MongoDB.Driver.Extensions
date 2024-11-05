using MongoDB.Driver;
using Myrtle.Abstractions;
using Myrtle.Abstractions.Exceptions;

namespace Myrtle;

/// <summary>
/// Manages the lifecycle of a MongoDB transaction, including starting, committing, and aborting transactions.
/// Utilizes the MongoDB client provided by an <see cref="IMongoConnection"/> implementation.
/// </summary>
internal sealed class MongoTransactionContext : IMongoTransactionContext
{
    private readonly IMongoConnection _mongoConnection;
    private IClientSessionHandle? _session;

    /// <inheritdoc/>
    public bool IsActive => _session is { IsInTransaction: true };

    /// <inheritdoc/>
    public IClientSessionHandle Session
    {
        get
        {
            if (_session == null)
            {
                throw new MongoTransactionNotStartedException();
            }

            return _session;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoTransactionContext"/> class.
    /// </summary>
    /// <param name="mongoConnection">The connection interface used to interact with MongoDB.</param>
    public MongoTransactionContext(IMongoConnection mongoConnection)
    {
        _mongoConnection = mongoConnection;
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (IsActive)
        {
            throw new MongoTransactionAlreadyStartedException();
        }

        _session = await _mongoConnection.Client.StartSessionAsync(cancellationToken: cancellationToken);
        _session.StartTransaction();
    }

    /// <inheritdoc/>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (!IsActive || _session == null)
        {
            throw new MongoTransactionNotStartedException();
        }

        try
        {
            await _session.CommitTransactionAsync(cancellationToken);
        }
        finally
        {
            await DisposeAsync();
        }
    }

    /// <inheritdoc/>
    public async Task AbortAsync(CancellationToken cancellationToken = default)
    {
        if (!IsActive || _session == null)
        {
            throw new MongoTransactionNotStartedException();
        }

        try
        {
            await _session.AbortTransactionAsync(cancellationToken);
        }
        finally
        {
            await DisposeAsync();
        }
    }

    /// <summary>
    /// Disposes the current session, ensuring cleanup of resources.
    /// </summary>
    public void Dispose()
    {
        if (_session != null)
        {
            _session.Dispose();
            _session = null;
        }
    }

    /// <summary>
    /// Calls the synchronous Dispose method as session doesn't have an asynchronous Dispose.
    /// </summary>
    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }
}