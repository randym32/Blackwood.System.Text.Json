// Copyright (c) 2026 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackwood;

/// <summary>
/// System.Text.Json converter that accepts enum values as JSON strings in
/// PascalCase, camelCase, or snake_case.
/// </summary>
/// <typeparam name="TEnum">A struct enum type backed by integer values.</typeparam>
/// <remarks>
/// Hand-authored JSON often mixes naming styles. This converter tries, in order:
/// <list type="number">
/// <item><description>Case-insensitive <see cref="Enum.TryParse{TEnum}(ReadOnlySpan{char}, bool, out TEnum)"/> against CLR member names (PascalCase or camelCase).</description></item>
/// <item><description>After <see cref="SnakeCaseToPascalCase"/> (e.g.
/// <c>inverse_square</c> → <c>InverseSquare</c>).</description></item>
/// <item><description>After <see cref="CamelCaseToSnakeCase"/> (e.g.
/// <c>inverseSquare</c> → <c>inverse_square</c>) for enums whose members use snake_case names.</description></item>
/// </list>
/// Serialization always writes the CLR member name via <see cref="Enum.ToString()"/> (typically PascalCase).
/// </remarks>
public sealed class FlexibleStringEnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    /// <summary>
    /// Deserializes a JSON string token into <typeparamref name="TEnum"/>.
    /// </summary>
    /// <param name="reader">JSON reader positioned at a string token.</param>
    /// <param name="typeToConvert">Target enum type (same as <typeparamref name="TEnum"/>).</param>
    /// <param name="options">Serializer options (unused; naming is handled explicitly).</param>
    /// <returns>The parsed enum value.</returns>
    /// <exception cref="JsonException">
    /// Thrown when the token is not a string, is null/empty after read, or does not match any enum member
    /// after direct parse, snake_case-to-PascalCase, or camelCase-to-snake_case normalization.
    /// </exception>
    /// <remarks>
    /// For an enum member <c>InverseSquare</c>, accepted JSON strings include
    /// <c>InverseSquare</c>, <c>inverseSquare</c>, and <c>inverse_square</c>.
    /// </remarks>
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // First, check that we even have a string
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Expected string for enum {typeof(TEnum).Name}.");

        // Get the string
        var text = reader.GetString() ?? throw new JsonException($"Expected string for enum {typeof(TEnum).Name}.");

        // Case-insensitive match against CLR member names (PascalCase or camelCase).
        if (Enum.TryParse<TEnum>(text, ignoreCase: true, out var value))
            return value;

        // snake_case JSON → PascalCase member name, then parse.
        var pascal = SnakeCaseToPascalCase(text);
        if (Enum.TryParse<TEnum>(pascal, ignoreCase: true, out value))
            return value;

        // camelCase/PascalCase JSON → snake_case member name, then parse.
        var snake = CamelCaseToSnakeCase(text);
        if (Enum.TryParse<TEnum>(snake, ignoreCase: true, out value))
            return value;

        // Give up
        throw new JsonException($"Unknown {typeof(TEnum).Name} value '{text}'.");
    }


    /// <summary>Converts a snake_case JSON enum literal to PascalCase (typical CLR enum member name).</summary>
    /// <param name="name">
    /// Enum literal from JSON (e.g. <c>inverse_square</c>). Names without <c>_</c> are returned unchanged.
    /// </param>
    /// <returns>PascalCase form (e.g. <c>InverseSquare</c>).</returns>
    internal static string SnakeCaseToPascalCase(string name)
    {
        if (name.IndexOf('_') < 0)
            return name;

        var parts = name.Split('_');
        var buffer = new StringBuilder(name.Length);
        foreach (var part in parts)
        {
            if (part.Length == 0)
                continue;

            buffer.Append(char.ToUpperInvariant(part[0]));
            if (part.Length > 1)
                buffer.Append(part[1..].ToLowerInvariant());
        }

        return buffer.ToString();
    }


    /// <summary>Converts a camelCase or PascalCase JSON enum literal to snake_case.</summary>
    /// <param name="name">
    /// Enum literal from JSON. Already snake_case names and empty strings are returned unchanged.
    /// </param>
    /// <returns>
    /// snake_case form (e.g. <c>inverse_square</c>). Inserts <c>_</c> before each uppercase letter after the first.
    /// </returns>
    /// <example><c>inverseSquare</c> → <c>inverse_square</c>; <c>InverseSquare</c> → <c>inverse_square</c>.</example>
    internal static string CamelCaseToSnakeCase(string name)
    {
        // Already snake_case or empty — return unchanged.
        if (string.IsNullOrEmpty(name) || name.Contains('_'))
            return name;


        // Insert '_' before each uppercase letter (after the first) and lower-case it.
        var buffer = new StringBuilder(name.Length + 4);
        for (var i = 0; i < name.Length; i++)
        {
            var c = name[i];
            if (char.IsUpper(c))
            {
                if (i > 0)
                    buffer.Append('_');
                buffer.Append(char.ToLowerInvariant(c));
            }
            else
            {
                buffer.Append(c);
            }
        }


        return buffer.ToString();
    }


    /// <summary>
    /// Serializes an enum value as its CLR member name (PascalCase string).
    /// </summary>
    /// <param name="writer">JSON writer receiving the string token.</param>
    /// <param name="value">Enum value to write.</param>
    /// <param name="options">Serializer options (unused).</param>
    /// <remarks>
    /// A member named <c>None</c> is written as <c>"None"</c>, not snake_case.
    /// Round-trip with <see cref="Read"/> is supported for any input form accepted by read.
    /// </remarks>
    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}
