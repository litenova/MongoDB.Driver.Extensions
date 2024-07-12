using MongoDB.Driver;

namespace Myrtle.Abstractions;

/// <summary>
/// Defines a contract for accessing a specific MongoDB database.
/// </summary>
/// <remarks>
/// This interface provides a level of abstraction over a MongoDB database,
/// allowing for easier management of database-level operations and settings.
/// It's typically used to obtain references to specific collections within the database.
/// </remarks>
public interface IMongoDatabaseContext
{
    /// <summary>
    /// Gets the MongoDB database instance for performing database operations.
    /// </summary>
    /// <remarks>
    /// This property provides access to the underlying IMongoDatabase instance,
    /// which can be used for database-level operations or to obtain collection references.
    /// The specific database this context refers to should be determined by the implementation,
    /// typically based on configuration or dependency injection.
    /// </remarks>
    IMongoDatabase Database { get; }
}