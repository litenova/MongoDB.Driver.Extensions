using Myrtle.Abstractions.Configurations;
using Myrtle.Configurations;

namespace Myrtle.Extensions;

/// <summary>
/// Provides extension methods for IMongoConfigurationRegistry to easily add specific configurations.
/// </summary>
public static class MongoConfigurationRegistryExtensions
{
    /// <summary>
    /// Adds the DecimalConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddDecimalSerialization(this IMongoConfigurationRegistry registry)
    {
        registry.Register<DecimalConfiguration>();
        return registry;
    }

    /// <summary>
    /// Adds the EnumRepresentationConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddEnumRepresentation(this IMongoConfigurationRegistry registry)
    {
        registry.Register<EnumRepresentationConfiguration>();
        return registry;
    }

    /// <summary>
    /// Adds the IgnoreExtraElementsConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddIgnoreExtraElements(this IMongoConfigurationRegistry registry)
    {
        registry.Register<IgnoreExtraElementsConfiguration>();
        return registry;
    }

    /// <summary>
    /// Adds the ImmutableTypeConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddImmutableTypeSerialization(this IMongoConfigurationRegistry registry)
    {
        registry.Register<ImmutableTypeConfiguration>();
        return registry;
    }

    /// <summary>
    /// Adds the GuidSerializationConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddGuidSerialization(this IMongoConfigurationRegistry registry)
    {
        registry.Register<GuidSerializationConfiguration>();
        return registry;
    }

    /// <summary>
    /// Adds the CamelCaseElementNameConvention to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddCamelCaseElementNames(this IMongoConfigurationRegistry registry)
    {
        registry.Register<CamelCaseElementNameConvention>();
        return registry;
    }

    /// <summary>
    /// Adds the IgnoreIfNullConvention to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddIgnoreIfNull(this IMongoConfigurationRegistry registry)
    {
        registry.Register<IgnoreIfNullConventionConfiguration>();
        return registry;
    }

    /// <summary>
    /// Adds the StringObjectIdIdGeneratorConvention to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddStringObjectIdGenerator(this IMongoConfigurationRegistry registry)
    {
        registry.Register<StringObjectIdIdGeneratorConventionConfiguration>();
        return registry;
    }

    /// <summary>
    /// Adds the UtcDateTimeSerializationConfiguration to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configuration to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddUtcDateTimeSerialization(this IMongoConfigurationRegistry registry)
    {
        registry.Register<UtcDateTimeSerializationConfiguration>();
        return registry;
    }

    /// <summary>
    /// Adds all available configurations to the registry.
    /// </summary>
    /// <param name="registry">The IMongoConfigurationRegistry to add the configurations to.</param>
    /// <returns>The IMongoConfigurationRegistry for method chaining.</returns>
    public static IMongoConfigurationRegistry AddAllConfigurations(this IMongoConfigurationRegistry registry)
    {
        return registry
            .AddDecimalSerialization()
            .AddEnumRepresentation()
            .AddIgnoreExtraElements()
            .AddImmutableTypeSerialization()
            .AddGuidSerialization()
            .AddCamelCaseElementNames()
            .AddIgnoreIfNull()
            .AddStringObjectIdGenerator()
            .AddUtcDateTimeSerialization();
    }
}