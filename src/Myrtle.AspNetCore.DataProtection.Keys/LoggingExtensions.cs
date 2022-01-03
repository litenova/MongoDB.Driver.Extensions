namespace Microsoft.Extensions.Logging;

internal static partial class LoggingExtensions
{
    [LoggerMessage(1, LogLevel.Debug, "Reading data with key '{FriendlyName}', value '{Value}'.", EventName = "ReadKeyFromElement")]
    public static partial void ReadingXmlFromKey(this ILogger logger, string? friendlyName, string? value);

    [LoggerMessage(2, LogLevel.Debug, "Saving key '{FriendlyName}' to '{Collection} in {Database}'.", EventName = "LogSavingKeyToMongoDb")]
    public static partial void LogSavingKeyToMongoDb(this ILogger logger, string friendlyName, string collection, string database);
}
