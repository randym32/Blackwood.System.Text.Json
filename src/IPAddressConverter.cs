// Copyright (c) 2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackwood;

/// <summary>
/// Custom JSON converter for IPAddress serialization and deserialization.
/// </summary>
public class IPAddressConverter : JsonConverter<IPAddress>
{
    /// <summary>
    /// Reads an IPAddress from JSON.
    /// </summary>
    /// <param name="reader">The JSON reader</param>
    /// <param name="typeToConvert">The type to convert to</param>
    /// <param name="options">The serializer options</param>
    /// <returns>The parsed IPAddress</returns>
    public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null!;

        var stringValue = reader.GetString();
        return IPAddress.Parse(stringValue!);
    }

    /// <summary>
    /// Writes an IPAddress to JSON.
    /// </summary>
    /// <param name="writer">The JSON writer</param>
    /// <param name="value">The IPAddress to write</param>
    /// <param name="options">The serializer options</param>
    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }
        writer.WriteStringValue(value.ToString());
    }
}
