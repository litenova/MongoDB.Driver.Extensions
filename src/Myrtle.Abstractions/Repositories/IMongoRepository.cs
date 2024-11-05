// ReSharper disable TypeParameterCanBeVariant

namespace Myrtle.Abstractions.Repositories;

/// <summary>
/// Defines a contract for a generic repository pattern implementation for MongoDB.
/// </summary>
/// <typeparam name="TDocument">The type of documents managed by this repository.</typeparam>
/// <typeparam name="TId">The type of the identifier for the documents.</typeparam>
/// <remarks>
/// This interface provides a set of basic CRUD (Create, Read, Update, Delete) operations
/// for working with MongoDB collections. It's designed to be implemented by concrete
/// repository classes that handle specific document types.
/// 
/// The interface is flexible enough to work with various ID types, such as string,
/// Guid, ObjectId, or any custom ID type, allowing for different identification strategies.
/// </remarks>
public interface IMongoRepository<TDocument, TId> where TDocument : class
{
    /// <summary>
    /// Retrieves a document by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the document to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation, containing the retrieved document, or null if not found.</returns>
    Task<TDocument?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new document to the collection.
    /// </summary>
    /// <param name="document">The document to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(TDocument document, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds multiple documents to the collection.
    /// </summary>
    /// <param name="documents">The documents to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddManyAsync(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing document in the collection.
    /// </summary>
    /// <param name="id">The identifier of the document to update.</param>
    /// <param name="document">The updated document.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    Task UpdateAsync(TId id, TDocument document, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple documents in the collection.
    /// </summary>
    /// <param name="updates">A dictionary containing the IDs and documents to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    Task UpdateManyAsync(IDictionary<TId, TDocument> updates, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a document from the collection.
    /// </summary>
    /// <param name="id">The identifier of the document to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple documents from the collection.
    /// </summary>
    /// <param name="ids">The identifiers of the documents to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteManyAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
}