# Myrtle

![Myrtle Logo](assets/logo/logo-128x128.png)

Myrtle is a comprehensive collection of useful extensions and configurations for the official MongoDB C# driver. It aims to simplify and enhance the experience of working with MongoDB in .NET applications.

[![Build Status](https://github.com/litenova/Myrtle/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/litenova/Myrtle/actions/workflows/ci-cd.yml)
[![Coverage Status](https://coveralls.io/repos/github/litenova/Myrtle/badge.svg?branch=main)](https://coveralls.io/github/litenova/Myrtle?branch=main)

## Features

- **Enhanced Configuration**: Simplified setup for MongoDB with various conventions and serialization options.
- **Dependency Injection**: Easy integration with Microsoft.Extensions.DependencyInjection for ASP.NET Core applications.
- **Repository Pattern**: Generic repository implementation for streamlined data access.
- **Data Protection**: Support for storing ASP.NET Core Data Protection keys in MongoDB.
- **Extensible Architecture**: Modular design allowing for easy addition of new features and configurations.

## Packages

| Package | Version | Description |
|---------|---------|-------------|
| Myrtle.Abstractions | [![NuGet](https://img.shields.io/nuget/v/Myrtle.Abstractions.svg)](https://www.nuget.org/packages/Myrtle.Abstractions/) | Core abstractions and interfaces for Myrtle |
| Myrtle | [![NuGet](https://img.shields.io/nuget/v/Myrtle.svg)](https://www.nuget.org/packages/Myrtle/) | Main implementation of Myrtle extensions and configurations |
| Myrtle.Extensions.MicrosoftDependencyInjection | [![NuGet](https://img.shields.io/nuget/v/Myrtle.Extensions.MicrosoftDependencyInjection.svg)](https://www.nuget.org/packages/Myrtle.Extensions.MicrosoftDependencyInjection/) | Integration with Microsoft.Extensions.DependencyInjection |
| Myrtle.AspNetCore.DataProtection.Keys | [![NuGet](https://img.shields.io/nuget/v/Myrtle.AspNetCore.DataProtection.Keys.svg)](https://www.nuget.org/packages/Myrtle.AspNetCore.DataProtection.Keys/) | Support for storing ASP.NET Core Data Protection keys in MongoDB |

## Installation

You can install Myrtle packages via NuGet Package Manager or .NET CLI.

```bash
dotnet add package Myrtle
dotnet add package Myrtle.Extensions.MicrosoftDependencyInjection
dotnet add package Myrtle.AspNetCore.DataProtection.Keys
```

## Key Interfaces and Abstractions

Myrtle provides several key interfaces and abstractions to simplify working with MongoDB:

- `IMongoConnection`: Represents a connection to a MongoDB server.
- `IMongoDatabaseContext`: Provides access to a specific MongoDB database.
- `IMongoCollectionContext<TDocument>`: Represents a MongoDB collection for a specific document type.
- `IMongoRepository<TDocument, TId>`: Defines a generic repository pattern for MongoDB operations.
- `IMongoConfigurationRegistry`: Allows registration of custom MongoDB configurations.
- `IMongoConfiguration`: Represents a specific MongoDB configuration.

## Usage

### Basic Setup

1. Add MongoDB services to your application's service collection:

```csharp
services.AddMongoDB(options =>
{
    options.ConnectionString = "mongodb://localhost:27017";
    options.DatabaseName = "MyDatabase";
});
```

2. Configure MongoDB with custom configurations:

```csharp
services.AddMongoDBConfigurations(registry =>
{
    registry.AddUtcDateTimeSerialization()
            .AddDecimalSerialization()
            .AddEnumRepresentation()
            .AddIgnoreExtraElements()
            .AddAllConfigurations(); // Adds all available configurations
});
```

### Using the Repository Pattern

1. Define your aggregate root class:

```csharp
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

2. Create a custom repository by inheriting from `MongoRepository<TDocument, TId>`:

```csharp
public class UserRepository : MongoRepository<User, Guid>, IUserRepository
{
    public UserRepository(IMongoCollectionContext<User> collectionContext) 
        : base(collectionContext)
    {
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
}

public interface IUserRepository : IMongoRepository<User, Guid>
{
    Task<User> FindByEmailAsync(string email);
}
```

3. Register your custom repository in the dependency injection container:

```csharp
services.AddScoped<IUserRepository, UserRepository>();
```

4. Use the repository in your services:

```csharp
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.FindByEmailAsync(email);
    }

    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddAsync(user);
    }
}
```

### Data Protection Key Storage

To configure ASP.NET Core Data Protection to store keys in MongoDB:

```csharp
services.AddDataProtection()
    .PersistKeysToMongoDb(mongoClient, "MyDatabase", "DataProtectionKeys");
```

## License

Myrtle is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Support

If you encounter any issues or have questions, please [open an issue](https://github.com/litenova/Myrtle/issues) on GitHub.