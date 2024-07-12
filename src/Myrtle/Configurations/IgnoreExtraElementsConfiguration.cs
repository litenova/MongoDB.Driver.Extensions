using MongoDB.Bson.Serialization.Conventions;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Configures MongoDB to ignore extra elements during deserialization.
/// </summary>
/// <remarks>
/// This configuration allows MongoDB to ignore any fields present in the BSON document
/// that do not have corresponding properties in the C# class during deserialization.
/// It's crucial for handling schema evolution and flexibility in MongoDB documents.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Schema Evolution: Allows older versions of your application to read newer documents with additional fields.</description></item>
/// <item><description>Partial Updates: Enables working with a subset of fields from a document.</description></item>
/// <item><description>Interoperability: Facilitates sharing MongoDB databases between multiple applications or services using different subsets of fields.</description></item>
/// <item><description>Performance: Potentially improves deserialization performance by ignoring unnecessary fields.</description></item>
/// </list>
/// 
/// While useful, this configuration should be used thoughtfully. Ignoring extra elements can sometimes
/// hide issues like typos in field names or unexpected data structures. It's recommended to combine
/// this with careful logging and monitoring to ensure important data or structural changes are not missed.
/// </remarks>
public sealed class IgnoreExtraElementsConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to ignore extra elements when deserializing documents.
    /// </summary>
    /// <remarks>
    /// This method registers an IgnoreExtraElementsConvention with MongoDB's ConventionRegistry.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>MongoDB will not throw exceptions when encountering fields in BSON documents that don't have corresponding properties in C# classes.</description></item>
    /// <item><description>Extra fields in BSON documents will be silently ignored during deserialization.</description></item>
    /// <item><description>This behavior applies globally to all MongoDB operations in the application.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        ConventionRegistry.Register(nameof(IgnoreExtraElementsConvention), new ConventionPack { new IgnoreExtraElementsConvention(ignoreExtraElements: true) }, _ => true);
    }
}