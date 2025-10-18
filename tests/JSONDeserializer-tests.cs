using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blackwood.System.Text.Json.tests;

/// <summary>
/// Test suite for the JSONDeserializer class.
/// Tests cover deserialization from strings and streams, JSON converter functionality,
/// helper methods for converting JsonElements, and various edge cases.
/// </summary>
[TestFixture]
public class JSONDeserializerTests
{
    #region Deserialize String Tests

    /// <summary>
    /// Tests that deserializing a simple string works correctly.
    /// </summary>
    [Test]
    public void Deserialize_SimpleString_ShouldReturnString()
    {
        // Arrange
        var json = "\"Hello World\"";

        // Act
        var result = JSONDeserializer.Deserialize<string>(json);

        // Assert
        Assert.That(result, Is.EqualTo("Hello World"));
    }

    /// <summary>
    /// Tests that deserializing an integer works correctly.
    /// </summary>
    [Test]
    public void Deserialize_Integer_ShouldReturnInt()
    {
        // Arrange
        var json = "42";

        // Act
        var result = JSONDeserializer.Deserialize<int>(json);

        // Assert
        Assert.That(result, Is.EqualTo(42));
    }

    /// <summary>
    /// Tests that deserializing a double works correctly.
    /// </summary>
    [Test]
    public void Deserialize_Double_ShouldReturnDouble()
    {
        // Arrange
        var json = "3.14159";

        // Act
        var result = JSONDeserializer.Deserialize<double>(json);

        // Assert
        Assert.That(result, Is.EqualTo(3.14159));
    }

    /// <summary>
    /// Tests that deserializing a boolean works correctly.
    /// </summary>
    [Test]
    public void Deserialize_Boolean_ShouldReturnBool()
    {
        // Arrange
        var json = "true";

        // Act
        var result = JSONDeserializer.Deserialize<bool>(json);

        // Assert
        Assert.That(result, Is.True);
    }

