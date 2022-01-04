<h1 align="center">
  <br>
  <a href="https://github.com/litenova/Myrtle">
    <img src="assets/logo/logo-128x128.png">
  </a>
  <br>
  Myrtle
  <br>
</h1>

<h4 align="center">
Myrtle is a collection of useful extensions to official MongoDB C# driver.
</h4>

<p align="center">
   <a href='https://github.com/litenova/Myrtle/actions/workflows/release.yml'>
    <img src='https://github.com/litenova/Myrtle/actions/workflows/release.yml/badge.svg' alt='Coverage Status' />
  </a>
   <a href='https://coveralls.io/github/litenova/Myrtle?branch=main'>
    <img src='https://coveralls.io/repos/github/litenova/Myrtle/badge.svg?branch=main' alt='Coverage Status' />
  </a>

</p>

## Overview

Below you can find the overview and usages of each extension.

* `Myrtle.AspNetCore.DataProtection.Keys` Adds support for storing ASP.Net Core data protection keys using MongoDb.Driver into a mongo database.
* `Myrtle.Configuration [To Be Implemnted]` Provides integration of MongoDb.Driver with ASP.NET Core startup configuration.  

## Myrtle.AspNetCore.DataProtection.Keys 

[![Nuget](https://img.shields.io/nuget/v/Myrtle.AspNetCore.DataProtection.Keys)](https://www.nuget.org/packages/Myrtle.AspNetCore.DataProtection.Keys/)


Adds support for storing ASP.Net Core data protection keys using MongoDb.Driver into a mongo database.


### Installation

Using dotnet cli:

```
dotnet add package Myrtle.AspNetCore.DataProtection.Keys
```

Using nuget package manager console:

```
Install-Package Myrtle.AspNetCore.DataProtection.Keys
```

### Usages

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDataProtection()
            .PersistKeysToMongoDb();
}
```
