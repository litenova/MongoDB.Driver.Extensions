using MongoDB.Driver;
using Myrtle.Abstractions;
using Myrtle.Abstractions.Options;

namespace Myrtle;

internal sealed class MongoConnection(IMongoConnectionStringProvider connectionStringProvider) : IMongoConnection
{
    /// <inheritdoc /> 
    public MongoClient Client { get; } = new(connectionStringProvider.ConnectionString);
}