// Copyright © 2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackwood;

/// <summary>
/// Custom JSON converter for Point serialization and deserialization.
/// </summary>
public class Converter2D<T> : JsonConverter<T>
{
    /// <summary>
    /// Reads a Point from JSON.
    /// </summary>
    /// <param name="reader">The JSON reader</param>
    /// <param name="typeToConvert">The type to convert to</param>
    /// <param name="options">The serializer options</param>
    /// <returns>The parsed Point</returns>
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Capture the initial token, for any error reporting
        var initialToken = reader.TokenType;

        // Read the property value as a JsonElement to handle different types
        var value = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        var v = JSONDeserializer.JsonToNormal(value);
        if (v != null)
        {
            var p = JSONConvert.ConvertToType(v, typeof(T));
            if (p is T ret)
                return ret;
        }
        if (reader.TokenType == JsonTokenType.Null)
            return default(T);

        throw new JsonException($"Unexpected token type: {initialToken}");
    }

    /// <summary>
    /// Writes a Point to JSON.
    /// </summary>
    /// <param name="writer">The JSON writer</param>
    /// <param name="value">The Point to write</param>
    /// <param name="options">The serializer options</param>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, JSONDeserializer.ConvertValueToSerializableForm(value), options);
    }
}
