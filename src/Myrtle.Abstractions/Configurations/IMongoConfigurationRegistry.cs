using System.Reflection;

namespace Myrtle.Abstractions.Configurations;

/// <summary>
/// Defines a contract for a registry that manages MongoDB configurations.
/// </summary>
/// <remarks>
/// This interface extends IReadOnlyCollection{IMongoConfiguration}, providing enumeration
/// capabilities over the registered configurations. It allows for programmatic registration
/// of individual configuration classes and bulk registration from assemblies.
/// 
/// The registry is typically used during application startup to configure various aspects
/// of MongoDB behavior, such as serialization conventions, type mappings, or other global settings.
/// </remarks>
public interface IMongoConfigurationRegistry : IReadOnlyCollection<IMongoConfiguration>
{
    /// <summary>
    /// Registers a new configuration of type T.
    /// </summary>
    /// <typeparam name="T">The type of configuration to register. Must implement IMongoConfiguration and have a parameterless constructor.</typeparam>
    /// <remarks>
    /// This method creates a new instance of the specified configuration type and adds it to the registry.
    /// It's useful for registering individual, known configuration types.
    /// </remarks>
    void Register<T>() where T : IMongoConfiguration, new();

    /// <summary>
    /// Registers all IMongoConfiguration implementations found in the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan for IMongoConfiguration implementations.</param>
    /// <remarks>
    /// This method scans the provided assembly for all non-abstract classes that implement IMongoConfiguration,
    /// instantiates them, and adds them to the registry. It's useful for bulk registration of configurations
    /// from a module or plugin assembly.
    /// </remarks>
    void RegisterFromAssembly(Assembly assembly);
}