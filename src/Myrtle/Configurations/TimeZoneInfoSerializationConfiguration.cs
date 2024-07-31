using MongoDB.Bson.Serialization;
using Myrtle.Abstractions.Configurations;
using Myrtle.Serializers;

namespace Myrtle.Configurations;

/// <summary>
/// Configures the serialization of TimeZoneInfo values in MongoDB.
/// </summary>
/// <remarks>
/// This configuration ensures that all TimeZoneInfo values are stored as their ID string in MongoDB.
/// It's crucial for maintaining consistency in time zone data, especially in distributed systems
/// or applications dealing with multiple regions.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Ensuring consistent time zone storage across different servers or regions.</description></item>
/// <item><description>Simplifying time zone conversions and comparisons in queries.</description></item>
/// <item><description>Improving interoperability between different parts of a system or different applications.</description></item>
/// </list>
/// 
/// Note that this configuration affects how TimeZoneInfo values are serialized to the database.
/// Existing non-ID-based data in the database will not be automatically converted.
/// </remarks>
public sealed class TimeZoneInfoSerializationConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to serialize TimeZoneInfo values as their ID string.
    /// </summary>
    /// <remarks>
    /// This method registers a custom TimeZoneInfoSerializer with MongoDB's BsonSerializer.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>All TimeZoneInfo values will be converted to their ID string before being stored in MongoDB.</description></item>
    /// <item><description>TimeZoneInfo values retrieved from MongoDB will be constructed from their ID string.</description></item>
    /// <item><description>This behavior applies globally to all MongoDB operations in the application.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        BsonSerializer.RegisterSerializer(typeof(TimeZoneInfo), new TimeZoneInfoSerializer());
    }
}