// Copyright (c) 2020-2025 Randall Maas. All rights reserved.
// See LICENSE file in the project root for full license information.
using System.Reflection;
using System.Text.Json;

namespace Blackwood;

public partial class JSONDeserializer
{
    /// <summary>
    /// Converts a JsonElement to its most appropriate .NET object representation.
    /// This method provides intelligent type conversion based on the JSON value kind,
    /// including special handling for boolean strings and automatic type inference.
    /// </summary>
    /// <remarks>
    /// Supported conversions:
    /// - JsonValueKind.String: Converts to string, with special handling for "true"/"false" strings
    /// - JsonValueKind.Number: Converts to double
    /// - JsonValueKind.True/False: Converts to boolean
    /// - JsonValueKind.Array: Converts to object[] using ToArray method
    /// - JsonValueKind.Object: Converts to Dictionary&lt;string, object&gt; using ToDict method
    /// - JsonValueKind.Null: Returns null
    /// - Unsupported types: Returns null
    /// </remarks>
    /// <param name="item">The JsonElement to convert</param>
    /// <returns>The converted .NET object with the most appropriate type, or null for unsupported types</returns>
    /// <example>
    /// <code>
    /// var json = "\"true\"";
    /// var element = JsonDocument.Parse(json).RootElement;
    /// var result = JSONDeserializer.JsonToNormal(element); // Returns bool true
    /// </code>
    /// </example>
    static public object? JsonToNormal(JsonElement item)
    {
        // Switch on the value kind and return the appropriate .NET type
        switch (item.ValueKind)
        {
            case JsonValueKind.Null  : return null;
            case JsonValueKind.True  : return true;
            case JsonValueKind.False : return false;
            case JsonValueKind.Array : return ToArray(item);
            case JsonValueKind.Object: return ToDict(item);
            case JsonValueKind.String:
                // Special handling for boolean strings: "True" -> true, "False" -> false
                // Provides compatibility with systems that serialize booleans as strings
                var s = item.GetString();
                var sl = s?.ToLower();
                if ("true" == sl) return true;   // Convert "true" string to boolean true
                if ("false" == sl) return false; // Convert "false" string to boolean false
                return s;  // Return as string for all other string values
            case JsonValueKind.Number:
                // Better number parsing: try int32 first, then int64, then double
                // This provides better type inference for numeric values
                return item.TryGetInt32(out var intValue) ? intValue
                     : item.TryGetInt64(out var longValue) ? longValue
                     : item.GetDouble();
        }
        // If the value is not a recognized type (e.g., JsonValueKind.Null), return null
        return null;
    }


    /// <summary>
    /// Converts a dictionary containing JsonElement values to a dictionary with native .NET objects.
    /// </summary>
    /// <remarks>
    /// This method recursively processes all values in the dictionary, converting JsonElements
    /// to their appropriate .NET types.
    /// </remarks>
    /// <param name="jsonDictionary">The dictionary containing JsonElement values to convert</param>
    /// <returns>A new dictionary with all values converted to native .NET objects</returns>
    /// <example>
    /// <code>
    /// var dict = new Dictionary&lt;string, object&gt;();
    /// dict["name"] = JsonDocument.Parse("\"John\"").RootElement;
    /// dict["age"] = JsonDocument.Parse("30").RootElement;
    /// var result = JSONDeserializer.ToDict(dict);
    /// // result["name"] is string "John", result["age"] is double 30.0
    /// </code>
    /// </example>
    static public Dictionary<CasePreservingString, object> ToDict(Dictionary<string, object> jsonDictionary)
    {
        var ret = new Dictionary<CasePreservingString, object>();
        foreach (var item in jsonDictionary)
        {
            // Only process non-null values to avoid null reference exceptions
            if (null != item.Value)
            {
                if (item.Value is JsonElement e)
                {
                    // Convert JsonElement to native .NET object using recursive conversion
                    var obj = JsonToNormal(e);
                    if (null != obj)
                        ret[item.Key] = obj;  // Add converted value to result dictionary
                }
                else
                    ret[item.Key] = item.Value;
            }
        }
        return ret;
    }

