using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Myrtle.AspNetCore.DataProtection.Keys;

/// <summary>
/// Provides extension methods for configuring ASP.NET Core Data Protection to use MongoDB for key storage.
/// </summary>
public static class DataProtectionExtensions
{
    private const string DefaultCollectionName = "DataProtectionKeys";

    /// <summary>
    /// Configures data protection to persist keys to MongoDB.
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> to configure.</param>
    /// <param name="mongoClient">The MongoDB client to use.</param>
    /// <param name="databaseName">The name of the database to store keys in.</param>
    /// <param name="collectionName">The name of the collection to store keys in. Defaults to "DataProtectionKeys".</param>
    /// <returns>The <see cref="IDataProtectionBuilder"/> so that additional calls can be chained.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(
        this IDataProtectionBuilder builder,
        IMongoClient mongoClient,
        string databaseName,
        string collectionName = DefaultCollectionName)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger<MongoDbXmlRepository>() ?? NullLogger<MongoDbXmlRepository>.Instance;
            var database = mongoClient.GetDatabase(databaseName);
            var collection = database.GetCollection<DataProtectionKey>(collectionName);

            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(collection, logger);
            });
        });

        return builder;
    }

    /// <summary>
    /// Configures data protection to persist keys to MongoDB using a custom collection factory.
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> to configure.</param>
    /// <param name="collectionFactory">A factory function to create the MongoDB collection.</param>
    /// <returns>The <see cref="IDataProtectionBuilder"/> so that additional calls can be chained.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(
        this IDataProtectionBuilder builder,
        Func<IServiceProvider, IMongoCollection<DataProtectionKey>> collectionFactory)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger<MongoDbXmlRepository>() ?? NullLogger<MongoDbXmlRepository>.Instance;
            var collection = collectionFactory(services);

            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(collection, logger);
            });
        });

        return builder;
    }
}