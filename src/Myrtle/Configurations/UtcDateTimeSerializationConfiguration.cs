using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Configures the serialization of DateTime values in MongoDB.
/// </summary>
/// <remarks>
/// This configuration ensures that all DateTime values are stored in UTC format in MongoDB.
/// It's crucial for maintaining consistency in timestamp data, especially in distributed systems
/// or applications dealing with multiple time zones.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Ensuring consistent timestamp storage across different servers or regions.</description></item>
/// <item><description>Simplifying time zone conversions and comparisons in queries.</description></item>
/// <item><description>Improving interoperability between different parts of a system or different applications.</description></item>
/// </list>
/// 
/// Note that this configuration affects how DateTime values are serialized to the database.
/// Existing non-UTC data in the database will not be automatically converted.
/// </remarks>
public sealed class UtcDateTimeSerializationConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to serialize DateTime values as UTC.
    /// </summary>
    /// <remarks>
    /// This method registers a custom DateTimeSerializer with MongoDB's BsonSerializer.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>All DateTime values will be converted to UTC before being stored in MongoDB.</description></item>
    /// <item><description>DateTime values retrieved from MongoDB will be in UTC format.</description></item>
    /// <item><description>This behavior applies globally to all MongoDB operations in the application.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Utc));
    }
}