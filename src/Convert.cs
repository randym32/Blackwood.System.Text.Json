// Copyright (c) 2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.
using System.Drawing;
using System.Net;

namespace Blackwood;

/// <summary>
/// These are helpers to convert to .Net types
/// </summary>
public static class JSONConvert
{
    /// <summary>
    /// Converts an object to an integer.
    /// </summary>
    /// <param name="o">The object to convert</param>
    /// <returns>The integer value of the object</returns>
    public static int ToInt(object o)
    {
        if (o is int i) return i;
        if (o is long l) return (int)l;
        if (o is float f) return (int)f;
        if (o is double d) return (int)d;
        return 0;
    }


    /// <summary>
    /// Converts an object to a float
    /// </summary>
    /// <param name="o">The object to convert</param>
    /// <returns>The float value of the object</returns>
    public static float ToFloat(object o)
    {
        if (o is int i) return i;
        if (o is long l) return (float)l;
        if (o is float f) return f;
        if (o is double d) return (float)d;
        return 0;
    }

    /// <summary>
    /// Converts an object to a boolean.
    /// </summary>
    /// <param name="o">The object to convert</param>
    /// <returns>The boolean value of the object</returns>
    public static bool ToBool(object o)
    {
        if (o is bool b) return b;
        if (o is string s)
        {
            var sl = s.ToLower();
            if ("true" == sl) return true;
            if ("false" == sl) return false;
        }
        if (o is int i) return i != 0;
        if (o is long l) return l != 0;
        if (o is float f) return f != 0;
        if (o is double d) return d != 0;
        try { return Convert.ToBoolean(o); }
        catch { return false; }
    }


    /// <summary>
    /// Converts a string to a Color.
    /// Supports named colors, hex codes (#RRGGBB or #AARRGGBB), or returns null for invalid strings.
    /// </summary>
    /// <param name="s">The string representation of the color</param>
    /// <returns>The Color object corresponding to the string, or null if invalid</returns>
    public static Color? TryParseColor(string s)
    {
        // If the string is null or empty, return null
        if (string.IsNullOrWhiteSpace(s))
            return null;

        // Remove whitespace
        s = s.Trim();

        // If string is a hex color code
        if (s.StartsWith("#"))
        {
            try
            {
                return ColorTranslator.FromHtml(s);
            }
            catch
            {
                // If ColorTranslator.FromHtml throws an exception, return null
                return null;
            }
        }

        // If the string is a named color, return the color
        // Try parse as a named color
        var named = Color.FromName(s);
        // Valid named colors are not Empty and have a known name that matches the input
        // Color.FromName returns a color with the input as name even for invalid colors
        // We need to check if it's a known color by comparing with KnownColor enum
        if (named != Color.Empty && named.IsKnownColor)
            return named;

        // If the string is not a valid color, return null
        return null;
    }


    /// <summary>
    /// Converts a value to the specified property type.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="targetType">The target property type.</param>
    /// <returns>The converted value.</returns>
    public static object? ConvertToType(object? value, Type targetType)
    {
        if (value == null)
            return null;

        // If the value is already of the correct type, return it
        if (targetType.IsAssignableFrom(value.GetType()))
            return value;

        // Handle nullable types
        var underlyingType = Nullable.GetUnderlyingType(targetType);
        if (underlyingType != null)
            return ConvertToType(value, underlyingType);

        // Handle basic type conversions
        if (targetType == typeof(string))
            return value.ToString();

        if (targetType.IsEnum && value is string strValue)
            return Enum.Parse(targetType, strValue);


        if (targetType == typeof(int) || targetType == typeof(int?))
            return Convert.ToInt32(value);

        if (targetType == typeof(double) || targetType == typeof(double?))
            return Convert.ToDouble(value);

        if (targetType == typeof(bool) || targetType == typeof(bool?))
            return JSONConvert.ToBool(value);

        if (targetType == typeof(float) || targetType == typeof(float?))
            return Convert.ToSingle(value);

        if (targetType == typeof(decimal) || targetType == typeof(decimal?))
            return Convert.ToDecimal(value);

        if (targetType == typeof(long) || targetType == typeof(long?))
            return Convert.ToInt64(value);

        if (targetType == typeof(short) || targetType == typeof(short?))
            return Convert.ToInt16(value);

