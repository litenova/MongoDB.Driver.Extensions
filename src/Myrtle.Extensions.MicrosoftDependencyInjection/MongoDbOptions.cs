using Myrtle.Abstractions.Options;

namespace Myrtle.Extensions.MicrosoftDependencyInjection;

/// <summary>
/// Represents the options for a MongoDB connection.
/// </summary>
public sealed class MongoDbOptions : IMongoOptionsProvider
{
    /// <summary>
    /// The name of the configuration section.
    /// </summary>
    public const string ConfigurationSectionName = "MongoDb";

    /// <summary>
    /// The connection string to use.
    /// </summary>
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";

    /// <summary>
    /// The name of the database to use.
    /// </summary>
    public string DatabaseName { get; set; } = "Main";
}