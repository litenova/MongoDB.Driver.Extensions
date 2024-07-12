namespace Myrtle.Abstractions.Options;

/// <summary>
/// Defines a contract for providing MongoDB database names.
/// </summary>
/// <remarks>
/// This interface is used to abstract the source of MongoDB database names.
/// It allows for dynamic determination of which database to use, which can be
/// useful in scenarios such as multi-tenancy, environment-specific databases,
/// or when the database name needs to be determined at runtime.
/// 
/// Implementations can retrieve the database name from various sources such as
/// configuration files, environment variables, or compute it based on runtime conditions.
/// </remarks>
public interface IMongoDatabaseNameProvider
{
    /// <summary>
    /// Gets the name of the MongoDB database to use.
    /// </summary>
    /// <remarks>
    /// The database name should be a valid MongoDB database name.
    /// Implementers should ensure that the database name is appropriate for the
    /// current execution context (e.g., considering multi-tenancy or environment).
    /// 
    /// If the database does not exist, MongoDB will typically create it on first use,
    /// so implementers should be aware of potential database creation implications.
    /// </remarks>
    string DatabaseName { get; }
}