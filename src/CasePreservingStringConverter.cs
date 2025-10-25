// Copyright (c) 2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackwood;

/// <summary>
/// Custom JSON converter for CasePreservingString serialization and deserialization.
/// Supports both regular serialization and dictionary key serialization.
/// </summary>
public class CasePreservingStringConverter : JsonConverter<CasePreservingString>
{
    /// <summary>
    /// Reads a CasePreservingString from JSON.
    /// </summary>
    /// <param name="reader">The JSON reader</param>
    /// <param name="typeToConvert">The type to convert to</param>
    /// <param name="options">The serializer options</param>
    /// <returns>The parsed CasePreservingString</returns>
    public override CasePreservingString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return new CasePreservingString(null!);

        return new CasePreservingString(reader.GetString()!);
    }

    /// <summary>
    /// Writes a CasePreservingString to JSON.
    /// </summary>
    /// <param name="writer">The JSON writer</param>
    /// <param name="value">The CasePreservingString to write</param>
    /// <param name="options">The serializer options</param>
    public override void Write(Utf8JsonWriter writer, CasePreservingString value, JsonSerializerOptions options)
    {
        if (value.text == null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value.text);
    }

    /// <summary>
    /// Reads a CasePreservingString from JSON as a property name (for dictionary keys).
    /// </summary>
    /// <param name="reader">The JSON reader</param>
    /// <param name="typeToConvert">The type to convert to</param>
    /// <param name="options">The serializer options</param>
    /// <returns>The parsed CasePreservingString</returns>
    public override CasePreservingString ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new CasePreservingString(reader.GetString()!);
    }

    /// <summary>
    /// Writes a CasePreservingString to JSON as a property name (for dictionary keys).
    /// </summary>
    /// <param name="writer">The JSON writer</param>
    /// <param name="value">The CasePreservingString to write</param>
    /// <param name="options">The serializer options</param>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, CasePreservingString value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.text ?? string.Empty);
    }
}
