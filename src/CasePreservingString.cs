// Copyright © 2022-2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Blackwood;

/// <summary>
/// This represents a string that preserves its original case for display
/// purposes but allows for case-insensitive comparisons. It provides methods
/// to convert a string to this struct, compare two instances in a
/// case-insensitive manner, and generate a hash code based on the
/// uppercase version of the string.
/// </summary>
/// <remarks>
/// Creates a CasePreservingString instance.
/// </remarks>
/// <param name="text">The text string.</param>
public readonly struct CasePreservingString(string text)
{
    /// <summary>
    /// The string.
    /// </summary>
    public readonly string text = text;

    /// <summary>
    /// Provides the string with the original case.
    /// </summary>
    /// <returns>The string with the original case.</returns>
    public override string ToString()
    {
        return text;
    }

    /// <summary>
    /// The hash of the caseless string (all uppercase).
    /// </summary>
    /// <returns>The hash value</returns>
    public override int GetHashCode()
    {
        // Use a stable case-insensitive hash by using StringComparer.InvariantCultureIgnoreCase
        return text is null ? 0 : StringComparer.InvariantCultureIgnoreCase.GetHashCode(text);
    }


    /// <summary>
    /// Comparese the string in a caseless fashion.
    /// </summary>
    /// <param name="b">A string to compare against</param>
    /// <returns>True if the strings are equal, false otherwise</returns>
    public bool Equals(CasePreservingString b)
    {
        return string.Equals(text, b.text, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Compares this CasePreservingString with another object in a case-insensitive manner.
    /// </summary>
    /// <param name="obj">The object to compare against</param>
    /// <returns>True if the objects are equal, false otherwise</returns>
    public override bool Equals(object? obj)
    {
        return obj is CasePreservingString other && Equals(other);
    }

    /// <summary>
    /// Convert a string to one that uses case-less matching.
    /// </summary>
    /// <param name="string">The string to convert</param>
    public static implicit operator CasePreservingString(string @string) => new(@string);

    /// <summary>
    /// Convert a case-preserving string back to a regular string.
    /// </summary>
    /// <param name="string">The case-preserving string to convert</param>
    public static implicit operator string(CasePreservingString @string) => @string.text;
}
