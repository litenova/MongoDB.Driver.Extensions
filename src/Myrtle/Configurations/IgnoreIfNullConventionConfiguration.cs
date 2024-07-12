using MongoDB.Bson.Serialization.Conventions;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Configures MongoDB to ignore null values when serializing documents.
/// </summary>
/// <remarks>
/// This configuration sets up a convention to exclude properties with null values
/// from being serialized into MongoDB documents. This can lead to more compact
/// document storage and can be particularly useful in scenarios with sparse data.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Reducing document size for collections with many optional fields.</description></item>
/// <item><description>Implementing sparse indexes more effectively in MongoDB.</description></item>
/// <item><description>Improving query performance by reducing the amount of data that needs to be processed.</description></item>
/// </list>
/// 
/// Considerations:
/// <list type="bullet">
/// <item><description>This configuration may change the structure of your documents, potentially affecting existing queries or indexes.</description></item>
/// <item><description>It may make it harder to distinguish between a field that was explicitly set to null and a field that was not set at all.</description></item>
/// <item><description>Care should be taken when using this with applications that rely on the presence of null fields.</description></item>
/// </list>
/// </remarks>
public sealed class IgnoreIfNullConventionConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to ignore null values when serializing documents.
    /// </summary>
    /// <remarks>
    /// This method registers an IgnoreIfNullConvention with MongoDB's ConventionRegistry.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>Properties with null values will not be included in serialized BSON documents.</description></item>
    /// <item><description>This behavior applies globally to all classes and collections in the application.</description></item>
    /// <item><description>Existing documents in the database will not be affected until they are updated.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        ConventionRegistry.Register("IgnoreIfNull", new ConventionPack { new IgnoreIfNullConvention(true) }, _ => true);
    }
}