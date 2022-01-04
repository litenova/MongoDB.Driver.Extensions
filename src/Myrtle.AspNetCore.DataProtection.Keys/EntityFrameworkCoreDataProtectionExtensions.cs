using System;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microsoft.AspNetCore.DataProtection;

/// <summary>
/// Extension method class for configuring instances of <see cref="MongoDbXmlRepository"/>
/// </summary>
public static class EntityFrameworkCoreDataProtectionExtensions
{
    private const string DataProtectionKeyCollectionName = "DataProtectionKeys";
    
    /// <summary>
    /// Configures the data protection system to persist keys to an EntityFrameworkCore datastore
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> instance to modify.</param>
    /// <returns>The value <paramref name="builder"/>.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(this IDataProtectionBuilder builder)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
            var client = services.GetRequiredService<IMongoClient>();
            var database = client.GetDatabase(DataProtectionKeyCollectionName);
            var collection = database.GetCollection<DataProtectionKey>("DataProtectionKeys");
            
            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(loggerFactory, collection);
            });
        });

        return builder;
    }
    
    /// <summary>
    /// Configures the data protection system to persist keys to an EntityFrameworkCore datastore
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> instance to modify.</param>
    /// <param name="mongoCollection">The mongo collection instance used to create the <see cref="MongoDbXmlRepository"/></param>
    /// <returns>The value <paramref name="builder"/>.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(this IDataProtectionBuilder builder, 
                                                              IMongoCollection<DataProtectionKey> mongoCollection)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
            
            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(loggerFactory, mongoCollection);
            });
        });

        return builder;
    }

    /// <summary>
    /// Configures the data protection system to persist keys to an EntityFrameworkCore datastore
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> instance to modify.</param>
    /// <param name="mongoCollectionFactory">The factory that creates a <see cref="IMongoCollection{DataProtectionKey}"/> instance</param>
    /// <returns>The value <paramref name="builder"/>.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(this IDataProtectionBuilder builder, 
                                                              Func<IMongoClient, IMongoCollection<DataProtectionKey>> mongoCollectionFactory)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
            var client = services.GetRequiredService<IMongoClient>();
            var collection = mongoCollectionFactory(client);
            
            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(loggerFactory, collection);
            });
        });

        return builder;
    }
    
    /// <summary>
    /// Configures the data protection system to persist keys to an EntityFrameworkCore datastore
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> instance to modify.</param>
    /// <param name="database">The <see cref="IMongoDatabase"/> instance</param>
    /// <returns>The value <paramref name="builder"/>.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(this IDataProtectionBuilder builder, 
                                                              IMongoDatabase database)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
            var collection = database.GetCollection<DataProtectionKey>("DataProtectionKeys");
            
            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(loggerFactory, collection);
            });
        });

        return builder;
    }
    
    /// <summary>
    /// Configures the data protection system to persist keys to an EntityFrameworkCore datastore
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> instance to modify.</param>
    /// <param name="mongoDatabaseFactory">The factory that creates a <see cref="IMongoDatabase"/> instance</param>
    /// <returns>The value <paramref name="builder"/>.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(this IDataProtectionBuilder builder, 
                                                              Func<IMongoClient, IMongoDatabase> mongoDatabaseFactory)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
            var client = services.GetRequiredService<IMongoClient>();
            var database = mongoDatabaseFactory(client);
            var collection = database.GetCollection<DataProtectionKey>("DataProtectionKeys");
            
            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(loggerFactory, collection);
            });
        });

        return builder;
    }
    
    /// <summary>
    /// Configures the data protection system to persist keys to an EntityFrameworkCore datastore
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> instance to modify.</param>
    /// <param name="mongoDatabaseFactory">The factory that creates a <see cref="IMongoDatabase"/> instance</param>
    /// <returns>The value <paramref name="builder"/>.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(this IDataProtectionBuilder builder, 
                                                              Func<IServiceProvider, IMongoDatabase> mongoDatabaseFactory)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
            var database = mongoDatabaseFactory(services);
            var collection = database.GetCollection<DataProtectionKey>("DataProtectionKeys");
            
            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(loggerFactory, collection);
            });
        });

        return builder;
    }
    
    /// <summary>
    /// Configures the data protection system to persist keys to an EntityFrameworkCore datastore
    /// </summary>
    /// <param name="builder">The <see cref="IDataProtectionBuilder"/> instance to modify.</param>
    /// <param name="factory">The factory that creates a <see cref="IMongoCollection{DataProtectionKey}"/> instance</param>
    /// <returns>The value <paramref name="builder"/>.</returns>
    public static IDataProtectionBuilder PersistKeysToMongoDb(this IDataProtectionBuilder builder, 
                                                              Func<IServiceProvider, IMongoCollection<DataProtectionKey>> factory)
    {
        builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
        {
            var loggerFactory = services.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
            var collection = factory(services);
            
            return new ConfigureOptions<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MongoDbXmlRepository(loggerFactory, collection);
            });
        });

        return builder;
    }
}