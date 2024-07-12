using MongoDB.Bson.Serialization.Conventions;
using Myrtle.Abstractions.Configurations;

namespace Myrtle.Configurations;

/// <summary>
/// Applies camel casing to element names when serializing to MongoDB.
/// </summary>
public sealed class CamelCaseElementNameConvention : IMongoConfiguration
{
    /// <summary>
    /// Configures the camel case element name convention.
    /// </summary>
    public void Configure()
    {
        ConventionRegistry.Register("CamelCase",
            new ConventionPack { new MongoDB.Bson.Serialization.Conventions.CamelCaseElementNameConvention() },
            _ => true);
    }
}