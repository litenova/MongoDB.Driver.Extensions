using MongoDB.Driver;
using Myrtle.Abstractions;
using Myrtle.Abstractions.Repositories;

namespace Myrtle;

/// <summary>
/// Provides a generic repository implementation for MongoDB operations.
/// </summary>
/// <typeparam name="TDocument">The type of documents managed by this repository.</typeparam>
/// <typeparam name="TId">The type of the identifier for the documents.</typeparam>
/// <remarks>
/// This class implements the IMongoRepository interface and provides basic CRUD operations
/// for documents in a MongoDB collection. It supports various ID types through the generic TId parameter.
/// </remarks>
public class MongoRepository<TDocument, TId> : IMongoRepository<TDocument, TId> where TDocument : class
{
    /// <summary>
    /// The MongoDB collection used by this repository.
    /// </summary>
    protected IMongoCollection<TDocument> Collection { get; }

    /// <summary>
    /// The transaction scope used by this repository.
    /// </summary>
    protected IMongoTransactionContext TransactionContext { get; }

    /// <summary>
    /// Initializes a new instance of the MongoRepository class with the specified collection context.
    /// </summary>
    /// <param name="collectionContext">The collection context to use for MongoDB operations.</param>
    /// <param name="transactionContext">The transaction scope to use for MongoDB operations.</param>
    public MongoRepository(
        IMongoCollectionContext<TDocument> collectionContext,
        IMongoTransactionContext transactionContext)
    {
        Collection = collectionContext.Collection;
        TransactionContext = transactionContext;
    }

    /// <inheritdoc />
    public async Task<TDocument?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", id);
        return TransactionContext.IsActive
            ? await Collection.Find(TransactionContext.Session, filter).FirstOrDefaultAsync(cancellationToken)
            : await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        if (TransactionContext.IsActive)
        {
            await Collection.InsertOneAsync(TransactionContext.Session, document, cancellationToken: cancellationToken);
        }
        else
        {
            await Collection.InsertOneAsync(document, cancellationToken: cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task AddManyAsync(
        IEnumerable<TDocument> documents,
        CancellationToken cancellationToken = default)
    {
        if (TransactionContext.IsActive)
        {
            await Collection.InsertManyAsync(TransactionContext.Session, documents, cancellationToken: cancellationToken);
        }
        else
        {
            await Collection.InsertManyAsync(documents, cancellationToken: cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TId id, TDocument document, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", id);

        if (TransactionContext.IsActive)
        {
            await Collection.ReplaceOneAsync(TransactionContext.Session, filter, document, cancellationToken: cancellationToken);
        }
        else
        {
            await Collection.ReplaceOneAsync(filter, document, cancellationToken: cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task UpdateManyAsync(
        IDictionary<TId, TDocument> updates,
        CancellationToken cancellationToken = default)
    {
        var writeModels = updates.Select(kvp =>
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", kvp.Key);
            return new ReplaceOneModel<TDocument>(filter, kvp.Value);
        });

        if (TransactionContext.IsActive)
        {
            await Collection.BulkWriteAsync(TransactionContext.Session, writeModels, cancellationToken: cancellationToken);
        }
        else
        {
            await Collection.BulkWriteAsync(writeModels, cancellationToken: cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", id);

        if (TransactionContext.IsActive)
        {
            await Collection.DeleteOneAsync(TransactionContext.Session, filter, cancellationToken: cancellationToken);
        }
        else
        {
            await Collection.DeleteOneAsync(filter, cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task DeleteManyAsync(
        IEnumerable<TId> ids,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<TDocument>.Filter.In("_id", ids);

        if (TransactionContext.IsActive)
        {
            await Collection.DeleteManyAsync(TransactionContext.Session, filter, cancellationToken: cancellationToken);
        }
        else
        {
            await Collection.DeleteManyAsync(filter, cancellationToken);
        }
    }
}