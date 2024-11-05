using System.Collections;
using System.Reflection;
using Myrtle.Abstractions.Configurations;

namespace Myrtle;

internal sealed class MongoConfigurationRegistry : IMongoConfigurationRegistry
{
    private readonly HashSet<IMongoConfiguration> _configurations = [];

    public IEnumerator<IMongoConfiguration> GetEnumerator() => _configurations.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc /> 
    public int Count => _configurations.Count;

    /// <inheritdoc /> 
    public void Register<T>() where T : IMongoConfiguration, new()
    {
        _configurations.Add(new T());
    }

    /// <inheritdoc /> 
    public void RegisterFromAssembly(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (typeof(IMongoConfiguration).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false })
            {
                var configurationInstance = Activator.CreateInstance(type) as IMongoConfiguration ??
                                            throw new NotSupportedException("Types that implement IMongoConfiguration must have a parameterless constructor.");

                _configurations.Add(configurationInstance);
            }
        }
    }
}