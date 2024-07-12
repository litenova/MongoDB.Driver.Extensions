namespace Myrtle.Abstractions.Options;

/// <summary>
/// Represents a provider for MongoDB options, combining connection string and database name provision.
/// </summary>
/// <remarks>
/// This interface aggregates the functionality of both <see cref="IMongoConnectionStringProvider"/> 
/// and <see cref="IMongoDatabaseNameProvider"/>. It's used to provide a complete set of 
/// basic options required for establishing a MongoDB connection and selecting a database.
/// 
/// Implementations of this interface can be used to centralize MongoDB configuration,
/// making it easier to manage and inject MongoDB options throughout an application.
/// 
/// This interface is particularly useful in scenarios where both the connection string
/// and database name need to be dynamically determined or managed together.
/// </remarks>
public interface IMongoOptionsProvider : IMongoConnectionStringProvider, IMongoDatabaseNameProvider
{
    // This interface combines IMongoConnectionStringProvider and IMongoDatabaseNameProvider
    // No additional members are defined here
}