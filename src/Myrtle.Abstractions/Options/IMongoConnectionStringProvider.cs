namespace Myrtle.Abstractions.Options;

/// <summary>
/// Defines a contract for providing MongoDB connection strings.
/// </summary>
/// <remarks>
/// This interface is used to abstract the source of MongoDB connection strings.
/// Implementations can retrieve the connection string from various sources such as
/// configuration files, environment variables, secret managers, or dynamic providers.
/// 
/// Using this interface allows for more flexible and secure management of connection strings,
/// especially in scenarios where connection details may change or need to be kept secure.
/// </remarks>
public interface IMongoConnectionStringProvider
{
    /// <summary>
    /// Gets the MongoDB connection string.
    /// </summary>
    /// <remarks>
    /// The connection string should be in a valid MongoDB connection string format.
    /// It typically includes details such as host, port, database name, and authentication credentials.
    /// 
    /// Implementers should ensure that sensitive information in the connection string
    /// (like passwords) is handled securely and not exposed unnecessarily.
    /// </remarks>
    string ConnectionString { get; }
}