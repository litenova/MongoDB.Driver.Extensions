using Microsoft.Extensions.Logging;

namespace Myrtle.AspNetCore.DataProtection.Keys;

/// <summary>
/// Provides extension methods for logging in the context of MongoDB data protection key operations.
/// </summary>
internal static partial class LoggingExtensions
{
    [LoggerMessage(1, LogLevel.Debug, "Reading data protection key: {FriendlyName}", EventName = "ReadingKeyFromElement")]
    public static partial void ReadingKeyFromElement(this ILogger logger, string? friendlyName, string? value);

    [LoggerMessage(2, LogLevel.Debug, "Saving data protection key '{FriendlyName}' to collection '{Collection}' in database '{Database}'", EventName = "SavingKeyToMongoDb")]
    public static partial void SavingKeyToMongoDb(this ILogger logger, string friendlyName, string collection, string database);
}