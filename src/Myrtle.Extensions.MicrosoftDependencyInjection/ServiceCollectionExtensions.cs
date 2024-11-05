using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Myrtle.Abstractions;
using Myrtle.Abstractions.Configurations;
using Myrtle.Abstractions.Options;
using Myrtle.Abstractions.Repositories;

namespace Myrtle.Extensions.MicrosoftDependencyInjection;

/// <summary>
/// Provides extension methods for IServiceCollection to configure MongoDB services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds MongoDB services to the specified IServiceCollection with a custom options configuration.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="setupAction">An Action to configure the MongoDbOptions.</param>
    /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
    /// <remarks>
    /// This overload allows for in-line configuration of MongoDB options using a setup action.
    /// It's useful for scenarios where options need to be configured programmatically.
    /// </remarks>
    public static IServiceCollection AddMongoDB(this IServiceCollection services, Action<MongoDbOptions> setupAction)
    {
        var options = new MongoDbOptions();
        setupAction(options);
        return services.AddMongoDB(options);
    }

    /// <summary>
    /// Adds MongoDB services to the specified IServiceCollection with the provided options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="options">The MongoDB options.</param>
    /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
    /// <remarks>
    /// This method adds the core MongoDB services to the dependency injection container,
    /// including IMongoConnection, IMongoDatabaseContext, and generic repositories.
    /// </remarks>
    public static IServiceCollection AddMongoDB(this IServiceCollection services, MongoDbOptions options)
    {
        services.AddSingleton<IMongoOptionsProvider>(options);
        services.AddSingleton<IMongoConnectionStringProvider>(options);
        services.AddSingleton<IMongoDatabaseNameProvider>(options);

        return services.AddMongoDBCore();
    }

    /// <summary>
    /// Adds core MongoDB services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
    /// <remarks>
    /// This method is called internally to register the core MongoDB services.
    /// It sets up IMongoConnection, IMongoDatabaseContext, IMongoCollectionContext, and IMongoRepository.
    /// </remarks>
    private static IServiceCollection AddMongoDBCore(this IServiceCollection services)
    {
        services.AddSingleton<IMongoConnection, MongoConnection>();
        services.AddSingleton<IMongoClient>(provider =>
        {
            var mongoConnection = provider.GetRequiredService<IMongoConnection>();
            return mongoConnection.Client;
        });
        services.AddSingleton<IMongoDatabaseContext, MongoDatabaseContext>();
        services.AddSingleton<IMongoDatabase>(provider =>
        {
            var mongoDatabaseContext = provider.GetRequiredService<IMongoDatabaseContext>();
            return mongoDatabaseContext.Database;
        });

        services.AddScoped(typeof(IMongoCollectionContext<>), typeof(MongoCollectionContext<>));
        services.AddScoped(typeof(IMongoRepository<,>), typeof(MongoRepository<,>));
        services.AddScoped<IMongoTransactionContext, MongoTransactionContext>();

        return services;
    }

    /// <summary>
    /// Configures MongoDB with custom configurations.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <param name="configureRegistry">An action to configure the MongoDB configuration registry.</param>
    /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
    /// <remarks>
    /// This method allows for custom configuration of MongoDB beyond the basic setup.
    /// It's used to apply specific conventions or serialization configurations.
    /// The configureRegistry action is where you can add or remove specific configurations.
    /// </remarks>
    public static IServiceCollection AddMongoDBConfigurations(this IServiceCollection services, Action<IMongoConfigurationRegistry> configureRegistry)
    {
        var registry = new MongoConfigurationRegistry();
        configureRegistry(registry);

        foreach (var configuration in registry)
        {
            configuration.Configure();
        }

        return services;
    }
}