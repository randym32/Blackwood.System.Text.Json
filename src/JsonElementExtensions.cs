// Copyright (c) 2026 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Text.Json;

namespace Blackwood;


/// <summary>
/// Case-insensitive and dual-name property lookup for scene JSON.
/// </summary>
public static class JsonElementExtensions
{
    /// <summary>Case-insensitive property lookup (exact match tried first).</summary>
    /// <param name="element">Object element to search.</param>
    /// <param name="name">Property name to match.</param>
    /// <param name="value">Matched property value when this method returns <see langword="true"/>.</param>
    /// <returns><see langword="true"/> when <paramref name="name"/> is found on <paramref name="element"/>.</returns>
    public static bool TryGetPropertyIgnoreCase(this JsonElement element, string name, out JsonElement value)
    {
        // The property matching the exact name (see above).
        if (element.ValueKind == JsonValueKind.Object
            && element.TryGetProperty(name, out value))
            return true;

        value = default;
        if (element.ValueKind != JsonValueKind.Object)
            return false;

        // Fallback: enumerate all properties to find a case-insensitive match
        foreach (var property in element.EnumerateObject())
        {
            if (property.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                value = property.Value;
                return true;
            }
        }


        return false;
    }
}
