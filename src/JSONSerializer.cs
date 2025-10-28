// Copyright (c) 2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;

namespace Blackwood;
public partial class JSONDeserializer
{
    /// <summary>
    /// Serializes a property value to a JSON-compatible format.
    /// </summary>
    /// <param name="value">The property value to serialize</param>
    /// <returns>A JSON-compatible representation of the value</returns>
    static public object? ConvertValueToSerializableForm(object? value)
    {
        if (value == null) return null;

        return value switch
        {
            Color      color => SerializeColor(color),
            Point      point => new { x = point.X, y = point.Y },
            PointF     point => new { x = point.X, y = point.Y },
            Size       size  => new { width = size.Width, height = size.Height },
            SizeF      size  => new { width = size.Width, height = size.Height },
            Rectangle  rect  => new { x = rect.X, y = rect.Y, width = rect.Width, height = rect.Height },
            RectangleF rect  => new { x = rect.X, y = rect.Y, width = rect.Width, height = rect.Height },
            Array array => SerializeArray(array),
            global::System.Net.IPAddress ipAddress => ipAddress.ToString(),
            _ => value // Fallback to standard representation
        };
    }

    /// <summary>
    /// Serializes an array to a JSON-compatible format.
    /// </summary>
    /// <param name="array">The array to serialize</param>
    /// <returns>A JSON-compatible array representation</returns>
    internal static object?[] SerializeArray(Array array)
    {
        var result = new object?[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = ConvertValueToSerializableForm(array.GetValue(i));
        }
        return result;
    }

    /// <summary>
    /// Serializes a Color to a JSON-compatible format.
    /// </summary>
    /// <param name="color">The color to serialize</param>
    /// <returns>A JSON-compatible color representation</returns>
    internal static object SerializeColor(Color color)
    {
        // If the color is a named color, return the name
        if (color.IsNamedColor) return color.Name;

        // Otherwise, return the RGBA values
        // Format the color as an HTML-style #AARRGGBB hex string (with alpha first, as in .NET's Color)
        return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    /// <summary>
    /// Writes a .NET object to JSON format.
    /// This method is primarily used for serialization scenarios, though the main
    /// focus of this class is on deserialization with improved parsing capabilities.
    /// </summary>
    /// <param name="writer">The JSON writer to write the serialized data to</param>
    /// <param name="value">The .NET object to serialize to JSON</param>
    /// <param name="options">The JSON serializer options to use</param>
    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        // If the value is null, write null; otherwise, serialize the value using System.Text.Json
        var serializedValue = ConvertValueToSerializableForm(value);
        if (null == serializedValue)
        {
            writer.WriteNullValue();
            return;
        }
        if (value is Color c)
            value = SerializeColor(c);
        JsonSerializer.Serialize(writer, serializedValue, serializedValue.GetType(), options);
    }

    /// <summary>
    /// JsonTypeInfoResolver that respects DefaultValueAttribute when 
    /// DefaultIgnoreCondition is WhenWritingDefault.  This allows 
    /// JsonSerializer.Serialize to properly skip properties that match their
    /// DefaultValue attribute.
    /// </summary>
    /// <remarks>
    /// Skip serialization if the value is the default value, as given in the
    /// [DefaultValue] attribute -- if it is present.  We don't use the types
    /// default value, as the DefaultValue is more authoratitative here.
    /// </remarks>
    public class DefaultValueAwareTypeInfoResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            var typeInfo = base.GetTypeInfo(type, options);

            // Check if we should skip default values based on JsonIgnoreCondition.WhenWritingDefault
            if (typeInfo.Kind != JsonTypeInfoKind.Object ||
                options.DefaultIgnoreCondition != JsonIgnoreCondition.WhenWritingDefault)
                return typeInfo;

            // Modify properties to respect DefaultValueAttribute
            foreach (var property in typeInfo.Properties)
            {
                // Try to get DefaultValueAttribute from the property's attribute provider
                if (property.AttributeProvider == null)
                    continue;

                DefaultValueAttribute? defaultValueAttr = null;

                var attrs = property.AttributeProvider.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                if (attrs.Length > 0)
                {
                    defaultValueAttr = attrs[0] as DefaultValueAttribute;
                }

                if (defaultValueAttr != null)
                {
                    var originalShouldSerialize = property.ShouldSerialize;
                    property.ShouldSerialize = (obj, value) =>
                    {
                        // First check the original condition (e.g., null checks)
                        if (originalShouldSerialize != null && !originalShouldSerialize(obj, value))
                            return false;

                        // Then check if value matches DefaultValue attribute
                        return !Equals(value, defaultValueAttr.Value);
                    };
                }
            }

            return typeInfo;
        }
    }

}
