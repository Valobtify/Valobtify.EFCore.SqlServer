[![NuGet Package](https://img.shields.io/nuget/v/Valobtify.EfCore.SqlServer)](https://www.nuget.org/packages/Valobtify.EfCore.SqlServer/)

### Table of Contents

- [Overview](#overview)
- [Installation](#installation)
- [Setting Up](#Setting-Up)
- [Configuring Max Length](#Configuring-Max-Length)

---


### Overview
`Valobtify.EFCore.SqlServer` is an extension of the `Valobtify` library that simplifies the configuration and persistence of single-value objects in Entity Framework Core. It automates the application of data annotations such as `MaxLength` and handles type conversions, making your value objects database-ready with minimal setup.

---

### Installation

To install the `Valobtify.EFCore.SqlServer` package, use the following command in your terminal:

```bash
dotnet add package Valobtify.EFCore.SqlServer
```  

Ensure that you have the required .NET SDK installed.

---

### Setting Up

To configure single-value objects in your Entity Framework Core `DbContext`, override the `OnModelCreating` method as shown below:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.SetupSingleValueObjects();

    base.OnModelCreating(modelBuilder);
}
```  

#### Explanation

The `SetupSingleValueObjects` method automatically applies the necessary configurations for your single-value objects, including data annotations and type conversions. By calling this method, you ensure your value objects are properly mapped to the database schema.

---

### Configuring Max Length

You can configure the maximum length for single-value objects in two ways:

#### 1. Using `MaxLengthAttribute`

```csharp
public sealed class Name : SingleValueObject<Name, string>, ICreatableValueObject<Name, string>
{
    [MaxLength(20)]
    public override string Value { get; }

    public Name(string value) : base(value)
    {
        Value = value;
    }

    public static Result<Name> Create(string value)
    {
        if (value.Length < 3) return new ResultError("Value is not valid");

        return new Name(value);
    }
}
```  

#### 2. Using `SetupSingleValueObjects()` in `DbContext.OnModelCreating`

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.SetupSingleValueObjects(action =>
    {
        action.SetupMaxLength<Name>(20);
    });

    base.OnModelCreating(modelBuilder);
}
```  

By following these steps, you can easily integrate and manage single-value objects in your Entity Framework Core project using the `Valobtify.EFCore.SqlServer` package.  