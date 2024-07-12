using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Configures the serialization of GUID values in MongoDB to use a standard representation.
/// </summary>
/// <remarks>
/// This configuration ensures that all GUID values are stored in MongoDB using a consistent,
/// standard representation. By default, it uses the 'GuidRepresentation.Standard' format,
/// which is compatible with other languages and platforms.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Ensuring consistency in GUID storage across different parts of an application or different applications.</description></item>
/// <item><description>Improving interoperability when working with GUIDs across different platforms or languages.</description></item>
/// <item><description>Standardizing GUID representation for easier querying and indexing in MongoDB.</description></item>
/// </list>
/// 
/// Note:
/// <list type="bullet">
/// <item><description>This configuration affects how new GUID values are serialized. Existing data in the database will not be automatically converted.</description></item>
/// <item><description>Changing GUID representation in an existing system should be done carefully to avoid compatibility issues with existing data.</description></item>
/// </list>
/// </remarks>
public sealed class GuidSerializationConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to serialize GUID values using the standard representation.
    /// </summary>
    /// <remarks>
    /// This method registers a custom GuidSerializer with MongoDB's BsonSerializer.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>All GUID values will be stored in MongoDB using the standard representation.</description></item>
    /// <item><description>This ensures consistent GUID handling across different platforms and languages.</description></item>
    /// <item><description>The configuration applies globally to all MongoDB operations in the application.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
    }
}