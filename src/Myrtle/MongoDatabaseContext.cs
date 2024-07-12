using MongoDB.Driver;
using Myrtle.Abstractions;
using Myrtle.Abstractions.Options;

namespace Myrtle;

internal sealed class MongoDatabaseContext(IMongoConnection connection, IMongoDatabaseNameProvider mongoDatabaseNameProvider) : IMongoDatabaseContext
{
    public IMongoDatabase Database { get; } = connection.Client.GetDatabase(mongoDatabaseNameProvider.DatabaseName);
}