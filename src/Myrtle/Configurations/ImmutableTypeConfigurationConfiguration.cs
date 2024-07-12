using MongoDB.Bson.Serialization.Conventions;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Configures MongoDB to properly handle immutable types.
/// </summary>
/// <remarks>
/// This configuration enables MongoDB to work effectively with immutable types in C#,
/// such as records or classes with init-only properties.
/// It's important for maintaining the immutability of objects while allowing MongoDB to serialize and deserialize them.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Working with C# records or classes with init-only properties.</description></item>
/// <item><description>Ensuring that immutable objects can be properly serialized to and deserialized from MongoDB.</description></item>
/// <item><description>Maintaining immutability guarantees in domain models while persisting to MongoDB.</description></item>
/// </list>
/// 
/// Note that this configuration may have a small performance impact during serialization and deserialization.
/// </remarks>
public sealed class ImmutableTypeConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to use the immutable type class map convention.
    /// </summary>
    /// <remarks>
    /// This method registers an ImmutableTypeClassMapConvention with MongoDB's ConventionRegistry.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>MongoDB will be able to properly serialize and deserialize immutable types.</description></item>
    /// <item><description>Immutable objects can be stored in and retrieved from MongoDB without losing their immutability guarantees.</description></item>
    /// <item><description>This behavior applies to all immutable types in the application.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        var conventions = new ConventionPack { new ImmutableTypeClassMapConvention() };
        ConventionRegistry.Register(nameof(ImmutableTypeClassMapConvention), conventions, type => true);
    }
}