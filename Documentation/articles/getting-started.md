# Getting Started with Blackwood.System.Text.Json

This guide will help you get up and running with Blackwood.System.Text.Json
quickly and easily.

## Prerequisites

- **.NET 8.0 or later** - The library targets .NET 8.0+
- **Visual Studio 2022** (Windows) or **VS Code** with C# extension (cross-platform)

## Installation

### Package Manager Console

```powershell
Install-Package Blackwood.System.Text.Json
```

### .NET CLI

```bash
dotnet add package Blackwood.System.Text.Json
```

### PackageReference

Add the following to your `.csproj` file:

```xml
<PackageReference Include="Blackwood.System.Text.Json" Version="2.0.0" />
```

## Quick Start

### 1. Add the Using Statement

```csharp
using Blackwood;
```

### 2. Basic Deserialization

The simplest way to use Blackwood.System.Text.Json is with the
`JSONDeserializer.Deserialize<T>()` method:

```csharp
using Blackwood;

// JSON with some common issues that standard System.Text.Json would struggle with
var json = """
{
    "name": "John Doe",
    "age": "30",           // String number - automatically converted to int
    "isActive": "True",    // String boolean - automatically converted to bool
    "salary": 75000.50,    // Regular number
    "department": "IT"     // Regular string
}
""";

// Define your model class to match the JSON structure
public class Employee
{
    public string Name { get; set; }      // Maps to "name" property
    public int Age { get; set; }          // Maps to "age" (string converted to int)
    public bool IsActive { get; set; }    // Maps to "isActive" (string converted to bool)
    public decimal Salary { get; set; }   // Maps to "salary" property
    public string Department { get; set; } // Maps to "department" property
}

// Deserialize JSON to the Employee object (automatic type conversion)
var employee = JSONDeserializer.Deserialize<Employee>(json);

// Display the results to verify type conversion worked
Console.WriteLine($"Name: {employee.Name}");
Console.WriteLine($"Age: {employee.Age} (type: {employee.Age.GetType().Name})");
Console.WriteLine($"Active: {employee.IsActive} (type: {employee.IsActive.GetType().Name})");
```


## Key Features in Action

### Case-Insensitive Property Matching

```csharp
// JSON with inconsistent property name casing
var json = """
{
    "FirstName": "John",
    "lastname": "Doe",      // Different casing from LastName
    "EMAIL": "john@example.com"  // Different casing from Email
}
""";

// Model class with standard PascalCase property names
public class Person
{
    public string FirstName { get; set; }  // Matches "FirstName"
    public string LastName { get; set; }   // Matches "lastname" (case-insensitive)
    public string Email { get; set; }      // Matches "EMAIL" (case-insensitive)
}

// Deserialize to the Person object.
// This works despite case mismatches - library handles case-insensitive matching
var person = JSONDeserializer.Deserialize<Person>(json);
```

### JSON with Comments and Trailing Commas

```csharp
// JSON with comments and trailing comma (normally invalid JSON)
var jsonWithComments = """
{
    // This is a comment - normally not allowed in JSON
    "name": "John",
    "age": 30,
    "city": "New York", // Trailing comma - normally causes parsing errors!
}
""";

// Deserialize JSON with comments and trailing commas (library handles both)
var person = JSONDeserializer.Deserialize<Person>(jsonWithComments);
```

### Flexible Type Conversion

Sometimes you will wish to work with JSON that might use different types from
the strict set used in .Net.  The tools can automatically convert between these.

```csharp
// JSON with string values that need type conversion
var json = """
{
    "id": "123",           // String to int conversion
    "price": "29.99",      // String to decimal conversion
    "inStock": "true",     // String to bool conversion
    "tags": ["new", "sale"] // Array handling (no conversion needed)
}
""";

// Model class with strongly-typed properties
public class Product
{
    public int Id { get; set; }           // Will receive converted int value
    public decimal Price { get; set; }    // Will receive converted decimal value
    public bool InStock { get; set; }     // Will receive converted bool value
    public string[] Tags { get; set; }    // Will receive string array
}

// Deserialize with automatic type conversion
var product = JSONDeserializer.Deserialize<Product>(json);
```


## Using Pre-configured Options

The library provides pre-configured `JsonSerializerOptions` optimized for common scenarios:

```csharp
// Use the pre-configured options (includes all enhanced features)
var options = JSONDeserializer.JSONOptions;

// Or create custom options based on the pre-configured ones
var customOptions = new JsonSerializerOptions(JSONDeserializer.JSONOptions)
{
    WriteIndented = true,                                    // Pretty-print JSON
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase       // Convert property names to camelCase
};
```


## Next Steps

- [API Reference](../api/index.md) - Explore the full API for detailed method documentation
- [Examples](examples.md) - for common patterns

## Troubleshooting

**Q: My properties aren't being populated**
A: Check that your JSON property names match your C# property names (case-insensitive matching is enabled by default).

**Q: I'm getting type conversion errors**
A: The library handles most common conversions automatically. For complex types, consider using custom converters.
