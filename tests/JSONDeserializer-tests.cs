using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;

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
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
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
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        var dict = (Dictionary<CasePreservingString, object>)result!;
        Assert.That(dict[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(dict[new CasePreservingString("age")], Is.EqualTo(30.0));
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
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        Assert.That(result[new CasePreservingString("name")], Is.EqualTo("John"));
        Assert.That(result[new CasePreservingString("age")], Is.EqualTo(30.0));
        Assert.That(result[new CasePreservingString("isActive")], Is.True);
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
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
        Assert.That(result.ContainsKey(new CasePreservingString("person")));
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
        Assert.That(result, Is.InstanceOf<Dictionary<CasePreservingString, object>>());
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
        Assert.That(options.Converters.Count, Is.EqualTo(9));
        Assert.That(options.Converters[0], Is.InstanceOf<JSONDeserializer>());
    }

    #endregion

    #region DefaultValue Attribute Tests

    /// <summary>
    /// Custom attribute for testing the SerializeProperties method with DefaultValue attributes.
    /// </summary>
    public class TestSerializeAttribute : Attribute
    {
        public string Description { get; set; } = "";
    }

    /// <summary>
    /// Test class with properties and fields that have DefaultValue attributes.
    /// </summary>
    public class TestClassWithDefaultValues
    {
        [TestSerialize(Description = "Default int property")]
        [DefaultValue(42)]
        public int DefaultIntProperty { get; set; }

        [TestSerialize(Description = "Default string property")]
        [DefaultValue("default")]
        public string DefaultStringProperty { get; set; } = string.Empty;

        [TestSerialize(Description = "Default bool property")]
        [DefaultValue(true)]
        public bool DefaultBoolProperty { get; set; }

        [TestSerialize(Description = "Default double property")]
        [DefaultValue(3.14)]
        public double DefaultDoubleProperty { get; set; }

        [TestSerialize(Description = "Default int field")]
        [DefaultValue(42)]
        public int DefaultIntField;

        [TestSerialize(Description = "Default string field")]
        [DefaultValue("default")]
        public string DefaultStringField = string.Empty;

        [TestSerialize(Description = "Default bool field")]
        [DefaultValue(true)]
        public bool DefaultBoolField;

        [TestSerialize(Description = "Default double field")]
        [DefaultValue(3.14)]
        public double DefaultDoubleField;

        // Properties without DefaultValue attributes
        [TestSerialize(Description = "Non-default int property")]
        public int NonDefaultIntProperty { get; set; }

        [TestSerialize(Description = "Non-default string property")]
        public string NonDefaultStringProperty { get; set; } = string.Empty;
    }
    #endregion

    #region Object Serialization with DefaultValue Tests

    /// <summary>
    /// Test class for verifying object serialization with DefaultValue attributes.
    /// </summary>
    public class TestObjectWithDefaultValues
    {
        [DefaultValue(42)]
        public int DefaultIntProperty { get; set; }

        [DefaultValue("default")]
        public string DefaultStringProperty { get; set; } = string.Empty;

        [DefaultValue(true)]
        public bool DefaultBoolProperty { get; set; }

        [DefaultValue(3.14)]
        public double DefaultDoubleProperty { get; set; }

        // Properties without DefaultValue attributes
        public int NonDefaultIntProperty { get; set; }
        public string NonDefaultStringProperty { get; set; } = string.Empty;
    }

    /// <summary>
    /// Tests that JsonSerializer.Serialize skips properties with DefaultValue attributes when their value matches the default.
    /// </summary>
    [Test]
    public void JsonSerializer_Serialize_WithDefaultValueAttributes_MatchingDefault_ShouldSkipProperties()
    {
        // Arrange
        var testObject = new TestObjectWithDefaultValues
        {
            DefaultIntProperty = 42,        // Matches default
            DefaultStringProperty = "default", // Matches default
            DefaultBoolProperty = true,     // Matches default
            DefaultDoubleProperty = 3.14,   // Matches default
            NonDefaultIntProperty = 100,    // No default value attribute
            NonDefaultStringProperty = "test" // No default value attribute
        };

        // Act
        var json = JsonSerializer.Serialize(testObject, JSONDeserializer.JSONOptions);

        // Assert
        Assert.That(json, Is.Not.Null);

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        Assert.That(root.ValueKind, Is.EqualTo(JsonValueKind.Object));

        // Properties with DefaultValue attributes should be skipped when value matches default
        Assert.That(root.TryGetProperty("DefaultIntProperty", out _), Is.False, "DefaultIntProperty should be skipped when value matches default");
        Assert.That(root.TryGetProperty("DefaultStringProperty", out _), Is.False, "DefaultStringProperty should be skipped when value matches default");
        Assert.That(root.TryGetProperty("DefaultBoolProperty", out _), Is.False, "DefaultBoolProperty should be skipped when value matches default");
        Assert.That(root.TryGetProperty("DefaultDoubleProperty", out _), Is.False, "DefaultDoubleProperty should be skipped when value matches default");

        // Properties without DefaultValue attributes should be included
        Assert.That(root.TryGetProperty("NonDefaultIntProperty", out var nonDefInt), Is.True, "NonDefaultIntProperty should be included (no default value attribute)");
        Assert.That(root.TryGetProperty("NonDefaultStringProperty", out var nonDefStr), Is.True, "NonDefaultStringProperty should be included (no default value attribute)");

        // Verify the actual values are present
        Assert.That(nonDefInt.GetInt32(), Is.EqualTo(100), "NonDefaultIntProperty value should be present");
        Assert.That(nonDefStr.GetString(), Is.EqualTo("test"), "NonDefaultStringProperty value should be present");
    }

    /// <summary>
    /// Tests that JsonSerializer.Serialize includes properties with DefaultValue attributes when their value differs from the default.
    /// </summary>
    [Test]
    public void JsonSerializer_Serialize_WithDefaultValueAttributes_DifferentFromDefault_ShouldIncludeProperties()
    {
        // Arrange
        var testObject = new TestObjectWithDefaultValues
        {
            DefaultIntProperty = 100,       // Different from default (42)
            DefaultStringProperty = "custom", // Different from default ("default")
            DefaultBoolProperty = false,    // Different from default (true)
            DefaultDoubleProperty = 2.71,   // Different from default (3.14)
            NonDefaultIntProperty = 200,
            NonDefaultStringProperty = "test"
        };

        // Act
        var json = JsonSerializer.Serialize(testObject, JSONDeserializer.JSONOptions);

        // Assert
        Assert.That(json, Is.Not.Null);

        // Properties with DefaultValue attributes should be included when value differs from default
        Assert.That(json, Does.Contain("DefaultIntProperty"), "DefaultIntProperty should be included when value differs from default");
        Assert.That(json, Does.Contain("DefaultStringProperty"), "DefaultStringProperty should be included when value differs from default");
        Assert.That(json, Does.Contain("DefaultBoolProperty"), "DefaultBoolProperty should be included when value differs from default");
        Assert.That(json, Does.Contain("DefaultDoubleProperty"), "DefaultDoubleProperty should be included when value differs from default");

        // Properties without DefaultValue attributes should be included
        Assert.That(json, Does.Contain("NonDefaultIntProperty"), "NonDefaultIntProperty should be included");
        Assert.That(json, Does.Contain("NonDefaultStringProperty"), "NonDefaultStringProperty should be included");

        // Verify the actual values are present
        Assert.That(json, Does.Contain("100"), "DefaultIntProperty value should be present");
        Assert.That(json, Does.Contain("\"custom\""), "DefaultStringProperty value should be present");
        Assert.That(json, Does.Contain("false"), "DefaultBoolProperty value should be present");
        Assert.That(json, Does.Contain("2.71"), "DefaultDoubleProperty value should be present");
        Assert.That(json, Does.Contain("200"), "NonDefaultIntProperty value should be present");
        Assert.That(json, Does.Contain("\"test\""), "NonDefaultStringProperty value should be present");
    }

    /// <summary>
    /// Tests that JsonSerializer.Serialize includes all properties when DefaultIgnoreCondition is not WhenWritingDefault.
    /// </summary>
    [Test]
    public void JsonSerializer_Serialize_WithDefaultValueAttributes_OtherIgnoreCondition_ShouldIncludeAllProperties()
    {
        // Arrange
        var testObject = new TestObjectWithDefaultValues
        {
            DefaultIntProperty = 42,        // Matches default
            DefaultStringProperty = "default", // Matches default
            DefaultBoolProperty = true,     // Matches default
            DefaultDoubleProperty = 3.14,   // Matches default
            NonDefaultIntProperty = 100,
            NonDefaultStringProperty = "test"
        };

        // Create options with different DefaultIgnoreCondition
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        // Act
        var json = JsonSerializer.Serialize(testObject, options);

        // Assert
        Assert.That(json, Is.Not.Null);

        // All properties should be included when DefaultIgnoreCondition is not WhenWritingDefault
        Assert.That(json, Does.Contain("DefaultIntProperty"), "DefaultIntProperty should be included when DefaultIgnoreCondition is not WhenWritingDefault");
        Assert.That(json, Does.Contain("DefaultStringProperty"), "DefaultStringProperty should be included when DefaultIgnoreCondition is not WhenWritingDefault");
        Assert.That(json, Does.Contain("DefaultBoolProperty"), "DefaultBoolProperty should be included when DefaultIgnoreCondition is not WhenWritingDefault");
        Assert.That(json, Does.Contain("DefaultDoubleProperty"), "DefaultDoubleProperty should be included when DefaultIgnoreCondition is not WhenWritingDefault");
        Assert.That(json, Does.Contain("NonDefaultIntProperty"), "NonDefaultIntProperty should be included");
        Assert.That(json, Does.Contain("NonDefaultStringProperty"), "NonDefaultStringProperty should be included");
    }

    #endregion
}