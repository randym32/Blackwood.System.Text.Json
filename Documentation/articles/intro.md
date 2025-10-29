# Introduction to Blackwood.System.Text.Json

**Blackwood.System.Text.Json** is a library to extend the standard
[`System.Text.Json`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json)
functionality in .NET. It provides added support for JSON parsing,
flexible type conversion, and improved handling of common JSON patterns.

- **JSON comment support** - The defaults here allows comments and documentation.
- **Trailing comma tolerance** - The defaults here allow trailing commas.
- **Type conversion** - Smart conversion of numbers, booleans, and strings.  For instance "True"/"False" (yes/no) strings can be converted to booleans

## Getting Started

The library is designed to be a drop-in enhancement to `System.Text.Json`. You
can start using it immediately with minimal code changes:

```csharp
using Blackwood;

// Simple deserialization with enhanced features
var json = """
{
    "name": "John Doe",
    "age": "30",        // String number - automatically converted
    "isActive": "True", // String boolean - automatically converted
    "settings": {
        "theme": "dark",
        "notifications": true
    }
}
""";

// Deserialize JSON with automatic type conversion
// The library handles string-to-number and string-to-boolean conversion
var result = JSONDeserializer.Deserialize<Person>(json);
```


## Core Components

- [`JSONDeserializer`](xref:Blackwood.JSONDeserializer) -- The main class providing enhanced JSON deserialization capabilities.
- [`JSONConvert`](xref:Blackwood.JSONConvert) -- A utility class for type conversion operations: parsing colors, booleans, etc.
- [`CasePreservingString`](xref:Blackwood.CasePreservingString) – Allows caseless matching, such as with a dictionary, while preserving the name case is preserved for printing, serialization, deserialization, etc.


## Next Steps

- [Getting Started Guide](getting-started.md) - Installation and basic setup
- [API Reference](../api/index.md) - Detailed API documentation
- [Examples](examples.md) - Real-world usage patterns
