// Copyright © 2020-2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackwood;

/// <summary>
/// A custom JSON deserializer extending the standard System.Text.Json
/// functionality to better handle common JSON patterns and provide more
/// flexible type conversion.
/// </summary>
/// <remarks>
/// This JSON converter supports
/// - Case-insensitive property name matching
/// - Support for JSON comments and trailing commas
/// - Automatic type inference for numbers (int, long, double)
/// - Special handling for boolean strings ("True"/"False")
/// - Flexible object conversion from JsonElement to native .NET types
/// </remarks>
public partial class JSONDeserializer : JsonConverter<object>
{
    /// <summary>
    /// Pre-configured JSON serializer options optimized for flexible JSON parsing.
    /// </summary>
    /// <remarks>
    /// These options support
    /// - Skips JSON comments during parsing
    /// - Allows trailing commas in JSON objects and arrays
    /// - Case-insensitive property name matching
    /// - Pretty-prints output with indentation
    /// - Ignores default values when writing JSON
    /// - Uses the custom JSONDeserializer for improved type conversion
    /// </remarks>
    public static readonly JsonSerializerOptions JSONOptions = new()
    {
        ReadCommentHandling = JsonCommentHandling.Skip,       // Skip // and /* */ comments
        AllowTrailingCommas = true,                           // Allow trailing commas in objects/arrays
        //IgnoreNullValues    = true,                         // Legacy option (deprecated)
        PropertyNameCaseInsensitive = true,                   // Match properties regardless of case
        WriteIndented       = true,                           // Pretty-print JSON output
        DefaultIgnoreCondition= JsonIgnoreCondition.WhenWritingDefault, // Skip default values
        // Use a helper converter to better map to the native .NET types
        Converters =
            {
                new JSONDeserializer()                        // Custom converter
            }
    };

    /// <summary>
    /// Deserializes a JSON string into a strongly-typed object.
    /// This method provides JSON parsing with support for comments,
    /// trailing commas, and case-insensitive property matching.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize to</typeparam>
    /// <param name="jsonString">The JSON string to deserialize</param>
    /// <returns>The deserialized object of type T, or default(T) if the input is null/empty</returns>
    /// <exception cref="JsonException">Thrown when the JSON string is malformed</exception>
    public static T? Deserialize<T>(string jsonString)
    {
        // Sanity check the string - return default value for null/empty input
        if (string.IsNullOrWhiteSpace(jsonString))
            return default;

        // Deserialize the structure using our options
        return JsonSerializer.Deserialize<T>(jsonString, JSONOptions);
    }

    /// <summary>
    /// Asynchronously deserializes JSON from a stream into a strongly-typed object.
    /// This method is optimized for reading large JSON documents from streams
    /// without loading the entire content into memory at once.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize to</typeparam>
    /// <param name="stream">The stream containing JSON data to deserialize</param>
    /// <returns>A ValueTask containing the deserialized object of type T</returns>
    /// <exception cref="JsonException">Thrown when the JSON stream is malformed</exception>
    /// <exception cref="ArgumentNullException">Thrown when the stream is null</exception>
    public static ValueTask<T?> Deserialize<T>(Stream stream)
    {
        // Deserialize the structure asynchronously using our options
        return JsonSerializer.DeserializeAsync<T>(stream, JSONOptions);
    }

    /// <summary>
    /// Custom JSON converter that reads JSON tokens and converts them to appropriate .NET types.
    /// This method extends type inference and conversion capabilities beyond
    /// the standard System.Text.Json behavior.
    /// </summary>
    /// <param name="reader">The JSON reader positioned at the token to convert</param>
    /// <param name="typeToConvert">The target type for conversion (typically object)</param>
    /// <param name="options">The JSON serializer options</param>
    /// <returns>The converted .NET object with the most appropriate type</returns>
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Better number parsing: try int32 first, then int64, then double
        // This provides better type inference for numeric values
        if (reader.TokenType == JsonTokenType.Number)
        {
            var oki = reader.TryGetInt32(out var vali);
            if (oki)
                return vali;  // Return as int32 if possible

            var okl = reader.TryGetInt64(out var vall);
            if (okl)
                return vall;  // Return as int64 if int32 doesn't fit

            var okd = reader.TryGetDouble(out var val);
            if (okd)
                return val;   // Return as double for decimal numbers
        }

        // Read the property value as a JsonElement to handle different types
        var value = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        return JSONDeserializer.JsonToNormal(value);
    }
}
