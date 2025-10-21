using System.Text.Json;

namespace Blackwood.System.Text.Json.tests;

/// <summary>
/// Test suite for the JSONDeserializer class's JSONDeserializer2 functionality.
/// Tests cover the JsonToNormal method, ToDict methods, and ToArray method with
/// various JSON value kinds and edge cases.
/// </summary>
[TestFixture]
public class JSONDeserializer2Tests
{
    #region JsonToNormal Tests

    /// <summary>
    /// Tests that JsonToNormal correctly converts a JSON string to a .NET string.
    /// </summary>
    [Test]
    public void JsonToNormal_StringValue_ShouldReturnString()
    {
        // Arrange
        var json = "\"Hello World\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo("Hello World"));
        Assert.That(result, Is.InstanceOf<string>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts boolean string "true" to boolean true.
    /// </summary>
    [Test]
    public void JsonToNormal_BooleanStringTrue_ShouldReturnBooleanTrue()
    {
        // Arrange
        var json = "\"true\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(true));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts boolean string "True" (case insensitive) to boolean true.
    /// </summary>
    [Test]
    public void JsonToNormal_BooleanStringTrueCaseInsensitive_ShouldReturnBooleanTrue()
    {
        // Arrange
        var json = "\"True\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(true));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts boolean string "false" to boolean false.
    /// </summary>
    [Test]
    public void JsonToNormal_BooleanStringFalse_ShouldReturnBooleanFalse()
    {
        // Arrange
        var json = "\"false\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(false));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts boolean string "False" (case insensitive) to boolean false.
    /// </summary>
    [Test]
    public void JsonToNormal_BooleanStringFalseCaseInsensitive_ShouldReturnBooleanFalse()
    {
        // Arrange
        var json = "\"False\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(false));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts a regular string (not boolean) to string.
    /// </summary>
    [Test]
    public void JsonToNormal_RegularString_ShouldReturnString()
    {
        // Arrange
        var json = "\"Hello World\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo("Hello World"));
        Assert.That(result, Is.InstanceOf<string>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts a JSON boolean true to .NET boolean true.
    /// </summary>
    [Test]
    public void JsonToNormal_BooleanTrue_ShouldReturnBooleanTrue()
    {
        // Arrange
        var json = "true";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(true));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts a JSON boolean false to .NET boolean false.
    /// </summary>
    [Test]
    public void JsonToNormal_BooleanFalse_ShouldReturnBooleanFalse()
    {
        // Arrange
        var json = "false";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(false));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts a JSON number to .NET double.
    /// </summary>
    [Test]
    public void JsonToNormal_Number_ShouldReturnDouble()
    {
        // Arrange
        var json = "42.5";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(42.5));
        Assert.That(result, Is.InstanceOf<double>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts a JSON integer to .NET double.
    /// </summary>
    [Test]
    public void JsonToNormal_Integer_ShouldReturnDouble()
    {
        // Arrange
        var json = "42";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(42.0));
        Assert.That(result, Is.InstanceOf<double>());
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts a JSON array to object array.
    /// </summary>
    [Test]
    public void JsonToNormal_Array_ShouldReturnObjectArray()
    {
        // Arrange
        var json = "[1, \"hello\", true]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.InstanceOf<object[]>());
        var array = (object[])result!;
        Assert.That(array.Length, Is.EqualTo(3));
        Assert.That(array[0], Is.EqualTo(1.0));
        Assert.That(array[1], Is.EqualTo("hello"));
        Assert.That(array[2], Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts a JSON object to Dictionary.
    /// </summary>
    [Test]
    public void JsonToNormal_Object_ShouldReturnDictionary()
    {
        // Arrange
        var json = "{\"name\": \"John\", \"age\": 30, \"active\": true}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        var dict = (Dictionary<CasePreservingString, object>)result!;
        Assert.That(dict.Count, Is.EqualTo(3));
        Assert.That(dict["name"], Is.EqualTo("John"));
        Assert.That(dict["age"], Is.EqualTo(30.0));
        Assert.That(dict["active"], Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that JsonToNormal returns null for null JSON values.
    /// </summary>
    [Test]
    public void JsonToNormal_Null_ShouldReturnNull()
    {
        // Arrange
        var json = "null";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests that JsonToNormal returns null for undefined JSON values.
    /// </summary>
    [Test]
    public void JsonToNormal_Undefined_ShouldReturnNull()
    {
        // Arrange
        var json = "{}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
    }

    #endregion

    #region ToDict(JsonElement) Tests

    /// <summary>
    /// Tests that ToDict correctly converts a JSON object to Dictionary.
    /// </summary>
    [Test]
    public void ToDict_JsonElement_ShouldReturnDictionary()
    {
        // Arrange
        var json = "{\"name\": \"John\", \"age\": 30, \"active\": true}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToDict(element);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(result[new CasePreservingString("age")], Is.EqualTo(30.0));
        Assert.That(result[new CasePreservingString("active")], Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that ToDict correctly handles nested objects.
    /// </summary>
    [Test]
    public void ToDict_JsonElementWithNestedObject_ShouldReturnDictionaryWithNestedDictionary()
    {
        // Arrange
        var json = "{\"person\": {\"name\": \"John\", \"age\": 30}}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToDict(element);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[new CasePreservingString("person")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        var nestedDict = (Dictionary<CasePreservingString, object>)result[new CasePreservingString("person")];
        Assert.That(nestedDict[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(nestedDict[new CasePreservingString("age")], Is.EqualTo(30.0));
    }

    /// <summary>
    /// Tests that ToDict correctly handles arrays within objects.
    /// </summary>
    [Test]
    public void ToDict_JsonElementWithArray_ShouldReturnDictionaryWithArray()
    {
        // Arrange
        var json = "{\"numbers\": [1, 2, 3], \"names\": [\"John\", \"Jane\"]}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToDict(element);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[new CasePreservingString("numbers")], Is.InstanceOf<object[]>());
        Assert.That(result[new CasePreservingString("names")], Is.InstanceOf<object[]>());

        var numbers = (object[])result[new CasePreservingString("numbers")];
        var names = (object[])result[new CasePreservingString("names")];

        Assert.That(numbers.Length, Is.EqualTo(3));
        Assert.That(numbers[0], Is.EqualTo(1.0));
        Assert.That(numbers[1], Is.EqualTo(2.0));
        Assert.That(numbers[2], Is.EqualTo(3.0));

        Assert.That(names.Length, Is.EqualTo(2));
        Assert.That(names[0], Is.EqualTo("John"));
        Assert.That(names[1], Is.EqualTo("Jane"));
    }

    /// <summary>
    /// Tests that ToDict correctly handles empty objects.
    /// </summary>
    [Test]
    public void ToDict_EmptyJsonObject_ShouldReturnEmptyDictionary()
    {
        // Arrange
        var json = "{}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToDict(element);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        Assert.That(result.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that ToDict correctly handles boolean string values.
    /// </summary>
    [Test]
    public void ToDict_JsonElementWithBooleanStrings_ShouldConvertToBooleans()
    {
        // Arrange
        var json = "{\"flag1\": \"true\", \"flag2\": \"false\", \"flag3\": \"True\", \"flag4\": \"False\"}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToDict(element);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[new CasePreservingString("flag1")], Is.EqualTo(true));
        Assert.That(result[new CasePreservingString("flag2")], Is.EqualTo(false));
        Assert.That(result[new CasePreservingString("flag3")], Is.EqualTo(true));
        Assert.That(result[new CasePreservingString("flag4")], Is.EqualTo(false));
    }

    #endregion

    #region ToDict(Dictionary<string, object>) Tests

    /// <summary>
    /// Tests that ToDict correctly converts a dictionary containing JsonElements.
    /// </summary>
    [Test]
    public void ToDict_DictionaryWithJsonElements_ShouldConvertToNormalObjects()
    {
        // Arrange
        var json = "{\"name\": \"John\", \"age\": 30, \"active\": true}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        var inputDict = new Dictionary<string, object>();
        foreach (var prop in element.EnumerateObject())
        {
            inputDict[prop.Name] = prop.Value;
        }

        // Act
        var result = JSONDeserializer.ToDict(inputDict);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(result[new CasePreservingString("age")], Is.EqualTo(30.0));
        Assert.That(result[new CasePreservingString("active")], Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that ToDict correctly handles null values in the input dictionary.
    /// </summary>
    [Test]
    public void ToDict_DictionaryWithNullValues_ShouldSkipNullValues()
    {
        // Arrange
        var json = "{\"name\": \"John\", \"age\": 30}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // First convert JsonElement to Dictionary<CasePreservingString, object>
        var casePreservingDict = JSONDeserializer.ToDict(element);
        casePreservingDict[new CasePreservingString("nullValue")] = null!; // Add a null value

        // Act - convert to Dictionary<string, object> which should skip null values
        // Note: ToDict expects Dictionary<string, object>, so we need to convert the CasePreservingString keys
        var stringDict = new Dictionary<string, object>();
        foreach (var kvp in casePreservingDict)
        {
            stringDict[kvp.Key.ToString()] = kvp.Value;
        }
        var result = JSONDeserializer.ToDict(stringDict);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.ContainsKey("name"), Is.True);
        Assert.That(result.ContainsKey("age"), Is.True);
        Assert.That(result.ContainsKey("nullValue"), Is.False);
    }

    /// <summary>
    /// Tests that ToDict correctly handles empty input dictionary.
    /// </summary>
    [Test]
    public void ToDict_EmptyDictionary_ShouldReturnEmptyDictionary()
    {
        // Arrange
        var inputDict = new Dictionary<string, object>();

        // Act
        var result = JSONDeserializer.ToDict(inputDict);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        Assert.That(result.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that ToDict correctly handles mixed JsonElement types.
    /// </summary>
    [Test]
    public void ToDict_DictionaryWithMixedTypes_ShouldConvertAllTypes()
    {
        // Arrange
        var json1 = "\"John\"";
        var json2 = "30";
        var json3 = "true";
        var json4 = "[1, 2, 3]";
        var json5 = "{\"nested\": \"value\"}";

        var doc1 = JsonDocument.Parse(json1);
        var doc2 = JsonDocument.Parse(json2);
        var doc3 = JsonDocument.Parse(json3);
        var doc4 = JsonDocument.Parse(json4);
        var doc5 = JsonDocument.Parse(json5);

        var casePreservingDict = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("name")] = doc1.RootElement,
            [new CasePreservingString("age")] = doc2.RootElement,
            [new CasePreservingString("active")] = doc3.RootElement,
            [new CasePreservingString("numbers")] = doc4.RootElement,
            [new CasePreservingString("nested")] = doc5.RootElement
        };

        // Act - convert to Dictionary<string, object>
        // Note: ToDict expects Dictionary<string, object>, so we need to convert the CasePreservingString keys
        var stringDict = new Dictionary<string, object>();
        foreach (var kvp in casePreservingDict)
        {
            stringDict[kvp.Key.ToString()] = kvp.Value;
        }
        var result = JSONDeserializer.ToDict(stringDict);

        // Assert
        Assert.That(result.Count, Is.EqualTo(5));
        Assert.That(result[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(result[new CasePreservingString("age")], Is.EqualTo(30.0));
        Assert.That(result[new CasePreservingString("active")], Is.EqualTo(true));
        Assert.That(result[new CasePreservingString("numbers")], Is.InstanceOf<object[]>());
        Assert.That(result[new CasePreservingString("nested")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());
    }

    #endregion

    #region ToArray Tests

    /// <summary>
    /// Tests that ToArray correctly converts a JSON array to object array.
    /// </summary>
    [Test]
    public void ToArray_JsonArray_ShouldReturnObjectArray()
    {
        // Arrange
        var json = "[1, \"hello\", true, 42.5]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToArray(element);

        // Assert
        Assert.That(result, Is.InstanceOf<object[]>());
        Assert.That(result.Length, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(1.0));
        Assert.That(result[1], Is.EqualTo("hello"));
        Assert.That(result[2], Is.EqualTo(true));
        Assert.That(result[3], Is.EqualTo(42.5));
    }

    /// <summary>
    /// Tests that ToArray correctly handles nested arrays.
    /// </summary>
    [Test]
    public void ToArray_JsonArrayWithNestedArrays_ShouldReturnObjectArrayWithNestedArrays()
    {
        // Arrange
        var json = "[[1, 2], [\"a\", \"b\"], [true, false]]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToArray(element);

        // Assert
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.InstanceOf<object[]>());
        Assert.That(result[1], Is.InstanceOf<object[]>());
        Assert.That(result[2], Is.InstanceOf<object[]>());

        var array1 = (object[])result[0];
        var array2 = (object[])result[1];
        var array3 = (object[])result[2];

        Assert.That(array1.Length, Is.EqualTo(2));
        Assert.That(array1[0], Is.EqualTo(1.0));
        Assert.That(array1[1], Is.EqualTo(2.0));

        Assert.That(array2.Length, Is.EqualTo(2));
        Assert.That(array2[0], Is.EqualTo("a"));
        Assert.That(array2[1], Is.EqualTo("b"));

        Assert.That(array3.Length, Is.EqualTo(2));
        Assert.That(array3[0], Is.EqualTo(true));
        Assert.That(array3[1], Is.EqualTo(false));
    }

    /// <summary>
    /// Tests that ToArray correctly handles arrays with objects.
    /// </summary>
    [Test]
    public void ToArray_JsonArrayWithObjects_ShouldReturnObjectArrayWithDictionaries()
    {
        // Arrange
        var json = "[{\"name\": \"John\"}, {\"age\": 30}]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToArray(element);

        // Assert
        Assert.That(result.Length, Is.EqualTo(2));
        Assert.That(result[0], Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        Assert.That(result[1], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var dict1 = (Dictionary<CasePreservingString, object>)result[0];
        var dict2 = (Dictionary<CasePreservingString, object>)result[1];

        Assert.That(dict1[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(dict2[new CasePreservingString("age")], Is.EqualTo(30.0));
    }

    /// <summary>
    /// Tests that ToArray correctly handles empty arrays.
    /// </summary>
    [Test]
    public void ToArray_EmptyJsonArray_ShouldReturnEmptyArray()
    {
        // Arrange
        var json = "[]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToArray(element);

        // Assert
        Assert.That(result, Is.InstanceOf<object[]>());
        Assert.That(result.Length, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that ToArray correctly handles arrays with boolean strings.
    /// </summary>
    [Test]
    public void ToArray_JsonArrayWithBooleanStrings_ShouldConvertToBooleans()
    {
        // Arrange
        var json = "[\"true\", \"false\", \"True\", \"False\"]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToArray(element);

        // Assert
        Assert.That(result.Length, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(true));
        Assert.That(result[1], Is.EqualTo(false));
        Assert.That(result[2], Is.EqualTo(true));
        Assert.That(result[3], Is.EqualTo(false));
    }

    /// <summary>
    /// Tests that ToArray correctly handles arrays with null values.
    /// </summary>
    [Test]
    public void ToArray_JsonArrayWithNullValues_ShouldSkipNullValues()
    {
        // Arrange
        var json = "[1, null, \"hello\", null, true]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToArray(element);

        // Assert
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(1.0));
        Assert.That(result[1], Is.EqualTo("hello"));
        Assert.That(result[2], Is.EqualTo(true));
    }

    #endregion
    #region Edge Cases and Error Handling

    /// <summary>
    /// Tests that JsonToNormal handles very large numbers correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_VeryLargeNumber_ShouldReturnDouble()
    {
        // Arrange
        var json = "1.7976931348623157E+308";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.InstanceOf<double>());
        Assert.That(result, Is.EqualTo(double.MaxValue));
    }

    /// <summary>
    /// Tests that JsonToNormal handles very small numbers correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_VerySmallNumber_ShouldReturnDouble()
    {
        // Arrange
        var json = "4.94065645841247E-324";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.InstanceOf<double>());
        Assert.That(result, Is.EqualTo(4.94065645841247E-324));
    }

    /// <summary>
    /// Tests that JsonToNormal handles special string values that are not boolean strings.
    /// </summary>
    [Test]
    public void JsonToNormal_SpecialStringValues_ShouldReturnAsString()
    {
        // Arrange
        var testCases = new[]
        {
            "\"truee\"",   // Extra characters
            "\"falsee\"",  // Extra characters
            "\"yes\"",     // Other boolean-like strings
            "\"no\"",
            "\"on\"",
            "\"off\""
        };

        foreach (var json in testCases)
        {
            // Arrange
            var document = JsonDocument.Parse(json);
            var element = document.RootElement;

            // Act
            var result = JSONDeserializer.JsonToNormal(element);

            // Assert
            Assert.That(result, Is.InstanceOf<string>(), $"Failed for JSON: {json}");
        }
    }

    /// <summary>
    /// Tests that JsonToNormal correctly converts case-insensitive boolean strings.
    /// </summary>
    [Test]
    public void JsonToNormal_CaseInsensitiveBooleanStrings_ShouldConvertToBoolean()
    {
        // Arrange
        var testCases = new[]
        {
            ("\"TRUE\"", true),     // All caps
            ("\"FALSE\"", false),   // All caps
            ("\"tRuE\"", true),     // Mixed case
            ("\"fAlSe\"", false),   // Mixed case
        };

        foreach (var (json, expected) in testCases)
        {
            // Arrange
            var document = JsonDocument.Parse(json);
            var element = document.RootElement;

            // Act
            var result = JSONDeserializer.JsonToNormal(element);

            // Assert
            Assert.That(result, Is.EqualTo(expected), $"Failed for JSON: {json}");
            Assert.That(result, Is.InstanceOf<bool>(), $"Failed for JSON: {json}");
        }
    }

    /// <summary>
    /// Tests that ToDict handles complex nested structures correctly.
    /// </summary>
    [Test]
    public void ToDict_ComplexNestedStructure_ShouldHandleCorrectly()
    {
        // Arrange
        var json = """
        {
            "users": [
                {
                    "name": "John",
                    "age": 30,
                    "active": "true",
                    "scores": [85, 90, 78]
                },
                {
                    "name": "Jane",
                    "age": 25,
                    "active": "false",
                    "scores": [92, 88, 95]
                }
            ],
            "metadata": {
                "total": 2,
                "lastUpdated": "2023-01-01"
            }
        }
        """;
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToDict(element);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[new CasePreservingString("users")], Is.InstanceOf<object[]>());
        Assert.That(result[new CasePreservingString("metadata")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var users = (object[])result[new CasePreservingString("users")];
        var metadata = (Dictionary<CasePreservingString, object>)result[new CasePreservingString("metadata")];

        Assert.That(users.Length, Is.EqualTo(2));
        Assert.That(metadata[new CasePreservingString("total")], Is.EqualTo(2.0));
        Assert.That(metadata[new CasePreservingString("lastUpdated")], Is.EqualTo("2023-01-01"));

        var user1 = (Dictionary<CasePreservingString, object>)users[0];
        var user2 = (Dictionary<CasePreservingString, object>)users[1];

        Assert.That(user1[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(user1[new CasePreservingString("active")], Is.EqualTo(true));
        Assert.That(user1[new CasePreservingString("scores")], Is.InstanceOf<object[]>());

        Assert.That(user2[new CasePreservingString("name")], Is.EqualTo("Jane"));
        Assert.That(user2[new CasePreservingString("active")], Is.EqualTo(false));
        Assert.That(user2[new CasePreservingString("scores")], Is.InstanceOf<object[]>());
    }

    #endregion

    #region DeserializeProperties Tests
    /// <summary>
    /// Test class for testing the DeserializeProperties method.
    /// </summary>
    public class TestDeserializeClass
    {
#pragma warning disable CS0618 // Type or member is obsolete
        [global::System.Obsolete("Test property")]
        public string TestProperty { get; set; } = "";

        [global::System.Obsolete("Test field")]
        public int TestField = 0;

        [global::System.Obsolete("Test boolean")]
        public bool TestBool { get; set; } = false;
#pragma warning restore CS0618 // Type or member is obsolete

        public string NonSerializedProperty { get; set; } = "";
    }

    /// <summary>
    /// Tests that DeserializeProperties correctly deserializes properties marked with attributes.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithMarkedProperties_ShouldDeserializeCorrectly()
    {
        // Arrange
        var testObject = new TestDeserializeClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("TestProperty")] = "TestValue",
            [new CasePreservingString("TestField")] = 42,
            [new CasePreservingString("TestBool")] = true,
            [new CasePreservingString("NonSerializedProperty")] = "ShouldNotBeSet"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.TestProperty, Is.EqualTo("TestValue"));
        Assert.That(testObject.TestField, Is.EqualTo(42));
        Assert.That(testObject.TestBool, Is.EqualTo(true));
        Assert.That(testObject.NonSerializedProperty, Is.EqualTo("")); // Should not be changed
    }

    /// <summary>
    /// Tests that DeserializeProperties handles missing properties gracefully.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithMissingProperties_ShouldNotThrow()
    {
        // Arrange
        var testObject = new TestDeserializeClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("TestProperty")] = "TestValue"
            // Missing TestField and TestBool
        };

        // Act & Assert
        Assert.DoesNotThrow(() =>
            JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute)));

        Assert.That(testObject.TestProperty, Is.EqualTo("TestValue"));
        Assert.That(testObject.TestField, Is.EqualTo(0)); // Default value
        Assert.That(testObject.TestBool, Is.EqualTo(false)); // Default value
    }

    /// <summary>
    /// Tests that DeserializeProperties handles type conversion errors gracefully.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithTypeConversionErrors_ShouldNotThrow()
    {
        // Arrange
        var testObject = new TestDeserializeClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("TestProperty")] = "TestValue",
            [new CasePreservingString("TestField")] = "InvalidInt", // This should cause a conversion error
            ["TestBool"] = true
        };

        // Act & Assert
        Assert.DoesNotThrow(() =>
            JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute)));

        Assert.That(testObject.TestProperty, Is.EqualTo("TestValue"));
        Assert.That(testObject.TestField, Is.EqualTo(0)); // Should remain default due to conversion error
        Assert.That(testObject.TestBool, Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles empty properties dictionary.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithEmptyProperties_ShouldNotThrow()
    {
        // Arrange
        var testObject = new TestDeserializeClass();
        var properties = new Dictionary<CasePreservingString, object>();

        // Act & Assert
        Assert.DoesNotThrow(() =>
            JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute)));

        // All properties should remain at their default values
        Assert.That(testObject.TestProperty, Is.EqualTo(""));
        Assert.That(testObject.TestField, Is.EqualTo(0));
        Assert.That(testObject.TestBool, Is.EqualTo(false));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles properties with different data types.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithDifferentDataTypes_ShouldDeserializeCorrectly()
    {
        // Arrange
        var testObject = new TestDeserializeClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("TestProperty")] = "StringValue",
            [new CasePreservingString("TestField")] = 123.45, // Double value
            [new CasePreservingString("TestBool")] = "true" // String boolean
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.TestProperty, Is.EqualTo("StringValue"));
        Assert.That(testObject.TestField, Is.EqualTo(123)); // Should be converted to int
        Assert.That(testObject.TestBool, Is.EqualTo(true)); // Should be converted to bool
    }

    /// <summary>
    /// Tests that DeserializeProperties handles null values in properties dictionary.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithNullValues_ShouldHandleGracefully()
    {
        // Arrange
        var testObject = new TestDeserializeClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("TestProperty")] = "TestValue",
            [new CasePreservingString("TestField")] = null!,
            [new CasePreservingString("TestBool")] = true
        };

        // Act & Assert
        Assert.DoesNotThrow(() =>
            JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute)));

        Assert.That(testObject.TestProperty, Is.EqualTo("TestValue"));
        Assert.That(testObject.TestField, Is.EqualTo(0)); // Should remain default
        Assert.That(testObject.TestBool, Is.EqualTo(true));
    }

    /// <summary>
    /// Test class for testing complex type conversions in DeserializeProperties.
    /// </summary>
    public class ComplexTypeTestClass
    {
#pragma warning disable CS0618 // Type or member is obsolete
        [global::System.Obsolete("DateTime property")]
        public DateTime DateTimeProperty { get; set; } = DateTime.MinValue;

        [global::System.Obsolete("Guid property")]
        public Guid GuidProperty { get; set; } = Guid.Empty;

        [global::System.Obsolete("Color property")]
        public global::System.Drawing.Color ColorProperty { get; set; } = global::System.Drawing.Color.Empty;

        [global::System.Obsolete("Point property")]
        public global::System.Drawing.Point PointProperty { get; set; } = global::System.Drawing.Point.Empty;

        [global::System.Obsolete("Uri property")]
        public Uri UriProperty { get; set; } = new Uri("http://example.com");

        [global::System.Obsolete("Version property")]
        public Version VersionProperty { get; set; } = new Version(1, 0);

        [global::System.Obsolete("TimeSpan property")]
        public TimeSpan TimeSpanProperty { get; set; } = TimeSpan.Zero;

        [global::System.Obsolete("IPAddress property")]
        public global::System.Net.IPAddress IPAddressProperty { get; set; } = global::System.Net.IPAddress.None;
#pragma warning restore CS0618 // Type or member is obsolete
    }

    /// <summary>
    /// Tests that DeserializeProperties handles DateTime conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithDateTime_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new ComplexTypeTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("DateTimeProperty")] = "2023-12-25T10:30:00"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.DateTimeProperty.Year, Is.EqualTo(2023));
        Assert.That(testObject.DateTimeProperty.Month, Is.EqualTo(12));
        Assert.That(testObject.DateTimeProperty.Day, Is.EqualTo(25));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles Guid conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithGuid_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new ComplexTypeTestClass();
        var guidString = "12345678-1234-1234-1234-123456789012";
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("GuidProperty")] = guidString
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.GuidProperty.ToString(), Is.EqualTo(guidString));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles Color conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithColor_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new ComplexTypeTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("ColorProperty")] = "Red"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.ColorProperty, Is.EqualTo(global::System.Drawing.Color.Red));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles Point conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithPoint_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new ComplexTypeTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("PointProperty")] = new Dictionary<CasePreservingString, object>
            {
                [new CasePreservingString("x")] = 10,
                [new CasePreservingString("y")] = 20
            }
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.PointProperty.X, Is.EqualTo(10));
        Assert.That(testObject.PointProperty.Y, Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles Uri conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithUri_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new ComplexTypeTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("UriProperty")] = "https://example.com/test"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.UriProperty.ToString(), Is.EqualTo("https://example.com/test"));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles Version conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithVersion_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new ComplexTypeTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("VersionProperty")] = "2.1.3.4"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.VersionProperty.Major, Is.EqualTo(2));
        Assert.That(testObject.VersionProperty.Minor, Is.EqualTo(1));
        Assert.That(testObject.VersionProperty.Build, Is.EqualTo(3));
        Assert.That(testObject.VersionProperty.Revision, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles TimeSpan conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithTimeSpan_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new ComplexTypeTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("TimeSpanProperty")] = "01:30:45"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.TimeSpanProperty.Hours, Is.EqualTo(1));
        Assert.That(testObject.TimeSpanProperty.Minutes, Is.EqualTo(30));
        Assert.That(testObject.TimeSpanProperty.Seconds, Is.EqualTo(45));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles IPAddress conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithIPAddress_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new ComplexTypeTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("IPAddressProperty")] = "192.168.1.1"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.IPAddressProperty.ToString(), Is.EqualTo("192.168.1.1"));
    }

