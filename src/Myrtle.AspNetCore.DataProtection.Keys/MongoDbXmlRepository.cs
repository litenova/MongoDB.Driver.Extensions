using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Myrtle.AspNetCore.DataProtection.Keys;

/// <summary>
/// Provides an implementation of IXmlRepository that stores data protection keys in MongoDB.
/// </summary>
/// <remarks>
/// This class allows ASP.NET Core Data Protection to persist encryption keys in a MongoDB database,
/// which is useful for scenarios where keys need to be shared across multiple instances of an application.
/// </remarks>
public sealed class MongoDbXmlRepository : IXmlRepository
{
    private readonly IMongoCollection<DataProtectionKey> _keyCollection;
    private readonly ILogger<MongoDbXmlRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoDbXmlRepository"/> class.
    /// </summary>
    /// <param name="keyCollection">The MongoDB collection used to store data protection keys.</param>
    /// <param name="logger">The logger used for logging operations and errors.</param>
    public MongoDbXmlRepository(IMongoCollection<DataProtectionKey> keyCollection, ILogger<MongoDbXmlRepository> logger)
    {
        _keyCollection = keyCollection;
        _logger = logger;
    }

    /// <inheritdoc />
    public IReadOnlyCollection<XElement> GetAllElements()
    {
        return _keyCollection.AsQueryable()
            .ToEnumerable()
            .Select(key =>
            {
                _logger.ReadingKeyFromElement(key.FriendlyName, key.Xml);
                return XElement.Parse(key.Xml);
            })
            .ToList()
            .AsReadOnly();
    }

    /// <inheritdoc />
    public void StoreElement(XElement element, string friendlyName)
    {
        var newKey = new DataProtectionKey { FriendlyName = friendlyName, Xml = element.ToString(SaveOptions.DisableFormatting) };

        _keyCollection.InsertOne(newKey);
        _logger.SavingKeyToMongoDb(friendlyName, _keyCollection.CollectionNamespace.CollectionName, _keyCollection.Database.DatabaseNamespace.DatabaseName);
    }
}