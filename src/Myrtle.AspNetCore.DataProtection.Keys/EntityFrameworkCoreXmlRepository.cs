using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

/// <summary>
/// An <see cref="IXmlRepository"/> backed by an mongodb database.
/// </summary>
public sealed class MongoDbXmlRepository : IXmlRepository
{
    private readonly IMongoCollection<DataProtectionKey> _keyCollection;
    private readonly ILogger _logger;

    /// <summary>
    /// Creates a new instance of the <see cref="MongoDbXmlRepository"/>.
    /// </summary>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
    /// <param name="keyCollection">The <see cref="IMongoCollection{TDocument}"/> used to store data protection keys</param>
    public MongoDbXmlRepository(ILoggerFactory loggerFactory,
                                IMongoCollection<DataProtectionKey> keyCollection)
    {
        _keyCollection = keyCollection;
        _logger = loggerFactory.CreateLogger<MongoDbXmlRepository>();
    }

    /// <inheritdoc />
    public IReadOnlyCollection<XElement> GetAllElements()
    {
        // forces complete enumeration
        return GetAllElementsCore()
               .ToList()
               .AsReadOnly();

        IEnumerable<XElement> GetAllElementsCore()
        {
            var cursor = _keyCollection.AsQueryable()
                                       .ToCursor();

            while (cursor.MoveNext())
            {
                foreach (var dataProtectionKey in cursor.Current)
                {
                    _logger.ReadingXmlFromKey(dataProtectionKey.FriendlyName!, dataProtectionKey.Xml);

                    if (!string.IsNullOrEmpty(dataProtectionKey.Xml))
                    {
                        yield return XElement.Parse(dataProtectionKey.Xml);
                    }
                }
            }
        }
    }

    /// <inheritdoc />
    public void StoreElement(XElement element, string friendlyName)
    {
        var newKey = new DataProtectionKey()
        {
            FriendlyName = friendlyName,
            Xml = element.ToString(SaveOptions.DisableFormatting)
        };

        _keyCollection.InsertOne(newKey);

        _logger.LogSavingKeyToMongoDb(friendlyName,
                                      _keyCollection.CollectionNamespace.CollectionName,
                                      _keyCollection.Database.DatabaseNamespace.DatabaseName);
    }
}