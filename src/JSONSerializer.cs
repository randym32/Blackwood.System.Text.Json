// Copyright © 2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.
using System.Text.Json;
using System.Reflection;
using System.Drawing;

namespace Blackwood;
public partial class JSONDeserializer
{
    /// <summary>
    /// Serializes custom properties marked with give attribute.
    /// </summary>
    /// <returns>A dictionary of serialized property values</returns>
    public static Dictionary<CasePreservingString, object> SerializeProperties(object from, Type AttributeType)
    {
        var properties = new Dictionary<CasePreservingString, object>();
        var type = from.GetType();

        // Get all properties marked with AttributeType
        var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(prop => Attribute.IsDefined(prop, AttributeType));

        foreach (var prop in propertyInfos)
        {
            try
            {
                // Serialize the field and its value, skipping if null or default
                SerializeProperty(properties, prop, prop.GetValue(from));
            }
            catch (Exception ex)
            {
                // Log error but continue with other properties
                properties[prop.Name] = $"Error serializing: {ex.Message}";
            }
        }

        // Get all fields marked with AttributeType
        var fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(prop => Attribute.IsDefined(prop, AttributeType));

        foreach (var field in fieldInfos)
        {
            try
            {
                // Serialize the field and its value, skipping if null or default
                SerializeProperty(properties, field, field.GetValue(from));
            }
            catch (Exception ex)
            {
                // Log error but continue with other properties
                properties[field.Name] = $"Error serializing: {ex.Message}";
            }
        }

        return properties;
    }

    /// <summary>
    /// Serializes a property value to a JSON-compatible format.
    /// </summary>
    /// <param name="properties">The dictionary to store the serialized properties</param>
    /// <param name="info">The property or field to serialize</param>
    /// <param name="value">The property value to serialize</param>
    static void SerializeProperty(Dictionary<CasePreservingString, object> properties, MemberInfo info, object? value)
    {
        // Skip nulls
        if (null == value) return;

        // Skip serialization if the value is the default value, as
        // given in the [DefaultValue] attribute -- if it is present.
        // We don't use the types default value, as the object may give
        // an initial value and skipping the type default will skip the
        // change from the initial value.
        var defaultAttr = info.GetCustomAttribute(typeof(System.ComponentModel.DefaultValueAttribute)) as System.ComponentModel.DefaultValueAttribute;
        if (defaultAttr != null && Equals(value, defaultAttr.Value))
            return;

        // Convert the value to a serializable form
        var serializedValue = ConvertValueToSerializableForm(value);
        if (null != serializedValue)
            properties[info.Name] = serializedValue;
    }



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

}
