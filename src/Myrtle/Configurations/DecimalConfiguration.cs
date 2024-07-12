using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Configures the serialization of decimal values in MongoDB.
/// </summary>
/// <remarks>
/// This configuration ensures that decimal values are stored as high-precision Decimal128 in MongoDB.
/// It's crucial for applications requiring exact decimal representations, such as financial calculations.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Financial applications requiring precise monetary calculations.</description></item>
/// <item><description>Scientific applications needing high-precision decimal representations.</description></item>
/// <item><description>Any scenario where the exactness of decimal values is critical.</description></item>
/// </list>
/// 
/// Note that using Decimal128 may have performance implications compared to other numeric types.
/// </remarks>
public sealed class DecimalConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to serialize decimal values as Decimal128.
    /// </summary>
    /// <remarks>
    /// This method registers a custom DecimalSerializer with MongoDB's BsonSerializer.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>All decimal values will be stored as high-precision Decimal128 in MongoDB.</description></item>
    /// <item><description>Decimal values can be stored and retrieved without loss of precision.</description></item>
    /// <item><description>This behavior applies globally to all MongoDB operations in the application.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
    }
}