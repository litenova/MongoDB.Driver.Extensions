using MongoDB.Driver;

namespace Myrtle.Abstractions;

/// <summary>
/// Defines a contract for accessing a specific MongoDB collection.
/// </summary>
/// <typeparam name="TDocument">The type of documents stored in the collection.</typeparam>
/// <remarks>
/// This interface provides a level of abstraction over a MongoDB collection,
/// allowing for type-safe access to collection-level operations.
/// It's typically used as a dependency in repositories or services that need to
/// perform CRUD operations on a specific collection.
/// </remarks>
public interface IMongoCollectionContext<TDocument>
{
    /// <summary>
    /// Gets the MongoDB collection instance for performing operations on documents of type TDocument.
    /// </summary>
    /// <remarks>
    /// This property provides access to the underlying IMongoCollection{TDocument} instance,
    /// which can be used for all standard MongoDB operations like inserting, updating, 
    /// deleting, and querying documents. The specific collection this context refers to
    /// should be determined by the implementation, typically based on the TDocument type
    /// or configuration.
    /// </remarks>
    IMongoCollection<TDocument> Collection { get; }
}