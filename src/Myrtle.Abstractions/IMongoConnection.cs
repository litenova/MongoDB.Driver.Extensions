using MongoDB.Driver;

namespace Myrtle.Abstractions;

/// <summary>
/// Defines a contract for managing a connection to a MongoDB database.
/// </summary>
/// <remarks>
/// This interface abstracts the creation and management of a MongoDB client,
/// allowing for easier testing and flexibility in how connections are established.
/// Implementations of this interface should handle connection string management
/// and any necessary connection pooling or lifecycle management.
/// </remarks>
public interface IMongoConnection
{
    /// <summary>
    /// Gets the MongoDB client instance used for database operations.
    /// </summary>
    /// <remarks>
    /// The client should be initialized using the appropriate connection string
    /// and any necessary options. It's typically used to obtain database and
    /// collection references for further operations.
    /// </remarks>
    MongoClient Client { get; }
}