# Examples and Recommendations

This article provides examples using Blackwood.System.Text.Json.

## Table of Contents

- [Basic Usage Patterns](#basic-usage-patterns)
- [Working with Complex JSON](#working-with-complex-json)
- [Type Conversion Examples](#type-conversion-examples)
- [Recommendations](#recommendations)

## Basic Usage Patterns

### Simple Object Deserialization

Below is a simple example demonstrating deserialization with automatic type
conversion to match the correct property types of your C# object.


```csharp
using Blackwood;

// Basic user data with mixed data types (strings and actual types)
var userJson = """
{
    "id": "12345",        // String that will be converted to int
    "name": "John Doe",   // Regular string
    "email": "john@example.com",
    "isActive": "true",   // String that will be converted to bool
    "lastLogin": "2024-01-15T10:30:00Z"  // ISO 8601 date string
}
""";

// Model class with strongly-typed properties
public class User
{
    public int Id { get; set; }           // Will receive converted int from "12345"
    public string Name { get; set; }      // Direct string mapping
    public string Email { get; set; }     // Direct string mapping
    public bool IsActive { get; set; }    // Will receive converted bool from "true"
    public DateTime LastLogin { get; set; } // Will parse ISO 8601 date string
}

// Deserialize with automatic type conversion
var user = JSONDeserializer.Deserialize<User>(userJson);
```

### Working with Arrays

Here is an example showing how deserialization works with an array.

```csharp
// JSON array with multiple objects containing string values that need conversion
var productsJson = """
[
    {
        "id": "1",           // String to int conversion
        "name": "Laptop",    // Direct string
        "price": "999.99",   // String to decimal conversion
        "inStock": "true"    // String to bool conversion
    },
    {
        "id": "2",           // String to int conversion
        "name": "Mouse",     // Direct string
        "price": "29.99",    // String to decimal conversion
        "inStock": "false"   // String to bool conversion
    }
]
""";

// Product model class
public class Product
{
    public int Id { get; set; }           // Will receive converted int values
    public string Name { get; set; }      // Direct string mapping
    public decimal Price { get; set; }    // Will receive converted decimal values
    public bool InStock { get; set; }     // Will receive converted bool values
}

// Deserialize array of products with automatic type conversion
var products = JSONDeserializer.Deserialize<Product[]>(productsJson);
```

## Working with Complex JSON

Now lets look at more complex structures, such as deeply nested objects, arrays,
and collections.


### Nested Objects and Collections

Below is an example featuring a deeply nested object structure that includes
collections.

```csharp
// Complex nested JSON with multiple levels and mixed data types
var orderJson = """
{
    "orderId": "ORD-001",     // Direct string
    "customer": {             // Nested object
        "id": "123",          // String to int conversion
        "name": "Alice Smith",
        "email": "alice@example.com"
    },
    "items": [                // Array of nested objects
        {
            "productId": "P001",
            "quantity": "2",      // String to int conversion
            "unitPrice": "19.99"  // String to decimal conversion
        },
        {
            "productId": "P002",
            "quantity": "1",      // String to int conversion
            "unitPrice": "49.99"  // String to decimal conversion
        }
    ],
    "shipping": {             // Another nested object
        "address": "123 Main St",
        "city": "New York",
        "zipCode": "10001"    // String (could be converted to int if needed)
    },
    "total": "89.97"          // String to decimal conversion
}
""";

// Main order class with nested object properties
public class Order
{
    public string OrderId { get; set; }        // Direct string mapping
    public Customer Customer { get; set; }     // Nested object
    public List<OrderItem> Items { get; set; } // Collection of nested objects
    public ShippingInfo Shipping { get; set; } // Nested object
    public decimal Total { get; set; }         // Will receive converted decimal
}

// Customer nested class
public class Customer
{
    public int Id { get; set; }      // Will receive converted int
    public string Name { get; set; } // Direct string mapping
    public string Email { get; set; } // Direct string mapping
}

// Order item nested class
public class OrderItem
{
    public string ProductId { get; set; }  // Direct string mapping
    public int Quantity { get; set; }      // Will receive converted int
    public decimal UnitPrice { get; set; } // Will receive converted decimal
}

// Shipping info nested class
public class ShippingInfo
{
    public string Address { get; set; }  // Direct string mapping
    public string City { get; set; }     // Direct string mapping
    public string ZipCode { get; set; }  // Direct string mapping
}

// Deserialize complex nested structure with automatic type conversion
var order = JSONDeserializer.Deserialize<Order>(orderJson);
```

### Dynamic JSON with Unknown Structure

When you are working with JSON data whose structure is not known -- or fully
known -- at compile time you can deserialize them into an `object` type.  For
instance processing configuration files or loosely-typed APIs.
This will deserialize into a dynamic object:

```csharp
// JSON with unknown structure - perfect for configuration files
var dynamicJson = """
{
    "config": {
        "database": {
            "host": "localhost",
            "port": "5432",        // String that can be converted to int
            "name": "myapp"
        },
        "features": {
            "enableLogging": "true",    // String that can be converted to bool
            "maxConnections": "100",    // String that can be converted to int
            "timeout": "30"            // String that can be converted to int
        }
    },
    "metadata": {
        "version": "1.0.0",
        "environment": "production"
    }
}
""";

// Deserialize to dynamic object when structure is unknown at compile time
var config = JSONDeserializer.Deserialize<object>(dynamicJson);

// Access nested properties with automatic type conversion
var host = config.config.database.host; // "localhost" (string)
var port = config.config.database.port; // 5432 (converted from "5432" string to int)
var enableLogging = config.config.features.enableLogging; // true (converted from "true" string to bool)
```

## Type Conversion Examples

The `JSONConvert` class provides tools to aid converting from JSON values (often
strings) to the expected .NET types in your classes or variables.

```csharp
// Convert various string types to their .NET equivalents
var intValue = JSONConvert.ToInt("123");           // 123 (string to int)
var floatValue = JSONConvert.ToFloat("45.67");     // 45.67f (string to float)
var boolValue = JSONConvert.ToBool("true");        // true (string to bool)
var boolValue2 = JSONConvert.ToBool("True");       // true (case insensitive conversion)

// Parse colors from various string formats
var color1 = JSONConvert.TryParseColor("#FF0000");     // Red (hex format)
var color2 = JSONConvert.TryParseColor("blue");        // Blue (named color)
var color3 = JSONConvert.TryParseColor("#00FF0080");   // Green with alpha (hex with alpha)

// Convert dictionary keys to lowercase for consistent casing
var dict = new Dictionary<string, object>
{
    ["FirstName"] = "John",
    ["LastName"] = "Doe",
    ["Email"] = "john@example.com"
};

// Convert all keys to lowercase
var lowerCaseDict = JSONConvert.LowerCaseKeys(dict);
// Result: { "firstname": "John", "lastname": "Doe", "email": "john@example.com" }
```



## Recommendations

1. Use strongly-typed models when possible
2. Always handle potential null values
3. Wrap deserialization in try-catch blocks
4. The library converts types, but validate your assumptions

### 1. Use Strongly-Typed Models When Possible

With strongly-typeed models, the deserialization converter has better information
how to deserialize the data.

```csharp
// Good: Strongly-typed model provides compile-time safety and IntelliSense
public class User
{
    public int Id { get; set; }      // Strongly-typed property
    public string Name { get; set; } // Strongly-typed property
    public string Email { get; set; } // Strongly-typed property
}

// Deserialize to strongly-typed object
var user = JSONDeserializer.Deserialize<User>(json);

// Avoid: Dynamic objects when structure is known (loses type safety)
var user = JSONDeserializer.Deserialize<object>(json);
```

### 2. Use Appropriate Data Types

```csharp
// Good: Use appropriate types for the data to enable proper operations
public class Product
{
    public int Id { get; set; }           // Integer for IDs (enables numeric operations)
    public decimal Price { get; set; }    // Decimal for money (prevents floating-point errors)
    public bool InStock { get; set; }     // Boolean for flags (enables logical operations)
    public DateTime CreatedAt { get; set; } // DateTime for dates (enables date operations)
}

// Avoid: Using strings for everything (loses type safety and functionality)
public class BadProduct
{
    public string Id { get; set; }        // Should be int (can't do math operations)
    public string Price { get; set; }     // Should be decimal (can't do calculations)
    public string InStock { get; set; }   // Should be bool (can't use in conditions)
}
```

### 3. Handle Null Values Gracefully

It's important to handle null values gracefully during deserialization.
JSON data might have missing fields, or incoming data could be incomplete or malformed.
If you don't check for nulls, you may run into `NullReferenceException`s or application errors.


When handling null values in a class, use nullable types and provide sensible
default property values to avoid runtime errors:

```csharp
// User class with null-safe defaults to prevent null reference exceptions
public class SafeUser
{
    public int Id { get; set; }                              // Required field
    public string Name { get; set; } = string.Empty;         // Default to empty string
    public string Email { get; set; } = string.Empty;        // Default to empty string
    public DateTime? LastLogin { get; set; }                 // Nullable for optional field
    public List<string> Tags { get; set; } = new List<string>(); // Initialize empty list
}
```


The following section demonstrates how to safely invoke the deserializer and handle cases where the resulting object or its properties might be null, preventing runtime errors and enabling robust error handling in your application.


```csharp
// Example: Graceful null handling after deserialization
var user = JSONDeserializer.Deserialize<User>(json);

if (user == null)
{
    // Handle null object appropriately
    Console.WriteLine("Deserialization failed: user is null.");
    return;
}

// Check for required properties that may be null or missing
if (string.IsNullOrEmpty(user.Email))
{
    Console.WriteLine("No email address provided; using default.");
    user.Email = "unknown@example.com";
}

// Alternative: Use null-conditional and null-coalescing operators
string displayName = user?.Name ?? "Guest";
DateTime lastLogin = user?.LastLogin ?? DateTime.MinValue;
```



### 4. Implement Proper Error Handling

```csharp
public class RobustJsonProcessor
{
    public async Task<T> ProcessJsonAsync<T>(string json, T defaultValue = default)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return defaultValue;
        }

        try
        {
            return JSONDeserializer.Deserialize<T>(json);
        }
        catch (JsonException ex)
        {
            // Log the error with context
            Logger.LogError(ex, "Failed to deserialize JSON: {Json}", json);
            return defaultValue;
        }
    }
}
```


## Next Steps

- [API Reference](../api/index.md) - Explore the complete API
- [Getting Started](getting-started.md) - Review the basics if needed
