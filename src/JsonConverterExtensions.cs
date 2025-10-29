// Copyright (c) 2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackwood;

/// <summary>
/// Extensions for JsonConverter.
/// </summary>
public static class JsonConverterExtensions
{
    /// <summary>
    /// Skips the value, array or object in the reader.
    /// </summary>
    /// <param name="ignored"></param>
    /// <param name="reader">The JsonReader to skip</param>
    public static void Skip(this JsonConverter ignored, Utf8JsonReader reader)
    {
        if (  reader.TokenType != JsonTokenType.StartObject
           && reader.TokenType != JsonTokenType.StartArray
           )
        {
            // Consume single value (primitives, null)
            // Just advance the reader
            return;
        }

        // Skip the value, array or object
        // Allow nested arrays and dictionaries by tracking the count
        int depth = reader.CurrentDepth;
        while (reader.Read())
        {
            if ((  reader.TokenType == JsonTokenType.EndObject
                || reader.TokenType == JsonTokenType.EndArray)
                && reader.CurrentDepth == depth - 1)
                break;
        }
    }
}
