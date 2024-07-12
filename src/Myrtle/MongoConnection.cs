using MongoDB.Driver;
using Myrtle.Abstractions;
using Myrtle.Abstractions.Options;

namespace Myrtle;

internal sealed class MongoConnection(IMongoConnectionStringProvider connectionStringProvider) : IMongoConnection
{
    public MongoClient Client { get; } = new(connectionStringProvider.ConnectionString);
}