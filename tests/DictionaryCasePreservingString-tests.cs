using System.Text.Json;
using Blackwood;

namespace Blackwood.System.Text.Json.tests;

/// <summary>
/// Test suite for Dictionary&lt;CasePreservingString, object&gt; serialization and deserialization.
/// Tests cover JSON serialization/deserialization, case-insensitive key behavior,
/// nested dictionaries, round-trip operations, and various edge cases.
/// </summary>
[TestFixture]
public class DictionaryCasePreservingStringTests
{
    #region Basic Dictionary Serialization Tests

    /// <summary>
    /// Tests that a simple dictionary with CasePreservingString keys serializes correctly.
    /// </summary>
    [Test]
    public void Serialize_SimpleDictionary_ShouldSerializeCorrectly()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe",
            ["Age"] = 30,
            ["IsActive"] = true
        };

        // Act
        var json = JsonSerializer.Serialize(dict, JSONDeserializer.JSONOptions);

        // Assert
        Assert.That(json, Is.Not.Null);
        Assert.That(json, Does.Contain("\"Name\""));
        Assert.That(json, Does.Contain("\"Age\""));
        Assert.That(json, Does.Contain("\"IsActive\""));
        Assert.That(json, Does.Contain("John Doe"));
        Assert.That(json, Does.Contain("30"));
        Assert.That(json, Does.Contain("true"));
    }

    /// <summary>
    /// Tests that a dictionary with CasePreservingString keys deserializes correctly.
    /// </summary>
    [Test]
    public void Deserialize_SimpleDictionary_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = """{"Name": "John Doe", "Age": 30, "IsActive": true}""";

        // Act
        var result = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[new CasePreservingString("Name")], Is.EqualTo("John Doe"));
        Assert.That(result[new CasePreservingString("Age")], Is.EqualTo(30));
        Assert.That(result[new CasePreservingString("IsActive")], Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that case-insensitive key lookup works correctly in deserialized dictionary.
    /// </summary>
    [Test]
    public void Deserialize_Dictionary_CaseInsensitiveKeyLookup()
    {
        // Arrange
        var json = """{"Name": "John Doe", "Age": 30}""";

        // Act
        var result = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(result[new CasePreservingString("name")], Is.EqualTo("John Doe"));
        Assert.That(result[new CasePreservingString("NAME")], Is.EqualTo("John Doe"));
        Assert.That(result[new CasePreservingString("age")], Is.EqualTo(30));
        Assert.That(result[new CasePreservingString("AGE")], Is.EqualTo(30));
    }

    #endregion

    #region Nested Dictionary Tests

    /// <summary>
    /// Tests serialization of nested dictionaries with CasePreservingString keys.
    /// </summary>
    [Test]
    public void Serialize_NestedDictionary_ShouldSerializeCorrectly()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["User"] = new Dictionary<CasePreservingString, object>
            {
                ["Name"] = "John Doe",
                ["Profile"] = new Dictionary<CasePreservingString, object>
                {
                    ["Theme"] = "Dark",
                    ["Language"] = "en-US"
                }
            },
            ["Settings"] = new Dictionary<CasePreservingString, object>
            {
                ["Notifications"] = true,
                ["AutoSave"] = false
            }
        };

        // Act
        var json = JsonSerializer.Serialize(dict, JSONDeserializer.JSONOptions);

        // Assert
        Assert.That(json, Is.Not.Null);
        Assert.That(json, Does.Contain("\"User\""));
        Assert.That(json, Does.Contain("\"Profile\""));
        Assert.That(json, Does.Contain("\"Settings\""));
    }

    /// <summary>
    /// Tests deserialization of nested dictionaries with CasePreservingString keys.
    /// </summary>
    [Test]
    public void Deserialize_NestedDictionary_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = """
        {
            "User": {
                "Name": "John Doe",
                "Profile": {
                    "Theme": "Dark",
                    "Language": "en-US"
                }
            },
            "Settings": {
                "Notifications": true,
                "AutoSave": false
            }
        }
        """;

        // Act
        var result = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result[new CasePreservingString("User")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var user = (Dictionary<CasePreservingString, object>)result[new CasePreservingString("User")];
        Assert.That(user[new CasePreservingString("Name")], Is.EqualTo("John Doe"));
        Assert.That(user[new CasePreservingString("Profile")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var profile = (Dictionary<CasePreservingString, object>)user[new CasePreservingString("Profile")];
        Assert.That(profile[new CasePreservingString("Theme")], Is.EqualTo("Dark"));
        Assert.That(profile[new CasePreservingString("Language")], Is.EqualTo("en-US"));
    }

    /// <summary>
    /// Tests case-insensitive key lookup in nested dictionaries.
    /// </summary>
    [Test]
    public void Deserialize_NestedDictionary_CaseInsensitiveLookup()
    {
        // Arrange
        var json = """{"User": {"Name": "John Doe", "Age": 30}}""";

        // Act
        var result = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        var user = (Dictionary<CasePreservingString, object>)result[new CasePreservingString("user")];
        Assert.That(user[new CasePreservingString("name")], Is.EqualTo("John Doe"));
        Assert.That(user[new CasePreservingString("age")], Is.EqualTo(30));
    }

    #endregion

    #region Round-Trip Tests

    /// <summary>
    /// Tests that a dictionary survives round-trip serialization/deserialization.
    /// </summary>
    [Test]
    public void RoundTrip_SimpleDictionary_PreservesData()
    {
        // Arrange
        var originalDict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe",
            ["Age"] = 30,
            ["IsActive"] = true,
            ["Score"] = 95.5
        };

        // Act
        var json = JsonSerializer.Serialize(originalDict, JSONDeserializer.JSONOptions);
        var deserializedDict = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(deserializedDict, Is.Not.Null);
        Assert.That(deserializedDict.Count, Is.EqualTo(originalDict.Count));
        Assert.That(deserializedDict[new CasePreservingString("Name")], Is.EqualTo(originalDict[new CasePreservingString("Name")]));
        Assert.That(deserializedDict[new CasePreservingString("Age")], Is.EqualTo(originalDict[new CasePreservingString("Age")]));
        Assert.That(deserializedDict[new CasePreservingString("IsActive")], Is.EqualTo(originalDict[new CasePreservingString("IsActive")]));
        Assert.That(deserializedDict[new CasePreservingString("Score")], Is.EqualTo(originalDict[new CasePreservingString("Score")]));
    }

    /// <summary>
    /// Tests that nested dictionaries survive round-trip serialization/deserialization.
    /// </summary>
    [Test]
    public void RoundTrip_NestedDictionary_PreservesData()
    {
        // Arrange
        var originalDict = new Dictionary<CasePreservingString, object>
        {
            ["User"] = new Dictionary<CasePreservingString, object>
            {
                ["Name"] = "John Doe",
                ["Profile"] = new Dictionary<CasePreservingString, object>
                {
                    ["Theme"] = "Dark",
                    ["Language"] = "en-US"
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(originalDict, JSONDeserializer.JSONOptions);
        var deserializedDict = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(deserializedDict, Is.Not.Null);
        Assert.That(deserializedDict[new CasePreservingString("User")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var user = (Dictionary<CasePreservingString, object>)deserializedDict[new CasePreservingString("User")];
        Assert.That(user[new CasePreservingString("Name")], Is.EqualTo("John Doe"));
        Assert.That(user[new CasePreservingString("Profile")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var profile = (Dictionary<CasePreservingString, object>)user[new CasePreservingString("Profile")];
        Assert.That(profile[new CasePreservingString("Theme")], Is.EqualTo("Dark"));
        Assert.That(profile[new CasePreservingString("Language")], Is.EqualTo("en-US"));
    }

    /// <summary>
    /// Tests that case-insensitive key behavior is preserved through round-trip.
    /// </summary>
    [Test]
    public void RoundTrip_Dictionary_CaseInsensitiveKeysPreserved()
    {
        // Arrange
        var originalDict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe",
            ["Age"] = 30
        };

        // Act
        var json = JsonSerializer.Serialize(originalDict, JSONDeserializer.JSONOptions);
        var deserializedDict = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert - Test case-insensitive access
        Assert.That(deserializedDict[new CasePreservingString("name")], Is.EqualTo("John Doe"));
        Assert.That(deserializedDict[new CasePreservingString("NAME")], Is.EqualTo("John Doe"));
        Assert.That(deserializedDict[new CasePreservingString("age")], Is.EqualTo(30));
        Assert.That(deserializedDict[new CasePreservingString("AGE")], Is.EqualTo(30));
    }

    #endregion

    #region Edge Cases and Special Values

    /// <summary>
    /// Tests serialization of an empty dictionary.
    /// </summary>
    [Test]
    public void Serialize_EmptyDictionary_ShouldSerializeCorrectly()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>();

        // Act
        var json = JsonSerializer.Serialize(dict, JSONDeserializer.JSONOptions);

        // Assert
        Assert.That(json, Is.EqualTo("{}"));
    }

    /// <summary>
    /// Tests deserialization of an empty dictionary.
    /// </summary>
    [Test]
    public void Deserialize_EmptyDictionary_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = "{}";

        // Act
        var result = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests serialization of dictionary with null values.
    /// </summary>
    [Test]
    public void Serialize_DictionaryWithNullValues_ShouldSerializeCorrectly()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe",
            ["MiddleName"] = null,
            ["Age"] = 30
        };

        // Act
        var json = JsonSerializer.Serialize(dict, JSONDeserializer.JSONOptions);

        // Assert
        Assert.That(json, Is.Not.Null);
        Assert.That(json, Does.Contain("\"Name\""));
        Assert.That(json, Does.Contain("\"MiddleName\""));
        Assert.That(json, Does.Contain("\"Age\""));
        Assert.That(json, Does.Contain("null"));
    }

    /// <summary>
    /// Tests deserialization of dictionary with null values.
    /// </summary>
    [Test]
    public void Deserialize_DictionaryWithNullValues_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = """{"Name": "John Doe", "MiddleName": null, "Age": 30}""";

        // Act
        var result = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[new CasePreservingString("Name")], Is.EqualTo("John Doe"));
        Assert.That(result[new CasePreservingString("MiddleName")], Is.Null);
        Assert.That(result[new CasePreservingString("Age")], Is.EqualTo(30));
    }

    /// <summary>
    /// Tests serialization of dictionary with various data types.
    /// </summary>
    [Test]
    public void Serialize_DictionaryWithVariousTypes_ShouldSerializeCorrectly()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["String"] = "Hello",
            ["Int"] = 42,
            ["Double"] = 3.14,
            ["Bool"] = true,
            ["Array"] = new[] { 1, 2, 3 },
            ["NestedDict"] = new Dictionary<CasePreservingString, object> { ["Key"] = "Value" }
        };

        // Act
        var json = JsonSerializer.Serialize(dict, JSONDeserializer.JSONOptions);

        // Assert
        Assert.That(json, Is.Not.Null);
        Assert.That(json, Does.Contain("\"String\""));
        Assert.That(json, Does.Contain("\"Int\""));
        Assert.That(json, Does.Contain("\"Double\""));
        Assert.That(json, Does.Contain("\"Bool\""));
        Assert.That(json, Does.Contain("\"Array\""));
        Assert.That(json, Does.Contain("\"NestedDict\""));
    }

    /// <summary>
    /// Tests deserialization of dictionary with various data types.
    /// </summary>
    [Test]
    public void Deserialize_DictionaryWithVariousTypes_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = """
        {
            "String": "Hello",
            "Int": 42,
            "Double": 3.14,
            "Bool": true,
            "Array": [1, 2, 3],
            "NestedDict": {"Key": "Value"}
        }
        """;

        // Act
        var result = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(6));
        Assert.That(result[new CasePreservingString("String")], Is.EqualTo("Hello"));
        Assert.That(result[new CasePreservingString("Int")], Is.EqualTo(42));
        Assert.That(result[new CasePreservingString("Double")], Is.EqualTo(3.14));
        Assert.That(result[new CasePreservingString("Bool")], Is.EqualTo(true));
        Assert.That(result[new CasePreservingString("Array")], Is.InstanceOf<object[]>());
        Assert.That(result[new CasePreservingString("NestedDict")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());
    }

    #endregion

    #region Case Sensitivity Tests

    /// <summary>
    /// Tests that keys with different cases are treated as the same key.
    /// </summary>
    [Test]
    public void Dictionary_CaseInsensitiveKeys_ShouldTreatAsSame()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe"
        };

        // Act & Assert
        Assert.That(dict.ContainsKey(new CasePreservingString("name")), Is.True);
        Assert.That(dict.ContainsKey(new CasePreservingString("NAME")), Is.True);
        Assert.That(dict.ContainsKey(new CasePreservingString("Name")), Is.True);
        Assert.That(dict.ContainsKey(new CasePreservingString("nAmE")), Is.True);
    }

    /// <summary>
    /// Tests that adding a key with different case overwrites the existing key.
    /// </summary>
    [Test]
    public void Dictionary_CaseInsensitiveKeys_ShouldOverwriteExisting()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe"
        };

        // Act
        dict[new CasePreservingString("NAME")] = "Jane Smith";

        // Assert
        Assert.That(dict.Count, Is.EqualTo(1));
        Assert.That(dict[new CasePreservingString("Name")], Is.EqualTo("Jane Smith"));
        Assert.That(dict[new CasePreservingString("name")], Is.EqualTo("Jane Smith"));
        Assert.That(dict[new CasePreservingString("NAME")], Is.EqualTo("Jane Smith"));
    }

    /// <summary>
    /// Tests that case-insensitive behavior is preserved through serialization.
    /// </summary>
    [Test]
    public void RoundTrip_CaseInsensitiveKeys_PreservesBehavior()
    {
        // Arrange
        var originalDict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe",
            ["Age"] = 30
        };

        // Act
        var json = JsonSerializer.Serialize(originalDict, JSONDeserializer.JSONOptions);
        var deserializedDict = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(deserializedDict.ContainsKey(new CasePreservingString("name")), Is.True);
        Assert.That(deserializedDict.ContainsKey(new CasePreservingString("NAME")), Is.True);
        Assert.That(deserializedDict.ContainsKey(new CasePreservingString("age")), Is.True);
        Assert.That(deserializedDict.ContainsKey(new CasePreservingString("AGE")), Is.True);
    }

    #endregion

    #region TryGetValue Tests

    /// <summary>
    /// Tests that TryGetValue works correctly with case-insensitive keys.
    /// </summary>
    [Test]
    public void TryGetValue_CaseInsensitiveKeys_ShouldFindValues()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe",
            ["Age"] = 30,
            ["IsActive"] = true
        };

        // Act & Assert
        Assert.That(dict.TryGetValue(new CasePreservingString("name"), out var nameValue), Is.True);
        Assert.That(nameValue, Is.EqualTo("John Doe"));

        Assert.That(dict.TryGetValue(new CasePreservingString("NAME"), out var nameValue2), Is.True);
        Assert.That(nameValue2, Is.EqualTo("John Doe"));

        Assert.That(dict.TryGetValue(new CasePreservingString("age"), out var ageValue), Is.True);
        Assert.That(ageValue, Is.EqualTo(30));

        Assert.That(dict.TryGetValue(new CasePreservingString("AGE"), out var ageValue2), Is.True);
        Assert.That(ageValue2, Is.EqualTo(30));

        Assert.That(dict.TryGetValue(new CasePreservingString("isactive"), out var isActiveValue), Is.True);
        Assert.That(isActiveValue, Is.EqualTo(true));

        Assert.That(dict.TryGetValue(new CasePreservingString("ISACTIVE"), out var isActiveValue2), Is.True);
        Assert.That(isActiveValue2, Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that TryGetValue returns false for non-existent keys.
    /// </summary>
    [Test]
    public void TryGetValue_NonExistentKey_ShouldReturnFalse()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe",
            ["Age"] = 30
        };

        // Act & Assert
        Assert.That(dict.TryGetValue(new CasePreservingString("NonExistent"), out var value), Is.False);
        Assert.That(value, Is.Null);

        Assert.That(dict.TryGetValue(new CasePreservingString("Email"), out var emailValue), Is.False);
        Assert.That(emailValue, Is.Null);
    }

    /// <summary>
    /// Tests that TryGetValue works correctly after deserialization.
    /// </summary>
    [Test]
    public void TryGetValue_AfterDeserialization_ShouldWorkCorrectly()
    {
        // Arrange
        var json = """{"Name": "John Doe", "Age": 30, "IsActive": true}""";

        // Act
        var dict = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(dict.TryGetValue(new CasePreservingString("name"), out var nameValue), Is.True);
        Assert.That(nameValue, Is.EqualTo("John Doe"));

        Assert.That(dict.TryGetValue(new CasePreservingString("NAME"), out var nameValue2), Is.True);
        Assert.That(nameValue2, Is.EqualTo("John Doe"));

        Assert.That(dict.TryGetValue(new CasePreservingString("age"), out var ageValue), Is.True);
        Assert.That(ageValue, Is.EqualTo(30));

        Assert.That(dict.TryGetValue(new CasePreservingString("isactive"), out var isActiveValue), Is.True);
        Assert.That(isActiveValue, Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that TryGetValue works correctly with nested dictionaries.
    /// </summary>
    [Test]
    public void TryGetValue_NestedDictionary_ShouldWorkCorrectly()
    {
        // Arrange
        var json = """
        {
            "User": {
                "Name": "John Doe",
                "Profile": {
                    "Theme": "Dark",
                    "Language": "en-US"
                }
            },
            "Settings": {
                "Notifications": true,
                "AutoSave": false
            }
        }
        """;

        // Act
        var dict = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(dict.TryGetValue(new CasePreservingString("user"), out var userValue), Is.True);
        Assert.That(userValue, Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var user = (Dictionary<CasePreservingString, object>)userValue!;
        Assert.That(user.TryGetValue(new CasePreservingString("name"), out var nameValue), Is.True);
        Assert.That(nameValue, Is.EqualTo("John Doe"));

        Assert.That(user.TryGetValue(new CasePreservingString("profile"), out var profileValue), Is.True);
        Assert.That(profileValue, Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var profile = (Dictionary<CasePreservingString, object>)profileValue!;
        Assert.That(profile.TryGetValue(new CasePreservingString("theme"), out var themeValue), Is.True);
        Assert.That(themeValue, Is.EqualTo("Dark"));

        Assert.That(profile.TryGetValue(new CasePreservingString("language"), out var languageValue), Is.True);
        Assert.That(languageValue, Is.EqualTo("en-US"));
    }

    /// <summary>
    /// Tests that TryGetValue works correctly with round-trip serialization.
    /// </summary>
    [Test]
    public void TryGetValue_RoundTripSerialization_ShouldPreserveBehavior()
    {
        // Arrange
        var originalDict = new Dictionary<CasePreservingString, object>
        {
            ["Name"] = "John Doe",
            ["Age"] = 30,
            ["IsActive"] = true
        };

        // Act
        var json = JsonSerializer.Serialize(originalDict, JSONDeserializer.JSONOptions);
        var deserializedDict = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(deserializedDict.TryGetValue(new CasePreservingString("name"), out var nameValue), Is.True);
        Assert.That(nameValue, Is.EqualTo("John Doe"));

        Assert.That(deserializedDict.TryGetValue(new CasePreservingString("NAME"), out var nameValue2), Is.True);
        Assert.That(nameValue2, Is.EqualTo("John Doe"));

        Assert.That(deserializedDict.TryGetValue(new CasePreservingString("age"), out var ageValue), Is.True);
        Assert.That(ageValue, Is.EqualTo(30));

        Assert.That(deserializedDict.TryGetValue(new CasePreservingString("isactive"), out var isActiveValue), Is.True);
        Assert.That(isActiveValue, Is.EqualTo(true));

        // Test non-existent key
        Assert.That(deserializedDict.TryGetValue(new CasePreservingString("NonExistent"), out var nonExistentValue), Is.False);
        Assert.That(nonExistentValue, Is.Null);
    }

    #endregion

    #region Complex Scenarios

    /// <summary>
    /// Tests serialization of a complex nested structure with arrays and dictionaries.
    /// </summary>
    [Test]
    public void Serialize_ComplexNestedStructure_ShouldSerializeCorrectly()
    {
        // Arrange
        var dict = new Dictionary<CasePreservingString, object>
        {
            ["Users"] = new[]
            {
                new Dictionary<CasePreservingString, object>
                {
                    ["Name"] = "John Doe",
                    ["Profile"] = new Dictionary<CasePreservingString, object>
                    {
                        ["Theme"] = "Dark",
                        ["Settings"] = new Dictionary<CasePreservingString, object>
                        {
                            ["Notifications"] = true,
                            ["AutoSave"] = false
                        }
                    }
                },
                new Dictionary<CasePreservingString, object>
                {
                    ["Name"] = "Jane Smith",
                    ["Profile"] = new Dictionary<CasePreservingString, object>
                    {
                        ["Theme"] = "Light",
                        ["Settings"] = new Dictionary<CasePreservingString, object>
                        {
                            ["Notifications"] = false,
                            ["AutoSave"] = true
                        }
                    }
                }
            },
            ["Metadata"] = new Dictionary<CasePreservingString, object>
            {
                ["Version"] = "1.0.0",
                ["LastUpdated"] = "2024-01-01T00:00:00Z"
            }
        };

        // Act
        var json = JsonSerializer.Serialize(dict, JSONDeserializer.JSONOptions);

        // Assert
        Assert.That(json, Is.Not.Null);
        Assert.That(json, Does.Contain("\"Users\""));
        Assert.That(json, Does.Contain("\"Metadata\""));
        Assert.That(json, Does.Contain("\"Profile\""));
        Assert.That(json, Does.Contain("\"Settings\""));
    }

    /// <summary>
    /// Tests deserialization of a complex nested structure.
    /// </summary>
    [Test]
    public void Deserialize_ComplexNestedStructure_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = """
        {
            "Users": [
                {
                    "Name": "John Doe",
                    "Profile": {
                        "Theme": "Dark",
                        "Settings": {
                            "Notifications": true,
                            "AutoSave": false
                        }
                    }
                },
                {
                    "Name": "Jane Smith",
                    "Profile": {
                        "Theme": "Light",
                        "Settings": {
                            "Notifications": false,
                            "AutoSave": true
                        }
                    }
                }
            ],
            "Metadata": {
                "Version": "1.0.0",
                "LastUpdated": "2024-01-01T00:00:00Z"
            }
        }
        """;

        // Act
        var result = JSONDeserializer.Deserialize<Dictionary<CasePreservingString, object>>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result[new CasePreservingString("Users")], Is.InstanceOf<object[]>());
        Assert.That(result[new CasePreservingString("Metadata")], Is.InstanceOf<Dictionary<CasePreservingString, object>>());

        var metadata = (Dictionary<CasePreservingString, object>)result[new CasePreservingString("Metadata")];
        Assert.That(metadata[new CasePreservingString("Version")], Is.EqualTo("1.0.0"));
        Assert.That(metadata[new CasePreservingString("LastUpdated")], Is.EqualTo("2024-01-01T00:00:00Z"));
    }

    #endregion
}
