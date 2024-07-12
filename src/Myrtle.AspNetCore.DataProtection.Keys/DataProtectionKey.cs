using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Myrtle.AspNetCore.DataProtection.Keys;

/// <summary>
/// Represents a data protection key stored in MongoDB.
/// </summary>
/// <remarks>
/// This class is used as the document model for storing ASP.NET Core data protection keys in MongoDB.
/// It includes the key's friendly name and XML representation, along with a MongoDB-specific identifier.
/// </remarks>
public sealed class DataProtectionKey
{
    /// <summary>
    /// Gets or initializes the unique identifier for the data protection key.
    /// </summary>
    /// <remarks>
    /// This property is marked with [BsonId] to indicate that it serves as the document's primary key in MongoDB.
    /// </remarks>
    [BsonId]
    public ObjectId Id { get; init; }

    /// <summary>
    /// Gets or initializes the friendly name of the data protection key.
    /// </summary>
    public string FriendlyName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes the XML representation of the data protection key.
    /// </summary>
    public string Xml { get; init; } = string.Empty;
}