    /// <summary>
    /// Converts a JsonElement representing a JSON object to a Dictionary&lt;string, object&gt;.
    /// </summary>
    /// <remarks>
    /// This method processes all properties in the JSON object and converts their values
    /// to appropriate .NET types.
    /// </remarks>
    /// <param name="jsonDictionary">The JsonElement representing a JSON object to convert</param>
    /// <returns>A dictionary containing all properties with converted .NET object values</returns>
    /// <example>
    /// <code>
    /// var json = "{\"name\": \"John\", \"age\": 30, \"active\": true}";
    /// var element = JsonDocument.Parse(json).RootElement;
    /// var result = JSONDeserializer.ToDict(element);
    /// // result["name"] is string "John", result["age"] is double 30.0, result["active"] is bool true
    /// </code>
    /// </example>
    static public Dictionary<CasePreservingString, object> ToDict(JsonElement jsonDictionary)
    {
        // Create a new dictionary to hold the converted properties
        var ret = new Dictionary<CasePreservingString, object>();
        foreach (var item in jsonDictionary.EnumerateObject())
        {
            // Convert each property value to a native .NET object using recursive conversion
            var obj = JsonToNormal(item.Value);
            if (null != obj)
                ret[item.Name] = obj;  // Add property with converted value to result
        }
        // Return the fully converted dictionary
        return ret;
    }

    /// <summary>
    /// Converts a JsonElement representing a JSON array to an object array.
    /// </summary>
    /// <remarks>
    /// This method processes all elements in the JSON array and converts each
    /// element to its appropriate .NET type.
    /// </remarks>
    /// <param name="v">The JsonElement representing a JSON array to convert</param>
    /// <returns>An object array containing all converted elements</returns>
    /// <example>
    /// <code>
    /// var json = "[1, \"hello\", true, [2, 3]]";
    /// var element = JsonDocument.Parse(json).RootElement;
    /// var result = JSONDeserializer.ToArray(element);
    /// // result[0] is double 1.0, result[1] is string "hello", result[2] is bool true, result[3] is object[] [2.0, 3.0]
    /// </code>
    /// </example>
    static public object[] ToArray(JsonElement v)
    {
        // Create a list to collect converted elements
        var ret = new List<object>();
        foreach (var item in v.EnumerateArray())
        {
            // Convert each array element to a native .NET object using recursive conversion
            var obj = JsonToNormal(item);
            if (null != obj)
                ret.Add(obj);  // Add converted element to the list
        }
        // Convert list to array and return
        return ret.ToArray();
    }

    /// <summary>
    /// Deserializes properties from a JSON element to an object.
    /// </summary>
    /// <param name="obj">The object to deserialize properties to</param>
    /// <param name="properties">The properties to deserialize</param>
    /// <param name="attributeType">The attribute type to use for deserialization</param>
    static public void DeserializeProperties(object obj, Dictionary<CasePreservingString, object> properties, Type attributeType)
    {
        var type = obj.GetType();

        // Get all properties marked with AttributeType
        var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(prop => Attribute.IsDefined(prop, attributeType));

        foreach (var prop in propertyInfos)
        {
            // Get the property value from the JSON element
            if (!properties.TryGetValue(prop.Name, out var value)) continue;
            try
            {
                // Try to convert the value to the property type
                value = JSONConvert.ConvertToType(value, prop.PropertyType);
                // Set the property value
                prop.SetValue(obj, value);
            }
            catch (Exception)
            {
                // Log error but continue with other properties
                //  $"Error deserializing: {ex.Message}";
            }
        }

        // Get all fields marked with AttributeType
        var fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(prop => Attribute.IsDefined(prop, attributeType));

        foreach (var field in fieldInfos)
        {
            // Get the field value from the JSON element
            if (!properties.TryGetValue(field.Name, out var value)) continue;
            try
            {
                // Try to convert the value to the field type
                value = JSONConvert.ConvertToType(value, field.FieldType);
                // Set the field value
                field.SetValue(obj, value);
            }
            catch (Exception)
            {
                // Log error but continue with other fields
                //  $"Error deserializing: {ex.Message}";
            }
        }
    }
}