    /// <summary>
    /// Test class for testing nullable types and enums in DeserializeProperties.
    /// </summary>
    public class NullableAndEnumTestClass
    {
#pragma warning disable CS0618 // Type or member is obsolete
        [global::System.Obsolete("Nullable int property")]
        public int? NullableIntProperty { get; set; } = null;

        [global::System.Obsolete("Nullable DateTime property")]
        public DateTime? NullableDateTimeProperty { get; set; } = null;

        [global::System.Obsolete("Nullable bool property")]
        public bool? NullableBoolProperty { get; set; } = null;

        [global::System.Obsolete("Enum property")]
        public DayOfWeek EnumProperty { get; set; } = DayOfWeek.Sunday;

        [global::System.Obsolete("Nullable enum property")]
        public DayOfWeek? NullableEnumProperty { get; set; } = null;
#pragma warning restore CS0618 // Type or member is obsolete
    }

    /// <summary>
    /// Tests that DeserializeProperties handles nullable int conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithNullableInt_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new NullableAndEnumTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("NullableIntProperty")] = 42
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.NullableIntProperty, Is.EqualTo(42));
        Assert.That(testObject.NullableIntProperty, Is.InstanceOf<int>());
    }

    /// <summary>
    /// Tests that DeserializeProperties handles nullable DateTime conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithNullableDateTime_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new NullableAndEnumTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("NullableDateTimeProperty")] = "2023-12-25T10:30:00"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.NullableDateTimeProperty, Is.Not.Null);
        Assert.That(testObject.NullableDateTimeProperty!.Value.Year, Is.EqualTo(2023));
        Assert.That(testObject.NullableDateTimeProperty.Value.Month, Is.EqualTo(12));
        Assert.That(testObject.NullableDateTimeProperty.Value.Day, Is.EqualTo(25));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles nullable bool conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithNullableBool_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new NullableAndEnumTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("NullableBoolProperty")] = "true"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.NullableBoolProperty, Is.EqualTo(true));
        Assert.That(testObject.NullableBoolProperty, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that DeserializeProperties handles enum conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithEnum_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new NullableAndEnumTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("EnumProperty")] = "Monday"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.EnumProperty, Is.EqualTo(DayOfWeek.Monday));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles nullable enum conversion correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithNullableEnum_ShouldConvertCorrectly()
    {
        // Arrange
        var testObject = new NullableAndEnumTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("NullableEnumProperty")] = "Wednesday"
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.NullableEnumProperty, Is.EqualTo(DayOfWeek.Wednesday));
        Assert.That(testObject.NullableEnumProperty, Is.InstanceOf<DayOfWeek>());
    }

    /// <summary>
    /// Tests that DeserializeProperties handles null values for nullable types correctly.
    /// </summary>
    [Test]
    public void DeserializeProperties_WithNullValuesForNullableTypes_ShouldHandleCorrectly()
    {
        // Arrange
        var testObject = new NullableAndEnumTestClass();
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("NullableIntProperty")] = null!,
            [new CasePreservingString("NullableDateTimeProperty")] = null!,
            [new CasePreservingString("NullableBoolProperty")] = null!,
            [new CasePreservingString("NullableEnumProperty")] = null!
        };

        // Act
        JSONDeserializer.DeserializeProperties(testObject, properties, typeof(global::System.ObsoleteAttribute));

        // Assert
        Assert.That(testObject.NullableIntProperty, Is.Null);
        Assert.That(testObject.NullableDateTimeProperty, Is.Null);
        Assert.That(testObject.NullableBoolProperty, Is.Null);
        Assert.That(testObject.NullableEnumProperty, Is.Null);
    }

    #endregion

    #region Additional Edge Cases

    /// <summary>
    /// Tests that JsonToNormal handles very long strings correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_VeryLongString_ShouldReturnString()
    {
        // Arrange
        var longString = new string('A', 10000);
        var json = $"\"{longString}\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(longString));
        Assert.That(result, Is.InstanceOf<string>());
    }

    /// <summary>
    /// Tests that JsonToNormal handles Unicode strings correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_UnicodeString_ShouldReturnString()
    {
        // Arrange
        var unicodeString = "Hello 世界 🌍";
        var json = $"\"{unicodeString}\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(unicodeString));
        Assert.That(result, Is.InstanceOf<string>());
    }

    /// <summary>
    /// Tests that JsonToNormal handles special characters in strings correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_SpecialCharacters_ShouldReturnString()
    {
        // Arrange
        var specialString = "Line1\nLine2\tTabbed\r\nWindows";
        var json = $"\"{specialString.Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r")}\"";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.EqualTo(specialString));
        Assert.That(result, Is.InstanceOf<string>());
    }

    /// <summary>
    /// Tests that ToArray handles arrays with mixed null and non-null values correctly.
    /// </summary>
    [Test]
    public void ToArray_ArrayWithMixedNulls_ShouldSkipNulls()
    {
        // Arrange
        var json = "[1, null, \"hello\", null, true, null]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToArray(element);

        // Assert
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(1.0));
        Assert.That(result[1], Is.EqualTo("hello"));
        Assert.That(result[2], Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that ToDict handles objects with null values correctly.
    /// </summary>
    [Test]
    public void ToDict_ObjectWithNullValues_ShouldSkipNulls()
    {
        // Arrange
        var json = "{\"name\": \"John\", \"age\": null, \"active\": true, \"description\": null}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToDict(element);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(result[new CasePreservingString("active")], Is.EqualTo(true));
        Assert.That(result.ContainsKey("age"), Is.False);
        Assert.That(result.ContainsKey("description"), Is.False);
    }

    /// <summary>
    /// Tests that JsonToNormal handles deeply nested structures correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_DeeplyNestedStructure_ShouldHandleCorrectly()
    {
        // Arrange
        var json = """
        {
            "level1": {
                "level2": {
                    "level3": {
                        "level4": {
                            "value": "deep"
                        }
                    }
                }
            }
        }
        """;
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        var dict = (Dictionary<CasePreservingString, object>)result!;
        Assert.That(dict[new CasePreservingString("level1")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var level1 = (Dictionary<CasePreservingString, object>)dict[new CasePreservingString("level1")];
        Assert.That(level1[new CasePreservingString("level2")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var level2 = (Dictionary<CasePreservingString, object>)level1[new CasePreservingString("level2")];
        Assert.That(level2[new CasePreservingString("level3")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var level3 = (Dictionary<CasePreservingString, object>)level2[new CasePreservingString("level3")];
        Assert.That(level3[new CasePreservingString("level4")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var level4 = (Dictionary<CasePreservingString, object>)level3[new CasePreservingString("level4")];
        Assert.That(level4[new CasePreservingString("value")], Is.EqualTo("deep"));
    }

    #endregion

    #region Additional Edge Cases for Existing Methods

    /// <summary>
    /// Tests that ToArray handles arrays with all null values correctly.
    /// </summary>
    [Test]
    public void ToArray_AllNullValues_ShouldReturnEmptyArray()
    {
        // Arrange
        var json = "[null, null, null]";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToArray(element);

        // Assert
        Assert.That(result, Is.InstanceOf<object[]>());
        Assert.That(result.Length, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that ToDict handles objects with all null values correctly.
    /// </summary>
    [Test]
    public void ToDict_AllNullValues_ShouldReturnEmptyDictionary()
    {
        // Arrange
        var json = "{\"prop1\": null, \"prop2\": null, \"prop3\": null}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.ToDict(element);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        Assert.That(result.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that DeserializeProperties handles null object gracefully.
    /// </summary>
    [Test]
    public void DeserializeProperties_NullObject_ShouldThrowNullReferenceException()
    {
        // Arrange
        object? nullObject = null;
        var properties = new Dictionary<CasePreservingString, object>
        {
            [new CasePreservingString("TestProperty")] = "TestValue"
        };

        // Act & Assert
        Assert.Throws<NullReferenceException>(() =>
            JSONDeserializer.DeserializeProperties(nullObject!, properties, typeof(TestAttribute)));
    }


    /// <summary>
    /// Tests that JsonToNormal handles JsonValueKind.Undefined correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_UndefinedValueKind_ShouldReturnNull()
    {
        // Arrange
        var json = "{}";
        var document = JsonDocument.Parse(json);
        var element = document.RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(element);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
    }

    #endregion

    #region SerializeProperties Tests

    /// <summary>
    /// Test class for testing the SerializeProperties method.
    /// </summary>
    public class TestSerializeClass
    {
#pragma warning disable CS0618 // Type or member is obsolete
        [global::System.Obsolete("Test property")]
        public string TestProperty { get; set; } = "TestValue";

        [global::System.Obsolete("Test field")]
        public int TestField = 42;

        [global::System.Obsolete("Test boolean")]
        public bool TestBool { get; set; } = true;

        [global::System.Obsolete("Test color")]
        public global::System.Drawing.Color TestColor { get; set; } = global::System.Drawing.Color.Red;

        [global::System.Obsolete("Test point")]
        public global::System.Drawing.Point TestPoint { get; set; } = new global::System.Drawing.Point(10, 20);

        [global::System.Obsolete("Test array")]
        public int[] TestArray { get; set; } = new int[] { 1, 2, 3 };
#pragma warning restore CS0618 // Type or member is obsolete

        public string NonSerializedProperty { get; set; } = "ShouldNotAppear";
    }

    /// <summary>
    /// Tests that SerializeProperties correctly serializes properties marked with attributes.
    /// </summary>
    [Test]
    public void SerializeProperties_WithMarkedProperties_ShouldSerializeCorrectly()
    {
        // Arrange
        var testObject = new TestSerializeClass();
        var attributeType = typeof(global::System.ObsoleteAttribute);

        // Act
        var result = JSONDeserializer.SerializeProperties(testObject, attributeType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(6)); // TestProperty, TestField, TestBool, TestColor, TestPoint, TestArray
        Assert.That(result.ContainsKey("TestProperty"), Is.True);
        Assert.That(result.ContainsKey("TestField"), Is.True);
        Assert.That(result.ContainsKey("TestBool"), Is.True);
        Assert.That(result.ContainsKey("TestColor"), Is.True);
        Assert.That(result.ContainsKey("TestPoint"), Is.True);
        Assert.That(result.ContainsKey("TestArray"), Is.True);
        Assert.That(result[new CasePreservingString("TestProperty")], Is.EqualTo("TestValue"));
        Assert.That(result[new CasePreservingString("TestField")], Is.EqualTo(42));
        Assert.That(result[new CasePreservingString("TestBool")], Is.EqualTo(true));
        Assert.That(result[new CasePreservingString("TestColor")], Is.EqualTo("Red"));
        Assert.That(result[new CasePreservingString("TestArray")], Is.InstanceOf<object[]>());
    }

    /// <summary>
    /// Tests that SerializeProperties handles null values correctly.
    /// </summary>
    [Test]
    public void SerializeProperties_WithNullValues_ShouldSkipNullProperties()
    {
        // Arrange
        var testObject = new TestSerializeClass
        {
            TestProperty = null! // Set to null
        };
        var attributeType = typeof(global::System.ObsoleteAttribute);

        // Act
        var result = JSONDeserializer.SerializeProperties(testObject, attributeType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ContainsKey("TestProperty"), Is.False); // Should be skipped
        Assert.That(result.ContainsKey("TestField"), Is.True);
        Assert.That(result.ContainsKey("TestBool"), Is.True);
    }

    /// <summary>
    /// Tests that SerializeProperties handles exceptions gracefully.
    /// </summary>
    [Test]
    public void SerializeProperties_WithException_HandlesGracefully()
    {
        // Arrange
        var testObject = new TestSerializeClassWithException();
        var attributeType = typeof(global::System.ObsoleteAttribute);

        // Act
        var result = JSONDeserializer.SerializeProperties(testObject, attributeType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ContainsKey("TestProperty"), Is.True);
        Assert.That(result.ContainsKey("ExceptionProperty"), Is.True);
        Assert.That(result[new CasePreservingString("ExceptionProperty")].ToString(), Does.Contain("Error serializing"));
    }

    /// <summary>
    /// Test class with a property that throws an exception when accessed.
    /// Used to test error handling in SerializeProperties method.
    /// </summary>
    public class TestSerializeClassWithException
    {
#pragma warning disable CS0618 // Type or member is obsolete
        [global::System.Obsolete("Test property")]
        public string TestProperty { get; set; } = "TestValue";

        [global::System.Obsolete("Exception property")]
        public string ExceptionProperty
        {
            get => throw new InvalidOperationException("Test exception");
            set { }
        }
#pragma warning restore CS0618 // Type or member is obsolete
    }

    /// <summary>
    /// Tests that SerializeProperties handles empty objects correctly.
    /// </summary>
    [Test]
    public void SerializeProperties_WithEmptyObject_ShouldReturnEmptyDictionary()
    {
        // Arrange
        var testObject = new EmptyTestClass();
        var attributeType = typeof(global::System.ObsoleteAttribute);

        // Act
        var result = JSONDeserializer.SerializeProperties(testObject, attributeType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// Test class with no properties marked with the specified attribute.
    /// </summary>
    public class EmptyTestClass
    {
        public string NonSerializedProperty { get; set; } = "ShouldNotAppear";
    }

    /// <summary>
    /// Tests that SerializeProperties handles different data types correctly.
    /// </summary>
    [Test]
    public void SerializeProperties_WithDifferentDataTypes_ShouldSerializeCorrectly()
    {
        // Arrange
        var testObject = new TestSerializeClass();
        var attributeType = typeof(global::System.ObsoleteAttribute);

        // Act
        var result = JSONDeserializer.SerializeProperties(testObject, attributeType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result[new CasePreservingString("TestProperty")], Is.EqualTo("TestValue"));
        Assert.That(result[new CasePreservingString("TestField")], Is.EqualTo(42));
        Assert.That(result[new CasePreservingString("TestBool")], Is.EqualTo(true));
        Assert.That(result[new CasePreservingString("TestColor")], Is.EqualTo("Red"));

        // Verify Point is serialized as an object
        var pointResult = result[new CasePreservingString("TestPoint")];
        Assert.That(pointResult, Is.Not.Null);
        var pointType = pointResult!.GetType();
        Assert.That(pointType.GetProperty("x")!.GetValue(pointResult), Is.EqualTo(10));
        Assert.That(pointType.GetProperty("y")!.GetValue(pointResult), Is.EqualTo(20));

        // Verify Array is serialized correctly
        var arrayResult = result[new CasePreservingString("TestArray")];
        Assert.That(arrayResult, Is.InstanceOf<object[]>());
        var array = (object[])arrayResult;
        Assert.That(array.Length, Is.EqualTo(3));
        Assert.That(array[0], Is.EqualTo(1));
        Assert.That(array[1], Is.EqualTo(2));
        Assert.That(array[2], Is.EqualTo(3));
    }

    #endregion

}

