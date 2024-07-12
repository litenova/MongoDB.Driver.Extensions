using Myrtle.Abstractions.Configurations;
using Myrtle.Configurations;

namespace Myrtle.Extensions;

/// <summary>
/// Provides extension methods for IMongoConfigurationRegistry to easily register specific configurations.
/// </summary>
public static class MongoConfigurationRegistryExtensions
{
    /// <summary>
    /// Registers the DecimalConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterDecimalSerializationConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<DecimalConfiguration>();
        return registry;
    }

    /// <summary>
    /// Registers the EnumRepresentationConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterEnumRepresentationConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<EnumRepresentationConfiguration>();
        return registry;
    }

    /// <summary>
    /// Registers the IgnoreExtraElementsConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterIgnoreExtraElementsConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<IgnoreExtraElementsConfiguration>();
        return registry;
    }

    /// <summary>
    /// Registers the ImmutableTypeConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterImmutableTypeSerializationConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<ImmutableTypeConfiguration>();
        return registry;
    }

    /// <summary>
    /// Registers the GuidSerializationConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterGuidSerializationConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<GuidSerializationConfiguration>();
        return registry;
    }

    /// <summary>
    /// Registers the CamelCaseElementNameConvention to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterCamelCaseElementNamesConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<CamelCaseElementNameConvention>();
        return registry;
    }

    /// <summary>
    /// Registers the IgnoreIfNullConvention to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterIgnoreIfNullConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<IgnoreIfNullConventionConfiguration>();
        return registry;
    }

    /// <summary>
    /// Registers the StringObjectIdIdGeneratorConvention to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterStringObjectIdGeneratorConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<StringObjectIdIdGeneratorConventionConfiguration>();
        return registry;
    }

    /// <summary>
    /// Registers the UtcDateTimeSerializationConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterUtcDateTimeSerializationConfiguration(this IMongoConfigurationRegistry registry)
    {
        registry.Register<UtcDateTimeSerializationConfiguration>();
        return registry;
    }

    /// <summary>
    /// Registers all available configurations to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to register the configurations to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry RegisterAllConfigurations(this IMongoConfigurationRegistry registry)
    {
        return registry
            .RegisterDecimalSerializationConfiguration()
            .RegisterEnumRepresentationConfiguration()
            .RegisterIgnoreExtraElementsConfiguration()
            .RegisterImmutableTypeSerializationConfiguration()
            .RegisterGuidSerializationConfiguration()
            .RegisterCamelCaseElementNamesConfiguration()
            .RegisterIgnoreIfNullConfiguration()
            .RegisterStringObjectIdGeneratorConfiguration()
            .RegisterUtcDateTimeSerializationConfiguration();
    }
}