        if (targetType == typeof(byte) || targetType == typeof(byte?))
            return Convert.ToByte(value);

        if (targetType == typeof(char) || targetType == typeof(char?))
            return Convert.ToChar(value);

        if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
            return Convert.ToDateTime(value);

        if (targetType == typeof(Guid) || targetType == typeof(Guid?))
            return Guid.Parse(value.ToString()!);

        if (targetType == typeof(Uri))
            return new Uri(value.ToString()!);

        if (targetType == typeof(TimeSpan) || targetType == typeof(TimeSpan?))
            return TimeSpan.Parse(value.ToString()!);

        if (targetType == typeof(Version))
            return Version.Parse(value.ToString()!);

        if (targetType == typeof(IPAddress))
            return IPAddress.Parse(value.ToString()!);

        // Handle Point type (with integer x and y properties)
        if (  (targetType == typeof(Point) || targetType == typeof(Point?))
           && value is Dictionary<CasePreservingString, object> pdict)
        {
            if (   pdict.TryGetValue("x", out var x)
                && pdict.TryGetValue("y", out var y)
                )
            return new Point(ToInt(x), ToInt(y));

            // Handle default type
            if (pdict.Count == 0)
                return new Point(0, 0);
        }

        // Handle PointF type (with float x and y properties)
        if ( (targetType == typeof(PointF) || targetType == typeof(PointF?))
           && value is Dictionary<CasePreservingString, object> pfdict)
        {
            if (  pfdict.TryGetValue("x", out var x)
               && pfdict.TryGetValue("y", out var y)
               )
            return new PointF(ToFloat(x), ToFloat(y));

            // Handle default type
            if (pfdict.Count == 0)
                return new PointF(0.0f, 0.0f);
        }

        // Handle Size type (with integer width and height properties)
        if (  (targetType == typeof(Size) || targetType == typeof(Size?))
           && value is Dictionary<CasePreservingString, object> sdict)
        {
            if (   sdict.TryGetValue("width", out var width)
               && sdict.TryGetValue("height", out var height)
               )
            return new Size(ToInt(width), ToInt(height));

            // Handle default type
            if (sdict.Count == 0)
                return new Size(0, 0);
        }

        // Handle SizeF type (with float width and height properties)
        if (  (targetType == typeof(SizeF) || targetType == typeof(SizeF?))
           && value is Dictionary<CasePreservingString, object> sfdict)
        {
            if (  sfdict.TryGetValue("width", out var width)
               && sfdict.TryGetValue("height", out var height)
               )
                return new SizeF(ToFloat(width), ToFloat(height));

            // Handle default type
            if (sfdict.Count == 0)
                return new SizeF(0.0f, 0.0f);
        }

        // Handle Rectangle type (with integer x, y, width, and height properties)
        if (  (targetType == typeof(Rectangle) || targetType == typeof(Rectangle?))
           && value is Dictionary<CasePreservingString, object> rdict)
        {
            if (  rdict.TryGetValue("x", out var x)
               && rdict.TryGetValue("y", out var y)
               && rdict.TryGetValue("width", out var width)
               && rdict.TryGetValue("height", out var height)
               )
                return new Rectangle(ToInt(x), ToInt(y), ToInt(width), ToInt(height));

            // Handle default type
            if (rdict.Count == 0)
                return new Rectangle(0, 0, 0, 0);
        }

        // Handle RectangleF type (with float x, y, width, and height properties)
        if (  (targetType == typeof(RectangleF) || targetType == typeof(RectangleF?))
           && value is Dictionary<CasePreservingString, object> rfdict)
        {
            if (  rfdict.TryGetValue("x", out var x)
               && rfdict.TryGetValue("y", out var y)
               && rfdict.TryGetValue("width", out var width)
               && rfdict.TryGetValue("height", out var height)
               )
                return new RectangleF(ToFloat(x), ToFloat(y), ToFloat(width), ToFloat(height));

            // Handle default type
            if (rfdict.Count == 0)
                return new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
        }

        if (targetType == typeof(Color) || targetType == typeof(Color?))
            return TryParseColor(value.ToString()!);

        // For other types, try to use the type converter or return the value as-is
        return value;
    }
}
