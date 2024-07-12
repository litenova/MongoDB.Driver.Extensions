namespace Myrtle.Abstractions.Configurations;

/// <summary>
/// Defines a contract for MongoDB configuration components.
/// </summary>
/// <remarks>
/// This interface is used to create modular configuration units for MongoDB.
/// Each implementation of this interface represents a specific aspect of MongoDB configuration,
/// such as serialization conventions, index creation, read/write concerns, or other MongoDB-specific settings.
/// 
/// Implementations of this interface are typically registered with an <see cref="IMongoConfigurationRegistry"/>
/// and applied during the application's startup process.
/// 
/// Example uses include:
/// <list type="bullet">
/// <item><description>Setting up custom serializers for specific types</description></item>
/// <item><description>Configuring global read/write concerns</description></item>
/// <item><description>Establishing naming conventions for collections or fields</description></item>
/// <item><description>Setting up automatic index creation for certain collections</description></item>
/// </list>
/// </remarks>
public interface IMongoConfiguration
{
    /// <summary>
    /// Applies the configuration to the MongoDB driver.
    /// </summary>
    /// <remarks>
    /// This method is called to execute the configuration logic.
    /// It should contain all necessary code to set up the specific MongoDB configuration aspect.
    /// 
    /// Implementations should be idempotent, as they may be called multiple times during
    /// the application lifecycle, depending on how the configuration system is set up.
    /// 
    /// Any exceptions thrown during the configuration process should be meaningful and
    /// help diagnose configuration issues.
    /// </remarks>
    void Configure();
}