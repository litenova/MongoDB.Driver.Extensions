using MongoDB.Bson.Serialization.Conventions;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Configures MongoDB to use ObjectId for string ID properties.
/// </summary>
/// <remarks>
/// This configuration sets up a convention to automatically use ObjectId as the
/// serialization and generation mechanism for string properties named "Id" (case-insensitive).
/// This allows for easier integration between MongoDB's native ObjectId type and
/// C# string representations of identifiers.
/// 
/// Use cases include:
/// <list type="bullet">
/// <item><description>Simplifying the use of MongoDB's ObjectId while keeping string IDs in C# models.</description></item>
/// <item><description>Automating ID generation for new documents without explicit ObjectId handling in C# code.</description></item>
/// <item><description>Maintaining compatibility with systems expecting string IDs while leveraging MongoDB's ObjectId internally.</description></item>
/// </list>
/// 
/// Considerations:
/// <list type="bullet">
/// <item><description>This convention only applies to string properties named "Id" (case-insensitive).</description></item>
/// <item><description>It may change how IDs are generated and stored, which could affect existing data or queries.</description></item>
/// <item><description>Care should be taken when using this in systems with custom ID generation logic.</description></item>
/// </list>
/// </remarks>
public sealed class StringObjectIdIdGeneratorConventionConfiguration : IMongoConfiguration
{
    /// <summary>
    /// Configures MongoDB to use ObjectId for string ID properties.
    /// </summary>
    /// <remarks>
    /// This method registers a StringObjectIdIdGeneratorConvention with MongoDB's ConventionRegistry.
    /// After applying this configuration:
    /// <list type="bullet">
    /// <item><description>String properties named "Id" (case-insensitive) will be treated as ObjectId in MongoDB.</description></item>
    /// <item><description>New documents will automatically get a generated ObjectId for their string Id property.</description></item>
    /// <item><description>This behavior applies globally to all classes with a string Id property in the application.</description></item>
    /// </list>
    /// </remarks>
    public void Configure()
    {
        ConventionRegistry.Register("StringObjectIdIdGenerator", new ConventionPack { new StringObjectIdIdGeneratorConvention() }, _ => true);
    }
}