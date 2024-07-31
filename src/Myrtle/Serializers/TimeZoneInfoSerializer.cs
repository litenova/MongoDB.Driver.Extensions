using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Myrtle.Serializers;

/// <summary>
/// Serializes and deserializes <see cref="TimeZoneInfo"/> objects to and from their string ID representation.
/// </summary>
/// <remarks>
/// This serializer converts <see cref="TimeZoneInfo"/> objects to their ID string when storing them in MongoDB,
/// and converts back to <see cref="TimeZoneInfo"/> objects when reading from MongoDB.
/// </remarks>
public class TimeZoneInfoSerializer : SerializerBase<TimeZoneInfo>
{
    /// <summary>
    /// Serializes a <see cref="TimeZoneInfo"/> object to its ID string representation.
    /// </summary>
    /// <param name="context">The context for the serialization process.</param>
    /// <param name="args">Serialization arguments.</param>
    /// <param name="value">The <see cref="TimeZoneInfo"/> object to serialize.</param>
    /// <remarks>
    /// This method writes the <see cref="TimeZoneInfo.Id"/> of the provided <paramref name="value"/> to the MongoDB document.
    /// </remarks>
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TimeZoneInfo value)
    {
        context.Writer.WriteString(value.Id);
    }

    /// <summary>
    /// Deserializes a <see cref="TimeZoneInfo"/> object from its ID string representation.
    /// </summary>
    /// <param name="context">The context for the deserialization process.</param>
    /// <param name="args">Deserialization arguments.</param>
    /// <returns>The deserialized <see cref="TimeZoneInfo"/> object.</returns>
    /// <remarks>
    /// This method reads the time zone ID string from the MongoDB document and converts it back to a <see cref="TimeZoneInfo"/>.
    /// </remarks>
    public override TimeZoneInfo Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var timeZoneId = context.Reader.ReadString();
        return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }
}