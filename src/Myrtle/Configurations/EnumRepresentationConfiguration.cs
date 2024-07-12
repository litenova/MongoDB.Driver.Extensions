using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Configures the representation of enum values in MongoDB.
/// </summary>
/// <remarks>
/// This configuration ensures that enum values are stored as strings in MongoDB instead of their numeric representations.
/// It's beneficial for readability, maintainability, and backward compatibility of stored data.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Improving readability of stored enum values in MongoDB documents.</description></item>
/// <item><description>Maintaining backward compatibility when enum definitions change.</description></item>
/// <item><description>Facilitating easier querying and aggregation on enum fields.</description></item>
/// </list>
/// 
/// Note that this may slightly increase storage requirements compared to numeric enum representations.
/// </remarks>
public sealed class EnumRepresentationConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to represent enum values as strings.
    /// </summary>
    /// <remarks>
    /// This method registers an EnumRepresentationConvention with MongoDB's ConventionRegistry.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>All enum values will be stored as strings in MongoDB documents.</description></item>
    /// <item><description>Enum values in queries and results will use string representations.</description></item>
    /// <item><description>This behavior applies globally to all enum types in the application.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        ConventionRegistry.Register("EnumStringConvention", new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, _ => true);
    }
}