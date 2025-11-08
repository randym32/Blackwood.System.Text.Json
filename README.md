# Blackwood.System.Text.Json Assembly

This assembly is collection of utilitites for the
[`System.Text.Json`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json)
to make JSON serialization and deserialization more flexible.

This does not have Windows specific material.


## Getting Started

### Prerequisites

- .NET 8.0 or later
- Visual Studio 2022 or later (Windows), or VS Code with C# extension (cross-platform)

- ### Installation

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

// Deserialize JSON with enhanced options
var json = """
{
    "name": "John Doe",
    "age": 30,
    "isActive": "True"
}
""";

var result = JSONDeserializer.Deserialize<Person>(json);
```

### Resources

- **Source Code**: [GitHub Repository](https://github.com/randym32/Blackwood.System.Text.Json)
- **NuGet Package**: [Blackwood.System.Text.Json](https://www.nuget.org/packages/Blackwood.System.Text.Json/)
- **Documentation**: [API Reference](~/api/)


## Documentation
The documentation can be found at [https://randym32.github.io/Blackwood.System.Text.Json](https://randym32.github.io/Blackwood.System.Text.Json)


## Contributing
View the [Blackwood.System.Text.Json GitHub Project](https://github.com/randym32/Blackwood.System.Text.Json)
for information on contributing.