    /// <summary>
    /// Tests that deserializing a complex object works correctly.
    /// </summary>
    [Test]
    public void Deserialize_ComplexObject_ShouldReturnObject()
    {
        // Arrange
        var json = """
        {
            "name": "John Doe",
            "age": 30,
            "isActive": true,
            "salary": 75000.50
        }
        """;

        // Act
        var result = JSONDeserializer.Deserialize<object>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Dictionary<string, object>>());
    }

    /// <summary>
    /// Tests that deserializing an array works correctly.
    /// </summary>
    [Test]
    public void Deserialize_Array_ShouldReturnArray()
    {
        // Arrange
        var json = "[1, 2, 3, 4, 5]";

        // Act
        var result = JSONDeserializer.Deserialize<object>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<object[]>());
    }

    /// <summary>
    /// Tests that deserializing null or empty string returns default value.
    /// </summary>
    [Test]
    public void Deserialize_NullOrEmptyString_ShouldReturnDefault()
    {
        // Act & Assert
        Assert.That(JSONDeserializer.Deserialize<string>((string)null!), Is.Null);
        Assert.That(JSONDeserializer.Deserialize<string>(""), Is.Null);
        Assert.That(JSONDeserializer.Deserialize<string>("   "), Is.Null);
        Assert.That(JSONDeserializer.Deserialize<int>((string)null!), Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that deserializing JSON with comments works correctly.
    /// </summary>
    [Test]
    public void Deserialize_JsonWithComments_ShouldWork()
    {
        // Arrange
        var json = """
        {
            // This is a comment
            "name": "John Doe",
            "age": 30 /* Another comment */
        }
        """;

        // Act
        var result = JSONDeserializer.Deserialize<object>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that deserializing JSON with trailing commas works correctly.
    /// </summary>
    [Test]
    public void Deserialize_JsonWithTrailingCommas_ShouldWork()
    {
        // Arrange
        var json = """
        {
            "name": "John Doe",
            "age": 30,
        }
        """;

        // Act
        var result = JSONDeserializer.Deserialize<object>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that deserializing with case-insensitive property names works correctly.
    /// </summary>
    [Test]
    public void Deserialize_CaseInsensitiveProperties_ShouldWork()
    {
        // Arrange
        var json = """
        {
            "Name": "John Doe",
            "AGE": 30
        }
        """;

        // Act
        var result = JSONDeserializer.Deserialize<object>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    #endregion

    #region Deserialize Stream Tests

    /// <summary>
    /// Tests that deserializing from a stream works correctly.
    /// </summary>
    [Test]
    public async Task Deserialize_FromStream_ShouldWork()
    {
        // Arrange
        var json = """
        {
            "name": "John Doe",
            "age": 30
        }
        """;
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        // Act
        var result = await JSONDeserializer.Deserialize<object>(stream);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that deserializing an empty stream throws an exception.
    /// </summary>
    [Test]
    public async Task Deserialize_EmptyStream_ShouldThrowException()
    {
        // Arrange
        using var stream = new MemoryStream();

        // Act & Assert
        Assert.ThrowsAsync<JsonException>(async () => await JSONDeserializer.Deserialize<string>(stream));
    }

    #endregion

    #region JsonToNormal Tests

    /// <summary>
    /// Tests that JsonToNormal converts a string JsonElement correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_String_ShouldReturnString()
    {
        // Arrange
        var json = JsonDocument.Parse("\"Hello World\"").RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(json);

        // Assert
        Assert.That(result, Is.EqualTo("Hello World"));
    }

    /// <summary>
    /// Tests that JsonToNormal converts boolean strings correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_BooleanStrings_ShouldReturnBooleans()
    {
        // Arrange
        var trueJson = JsonDocument.Parse("\"True\"").RootElement;
        var falseJson = JsonDocument.Parse("\"False\"").RootElement;

        // Act
        var trueResult = JSONDeserializer.JsonToNormal(trueJson);
        var falseResult = JSONDeserializer.JsonToNormal(falseJson);

        // Assert
        Assert.That(trueResult, Is.True);
        Assert.That(falseResult, Is.False);
    }

    /// <summary>
    /// Tests that JsonToNormal converts regular boolean values correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_BooleanValues_ShouldReturnBooleans()
    {
        // Arrange
        var trueJson = JsonDocument.Parse("true").RootElement;
        var falseJson = JsonDocument.Parse("false").RootElement;

        // Act
        var trueResult = JSONDeserializer.JsonToNormal(trueJson);
        var falseResult = JSONDeserializer.JsonToNormal(falseJson);

        // Assert
        Assert.That(trueResult, Is.True);
        Assert.That(falseResult, Is.False);
    }

    /// <summary>
    /// Tests that JsonToNormal converts numbers correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_Number_ShouldReturnDouble()
    {
        // Arrange
        var json = JsonDocument.Parse("3.14159").RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(json);

        // Assert
        Assert.That(result, Is.EqualTo(3.14159));
    }


    /// <summary>
    /// Tests that JsonToNormal converts objects correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_Object_ShouldReturnDictionary()
    {
        // Arrange
        var json = JsonDocument.Parse("""
        {
            "name": "John",
            "age": 30
        }
        """).RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(json);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<string, object>>());
        var dict = (Dictionary<string, object>)result!;
        Assert.That(dict["name"], Is.EqualTo("John"));
        Assert.That(dict["age"], Is.EqualTo(30.0));
    }

    /// <summary>
    /// Tests that JsonToNormal handles null values correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_Null_ShouldReturnNull()
    {
        // Arrange
        var json = JsonDocument.Parse("null").RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(json);

        // Assert
        Assert.That(result, Is.Null);
    }

    #endregion

    #region ToDict Tests

    /// <summary>
    /// Tests that ToDict converts a JsonElement object correctly.
    /// </summary>
    [Test]
    public void ToDict_JsonElement_ShouldReturnDictionary()
    {
        // Arrange
        var json = JsonDocument.Parse("""
        {
            "name": "John",
            "age": 30,
            "isActive": true
        }
        """).RootElement;

        // Act
        var result = JSONDeserializer.ToDict(json);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<string, object>>());
        Assert.That(result["name"], Is.EqualTo("John"));
        Assert.That(result["age"], Is.EqualTo(30.0));
        Assert.That(result["isActive"], Is.True);
    }

    /// <summary>
    /// Tests that ToDict converts a dictionary with JsonElement values correctly.
    /// </summary>
    [Test]
    public void ToDict_DictionaryWithJsonElements_ShouldReturnConvertedDictionary()
    {
        // Arrange
        var json = JsonDocument.Parse("""
        {
            "name": "John",
            "age": 30
        }
        """).RootElement;
        var dict = new Dictionary<string, object>
        {
            ["person"] = json
        };

        // Act
        var result = JSONDeserializer.ToDict(dict);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<string, object>>());
        Assert.That(result.ContainsKey("person"));
    }

    /// <summary>
    /// Tests that ToDict handles empty objects correctly.
    /// </summary>
    [Test]
    public void ToDict_EmptyObject_ShouldReturnEmptyDictionary()
    {
        // Arrange
        var json = JsonDocument.Parse("{}").RootElement;

        // Act
        var result = JSONDeserializer.ToDict(json);

        // Assert
        Assert.That(result, Is.InstanceOf<Dictionary<string, object>>());
        Assert.That(result.Count, Is.EqualTo(0));
    }

    #endregion

    #region ToArray Tests

    /// <summary>
    /// Tests that JsonToNormal converts arrays correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_Array_ShouldReturnObjectArray()
    {
        // Arrange
        var json = JsonDocument.Parse("[1, \"hello\", true]").RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(json);

        // Assert
        Assert.That(result, Is.InstanceOf<object[]>());
        var array = (object[])result!;
        Assert.That(array.Length, Is.EqualTo(3));
        Assert.That(array[0], Is.EqualTo(1.0));
        Assert.That(array[1], Is.EqualTo("hello"));
        Assert.That(array[2], Is.True);
    }

    /// <summary>
    /// Tests that JsonToNormal handles empty arrays correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_EmptyArray_ShouldReturnEmptyArray()
    {
        // Arrange
        var json = JsonDocument.Parse("[]").RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(json);

        // Assert
        Assert.That(result, Is.InstanceOf<object[]>());
        var array = (object[])result!;
        Assert.That(array.Length, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that JsonToNormal handles nested arrays correctly.
    /// </summary>
    [Test]
    public void JsonToNormal_NestedArrays_ShouldReturnNestedArrays()
    {
        // Arrange
        var json = JsonDocument.Parse("[[1, 2], [3, 4]]").RootElement;

        // Act
        var result = JSONDeserializer.JsonToNormal(json);

        // Assert
        Assert.That(result, Is.InstanceOf<object[]>());
        var array = (object[])result!;
        Assert.That(array.Length, Is.EqualTo(2));
        Assert.That(array[0], Is.InstanceOf<object[]>());
        Assert.That(array[1], Is.InstanceOf<object[]>());
    }

    #endregion

    #region Edge Cases and Error Handling

    /// <summary>
    /// Tests that malformed JSON throws appropriate exceptions.
    /// </summary>
    [Test]
    public void Deserialize_MalformedJson_ShouldThrowException()
    {
        // Arrange
        var malformedJson = "{ \"name\": \"John\", \"age\": }";

        // Act & Assert
        Assert.Throws<JsonException>(() => JSONDeserializer.Deserialize<object>(malformedJson));
    }

    /// <summary>
    /// Tests that very large numbers are handled correctly.
    /// </summary>
    [Test]
    public void Deserialize_LargeNumber_ShouldWork()
    {
        // Arrange
        var json = "9223372036854775807"; // Max long value

        // Act
        var result = JSONDeserializer.Deserialize<object>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that nested objects are handled correctly.
    /// </summary>
    [Test]
    public void Deserialize_NestedObjects_ShouldWork()
    {
        // Arrange
        var json = """
        {
            "person": {
                "name": "John",
                "address": {
                    "street": "123 Main St",
                    "city": "Anytown"
                }
            }
        }
        """;

        // Act
        var result = JSONDeserializer.Deserialize<object>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that arrays with mixed types are handled correctly.
    /// </summary>
    [Test]
    public void Deserialize_MixedTypeArray_ShouldWork()
    {
        // Arrange
        var json = "[1, \"hello\", true, null, {\"key\": \"value\"}]";

        // Act
        var result = JSONDeserializer.Deserialize<object>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that Unicode characters are handled correctly.
    /// </summary>
    [Test]
    public void Deserialize_UnicodeString_ShouldWork()
    {
        // Arrange
        var json = "\"Hello 世界 🌍\"";

        // Act
        var result = JSONDeserializer.Deserialize<string>(json);

        // Assert
        Assert.That(result, Is.EqualTo("Hello 世界 🌍"));
    }

    #endregion

    #region JSONOptions Tests

    /// <summary>
    /// Tests that the JSONOptions are configured correctly.
    /// </summary>
    [Test]
    public void JSONOptions_ShouldBeConfiguredCorrectly()
    {
        // Act
        var options = JSONDeserializer.JSONOptions;

        // Assert
        Assert.That(options.ReadCommentHandling, Is.EqualTo(JsonCommentHandling.Skip));
        Assert.That(options.AllowTrailingCommas, Is.True);
        Assert.That(options.PropertyNameCaseInsensitive, Is.True);
        Assert.That(options.WriteIndented, Is.True);
        Assert.That(options.DefaultIgnoreCondition, Is.EqualTo(JsonIgnoreCondition.WhenWritingDefault));
        Assert.That(options.Converters.Count, Is.EqualTo(1));
        Assert.That(options.Converters[0], Is.InstanceOf<JSONDeserializer>());
    }

    #endregion
}