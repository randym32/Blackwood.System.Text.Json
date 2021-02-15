// Copyright © 2020 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.  
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blackwood
{

/// <summary>
/// This is used to work with the JSON deserializer to better represent them as
/// .NET objects.
/// </summary>
public class JSONDeserializer : JsonConverter<object>
{
    /// <summary>
    /// This is used to deserialize more common JSON encodings
    /// </summary>
    /// <param name="jsonString">The str to decode the JSON from.</param>
    /// <returns>A task to decode the value</returns>
    public static T Deserialize<T>(string jsonString)
    {
        // Sanity check the string
        if (string.IsNullOrWhiteSpace(jsonString))
            return default(T);

        // Convert it to a form we can restore from
        var JSONOptions = new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
            IgnoreNullValues    = true,
            // Use a helper converter to better map to the native .NET types
            Converters =
                {
                    new JSONDeserializer()
                }
        };

        // Deserialize the structure
        return JsonSerializer.Deserialize<T>(jsonString, JSONOptions);
    }

    /// <summary>
    /// This is used to deserialize more common JSON encodings
    /// </summary>
    /// <param name="stream">The stream to read the JSON from.</param>
    /// <returns>A task to decode the value</returns>
    public static ValueTask<T?> Deserialize<T>(Stream stream)
    {
        // Convert it to a form we can restore from
        var JSONOptions = new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
            IgnoreNullValues    = true,
            // Use a helper converter to better map to the native .NET types
            Converters =
                {
                    new JSONDeserializer()
                }
        };

        // Deserialize the structure
        return JsonSerializer.DeserializeAsync<T>(stream, JSONOptions);
    }

    /// <summary>
    /// Reads C# objects from the stream for the JSON deserializer
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var type = reader.TokenType;

        if (type == JsonTokenType.Number)
        {
            var oki = reader.TryGetInt32(out var vali);
            if (oki)
            {
                return vali;
            }
            var okl = reader.TryGetInt64(out var vall);
            if (okl)
            {
                return vall;
            }
            var okd = reader.TryGetDouble(out var val);
            if (okd)
            {
                return val;
            }
        }

        if (type == JsonTokenType.String)
        {
            return reader.GetString();
        }

        if (type == JsonTokenType.True || type == JsonTokenType.False)
        {
            return reader.GetBoolean();
        }
        // copied from corefx repo:
        using var document = JsonDocument.ParseValue(ref reader);
        return document.RootElement.Clone();
    }

    /// <summary>
    /// Disabled since we aren't write
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
}