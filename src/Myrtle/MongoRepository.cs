using MongoDB.Bson;
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
    /// Gets the MongoDB collection used by this repository.
    /// </summary>
    protected IMongoCollection<TDocument> Collection { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoRepository{TDocument, TId}"/> class.
    /// </summary>
    /// <param name="collectionContext">The context providing access to the MongoDB collection.</param>
    public MongoRepository(IMongoCollectionContext<TDocument> collectionContext)
    {
        Collection = collectionContext.Collection;
    }

    /// <summary>
    /// Retrieves a document by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the document to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The retrieved document, or null if not found.</returns>
    public async Task<TDocument?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", id);
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Adds a new document to the collection.
    /// </summary>
    /// <param name="document">The document to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The added document.</returns>
    public async Task<TDocument> AddAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        await Collection.InsertOneAsync(document, cancellationToken: cancellationToken);
        return document;
    }

    /// <summary>
    /// Updates an existing document in the collection.
    /// </summary>
    /// <param name="id">The identifier of the document to update.</param>
    /// <param name="document">The updated document.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    public async Task UpdateAsync(TId id, TDocument document, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", id);
        await Collection.ReplaceOneAsync(filter, document, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Deletes a document from the collection.
    /// </summary>
    /// <param name="id">The identifier of the document to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", id);
        await Collection.DeleteOneAsync(filter, cancellationToken);
    }
}