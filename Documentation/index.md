# Blackwood.System.Text.Json

This assembly is collection of utilitites for the
[`System.Text.Json`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json)
to make JSON serialization and deserialization more flexible.

## Overview

**Utility Classes**:
- [`JSONDeserializer`](xref:Blackwood.JSONDeserializer) – Enhanced deserialization with flexible type handling.
- [`JSONConvert`](xref:Blackwood.JSONConvert) – Customer conversion utilities.

View the [API Documentation](./api/index.md) for detailed information about available namespaces, classes, and methods.


## Getting Started

### Prerequisites

- .NET 8.0 or later
- Visual Studio 2022 or later (Windows), or VS Code with C# extension (cross-platform)

### Installation

Install the package via NuGet:

```bash
dotnet add package Blackwood.System.Text.Json
```

Or using Package Manager Console:

```powershell
Install-Package Blackwood.System.Text.Json
```

### Quick Start

```csharp
using Blackwood;

// Example JSON structure
var json = """
{
    "name": "John Doe",
    "age": 30,
    "isActive": "True"
}
""";

// Deserialize the JSON to a Person object.
var result = JSONDeserializer.Deserialize<Person>(json);
```


### Resources

- **Source Code**: [GitHub Repository](https://github.com/randym32/Blackwood.System.Text.Json)
- **NuGet Package**: [Blackwood.System.Text.Json](https://www.nuget.org/packages/Blackwood.System.Text.Json/)
- **Documentation**: [API Reference](./api/index.md)



## Documentation

The API documentation is available at [https://randym32.github.io/Blackwood.System.Text.Json](https://randym32.github.io/Blackwood.System.Text.Json).


For instructions on how to build or modify the documentation, see
_[How to Build the Documentation](building-docs.md)_.

### Articles

For guides and examples, check out our articles:

- **[Introduction](./articles/intro.md)** - Overview of the library's features and capabilities
- **[Getting Started](./articles/getting-started.md)** - Installation, configuration, and basic usage
- **[Examples](./articles/examples.md)** - Common scenarios, flexible deserialization with type coercion, error handling.


## Contributing

We welcome contributions! Please see our [Contributing Guidelines](contributing/CONTRIBUTING.md) for information on how to contribute to this project.

- **Issues**: Report bugs or request features on [GitHub Issues](https://github.com/randym32/Blackwood.System.Text.Json/issues)
- **Pull Requests**: Submit improvements via [GitHub Pull Requests](https://github.com/randym32/Blackwood.System.Text.Json/pulls)
- **Code of Conduct**: Please read our [Code of Conduct](contributing/code_of_conduct.md)

## License

This project is licensed under the BSD 2-Clause License – see the [LICENSE](../LICENSE) file for details.

