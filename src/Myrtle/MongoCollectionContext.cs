using MongoDB.Driver;
using Myrtle.Abstractions;

namespace Myrtle;

internal sealed class MongoCollectionContext<TDocument>(IMongoDatabaseContext databaseContext) : IMongoCollectionContext<TDocument>
{
    /// <inheritdoc /> 
    public IMongoCollection<TDocument> Collection { get; } = databaseContext.Database.GetCollection<TDocument>(typeof(TDocument).Name);
}