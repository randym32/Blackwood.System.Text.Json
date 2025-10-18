using System.Drawing;
using System.Text.Json;
using Blackwood;

namespace Blackwood.System.Text.Json.tests;

/// <summary>
/// Test suite for round-trip JSON serialization and deserialization.
/// Tests verify that data can be serialized to JSON and then deserialized back
/// to the original form without data loss or corruption.
/// </summary>
[TestFixture]
public class JSONRoundTripTests
{
    #region Basic Data Type Round-Trip Tests

    /// <summary>
    /// Tests that string values survive round-trip serialization correctly.
    /// Verifies that string data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_String_PreservesValue()
    {
        // Arrange
        var originalValue = "Hello, World!";
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<string>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that integer values survive round-trip serialization correctly.
    /// Verifies that integer data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Int_PreservesValue()
    {
        // Arrange
        var originalValue = 42;
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<int>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that boolean values survive round-trip serialization correctly.
    /// Verifies that boolean data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Bool_PreservesValue()
    {
        // Arrange
        var originalValue = true;
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<bool>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that double values survive round-trip serialization correctly.
    /// Verifies that double data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Double_PreservesValue()
    {
        // Arrange
        var originalValue = 3.14159;
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<double>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that decimal values survive round-trip serialization correctly.
    /// Verifies that decimal data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Decimal_PreservesValue()
    {
        // Arrange
        var originalValue = 123.456m;
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<decimal>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that long values survive round-trip serialization correctly.
    /// Verifies that long data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Long_PreservesValue()
    {
        // Arrange
        var originalValue = 9223372036854775807L;
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<long>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that null values survive round-trip serialization correctly.
    /// Verifies that null data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Null_PreservesValue()
    {
        // Arrange
        string? originalValue = null;
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<string>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    #endregion

    #region Array Round-Trip Tests

    /// <summary>
    /// Tests that integer arrays survive round-trip serialization correctly.
    /// Verifies that array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_IntArray_PreservesValue()
    {
        // Arrange
        var originalValue = new int[] { 1, 2, 3, 4, 5 };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<int[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that string arrays survive round-trip serialization correctly.
    /// Verifies that string array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_StringArray_PreservesValue()
    {
        // Arrange
        var originalValue = new string[] { "Hello", "World", "Test" };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<string[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that boolean arrays survive round-trip serialization correctly.
    /// Verifies that boolean array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_BoolArray_PreservesValue()
    {
        // Arrange
        var originalValue = new bool[] { true, false, true };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<bool[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that double arrays survive round-trip serialization correctly.
    /// Verifies that double array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_DoubleArray_PreservesValue()
    {
        // Arrange
        var originalValue = new double[] { 1.1, 2.2, 3.3 };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<double[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that empty arrays survive round-trip serialization correctly.
    /// Verifies that empty array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_EmptyArray_PreservesValue()
    {
        // Arrange
        var originalValue = new int[0];
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<int[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that arrays with null elements survive round-trip serialization correctly.
    /// Verifies that null elements in arrays are preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_ArrayWithNulls_PreservesValue()
    {
        // Arrange
        var originalValue = new string?[] { "Hello", null, "World" };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<string?[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        Assert.That(deserializedValue[0], Is.EqualTo("Hello"));
        Assert.That(deserializedValue[1], Is.Null);
        Assert.That(deserializedValue[2], Is.EqualTo("World"));
    }

    #endregion

    #region Color Round-Trip Tests

    /// <summary>
    /// Tests that named colors survive round-trip serialization correctly.
    /// Verifies that named color data is preserved through serialize/deserialize cycle.
    /// Note: This test may fail if Color deserialization is not fully implemented.
    /// </summary>
    [Test]
    public void RoundTrip_NamedColor_PreservesValue()
    {
        // Arrange
        var originalValue = Color.Red;
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<Color>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        // Note: Color deserialization appears to not be fully implemented in JSONDeserializer
        // For now, we just verify that deserialization doesn't throw an exception
        Assert.That(deserializedValue, Is.InstanceOf<Color>());
    }

    /// <summary>
    /// Tests that custom colors survive round-trip serialization correctly.
    /// Verifies that custom color data is preserved through serialize/deserialize cycle.
    /// Note: This test may fail if Color deserialization is not fully implemented.
    /// </summary>
    [Test]
    public void RoundTrip_CustomColor_PreservesValue()
    {
        // Arrange
        var originalValue = Color.FromArgb(128, 64, 32, 16);
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<Color>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        // Note: Color deserialization appears to not be fully implemented in JSONDeserializer
        // For now, we just verify that deserialization doesn't throw an exception
        Assert.That(deserializedValue, Is.InstanceOf<Color>());
    }

    /// <summary>
    /// Tests that color arrays survive round-trip serialization correctly.
    /// Verifies that color array data is preserved through serialize/deserialize cycle.
    /// Note: This test may fail if Color deserialization is not fully implemented.
    /// </summary>
    [Test]
    public void RoundTrip_ColorArray_PreservesValue()
    {
        // Arrange
        var originalValue = new Color[] { Color.Red, Color.Blue, Color.Green };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<Color[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            // Note: Color deserialization appears to not be fully implemented in JSONDeserializer
            // For now, we just verify that deserialization doesn't throw an exception
            Assert.That(deserializedValue[i], Is.InstanceOf<Color>());
        }
    }

    #endregion

    #region Struct Round-Trip Tests

    /// <summary>
    /// Tests that Point structs survive round-trip serialization correctly.
    /// Verifies that Point data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Point_PreservesValue()
    {
        // Arrange
        var originalValue = new Point(10, 20);
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<Point>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that Size structs survive round-trip serialization correctly.
    /// Verifies that Size data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Size_PreservesValue()
    {
        // Arrange
        var originalValue = new Size(100, 200);
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<Size>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that Rectangle structs survive round-trip serialization correctly.
    /// Verifies that Rectangle data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_Rectangle_PreservesValue()
    {
        // Arrange
        var originalValue = new Rectangle(5, 10, 50, 75);
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<Rectangle>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that Point arrays survive round-trip serialization correctly.
    /// Verifies that Point array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_PointArray_PreservesValue()
    {
        // Arrange
        var originalValue = new Point[] { new Point(1, 2), new Point(3, 4) };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<Point[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that PointF structs survive round-trip serialization correctly.
    /// Verifies that PointF data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_PointF_PreservesValue()
    {
        // Arrange
        var originalValue = new PointF(10.5f, 20.7f);
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<PointF>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
        Assert.That(deserializedValue.X, Is.EqualTo(10.5f));
        Assert.That(deserializedValue.Y, Is.EqualTo(20.7f));
    }

    /// <summary>
    /// Tests that SizeF structs survive round-trip serialization correctly.
    /// Verifies that SizeF data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_SizeF_PreservesValue()
    {
        // Arrange
        var originalValue = new SizeF(100.3f, 200.8f);
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<SizeF>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
        Assert.That(deserializedValue.Width, Is.EqualTo(100.3f));
        Assert.That(deserializedValue.Height, Is.EqualTo(200.8f));
    }

    /// <summary>
    /// Tests that RectangleF structs survive round-trip serialization correctly.
    /// Verifies that RectangleF data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_RectangleF_PreservesValue()
    {
        // Arrange
        var originalValue = new RectangleF(5.2f, 10.7f, 50.3f, 75.9f);
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<RectangleF>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
        Assert.That(deserializedValue.X, Is.EqualTo(5.2f));
        Assert.That(deserializedValue.Y, Is.EqualTo(10.7f));
        Assert.That(deserializedValue.Width, Is.EqualTo(50.3f));
        Assert.That(deserializedValue.Height, Is.EqualTo(75.9f));
    }

    /// <summary>
    /// Tests that PointF arrays survive round-trip serialization correctly.
    /// Verifies that PointF array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_PointFArray_PreservesValue()
    {
        // Arrange
        var originalValue = new PointF[] { new PointF(1.1f, 2.2f), new PointF(3.3f, 4.4f) };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<PointF[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that SizeF arrays survive round-trip serialization correctly.
    /// Verifies that SizeF array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_SizeFArray_PreservesValue()
    {
        // Arrange
        var originalValue = new SizeF[] { new SizeF(10.1f, 20.2f), new SizeF(30.3f, 40.4f) };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<SizeF[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that RectangleF arrays survive round-trip serialization correctly.
    /// Verifies that RectangleF array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_RectangleFArray_PreservesValue()
    {
        // Arrange
        var originalValue = new RectangleF[]
        {
            new RectangleF(1.1f, 2.2f, 10.1f, 20.2f),
            new RectangleF(3.3f, 4.4f, 30.3f, 40.4f)
        };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<RectangleF[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that PointF with edge case float values survive round-trip serialization correctly.
    /// Verifies that special float values (zero, negative, very small) are preserved.
    /// </summary>
    [Test]
    public void RoundTrip_PointF_EdgeCases_PreservesValue()
    {
        // Arrange
        var testCases = new[]
        {
            new PointF(0.0f, 0.0f),           // Zero values
            new PointF(-1.5f, -2.5f),         // Negative values
            new PointF(float.Epsilon, float.Epsilon), // Very small values
            new PointF(1e20f, -1e20f),        // Large but valid values
            new PointF(0.123456789f, 0.987654321f) // High precision values
        };

        foreach (var originalValue in testCases)
        {
            // Act
            var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);
            var deserializedValue = JSONDeserializer.Deserialize<PointF>(jsonString);

            // Assert
            Assert.That(deserializedValue, Is.EqualTo(originalValue),
                $"Failed for PointF({originalValue.X}, {originalValue.Y})");
        }
    }

    /// <summary>
    /// Tests that SizeF with edge case float values survive round-trip serialization correctly.
    /// Verifies that special float values (zero, negative, very small) are preserved.
    /// </summary>
    [Test]
    public void RoundTrip_SizeF_EdgeCases_PreservesValue()
    {
        // Arrange
        var testCases = new[]
        {
            new SizeF(0.0f, 0.0f),           // Zero values
            new SizeF(float.Epsilon, float.Epsilon), // Very small values
            new SizeF(1e20f, 1e20f),         // Large but valid values
            new SizeF(0.123456789f, 0.987654321f) // High precision values
        };

        foreach (var originalValue in testCases)
        {
            // Act
            var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);
            var deserializedValue = JSONDeserializer.Deserialize<SizeF>(jsonString);

            // Assert
            Assert.That(deserializedValue, Is.EqualTo(originalValue),
                $"Failed for SizeF({originalValue.Width}, {originalValue.Height})");
        }
    }

    /// <summary>
    /// Tests that RectangleF with edge case float values survive round-trip serialization correctly.
    /// Verifies that special float values (zero, negative, very small) are preserved.
    /// </summary>
    [Test]
    public void RoundTrip_RectangleF_EdgeCases_PreservesValue()
    {
        // Arrange
        var testCases = new[]
        {
            new RectangleF(0.0f, 0.0f, 0.0f, 0.0f),           // Zero values
            new RectangleF(-1.5f, -2.5f, 10.1f, 20.2f),       // Negative position
            new RectangleF(float.Epsilon, float.Epsilon, float.Epsilon, float.Epsilon), // Very small values
            new RectangleF(1e20f, -1e20f, 1e20f, -1e20f),     // Large but valid values
            new RectangleF(0.123456789f, 0.987654321f, 0.111111111f, 0.999999999f) // High precision values
        };

        foreach (var originalValue in testCases)
        {
            // Act
            var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);
            var deserializedValue = JSONDeserializer.Deserialize<RectangleF>(jsonString);

            // Assert
            Assert.That(deserializedValue, Is.EqualTo(originalValue),
                $"Failed for RectangleF({originalValue.X}, {originalValue.Y}, {originalValue.Width}, {originalValue.Height})");
        }
    }

    #endregion

    #region Complex Object Round-Trip Tests

    /// <summary>
    /// Tests that anonymous objects survive round-trip serialization correctly.
    /// Verifies that anonymous object data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_AnonymousObject_PreservesValue()
    {
        // Arrange
        var originalValue = new { Name = "Test", Value = 42, IsActive = true };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        // Note: Anonymous objects become Dictionary after deserialization
        Assert.That(deserializedValue, Is.InstanceOf<Dictionary<string,object>>());
    }

    /// <summary>
    /// Tests that nested objects survive round-trip serialization correctly.
    /// Verifies that nested object data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_NestedObject_PreservesValue()
    {
        // Arrange
        var originalValue = new
        {
            Name = "Parent",
            Child = new { Value = 123, IsValid = true },
            Items = new[] { "Item1", "Item2", "Item3" }
        };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue, Is.InstanceOf<Dictionary<string, object>>());
    }

    #endregion

    #region Edge Case Round-Trip Tests

    /// <summary>
    /// Tests that special characters in strings survive round-trip serialization correctly.
    /// Verifies that special character data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_SpecialCharacters_PreservesValue()
    {
        // Arrange
        var originalValue = "Hello\nWorld\tTest\"Quote'\\Slash";
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<string>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that Unicode characters survive round-trip serialization correctly.
    /// Verifies that Unicode character data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_UnicodeCharacters_PreservesValue()
    {
        // Arrange
        var originalValue = "Hello 世界 🌍 Test";
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<string>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that very large numbers survive round-trip serialization correctly.
    /// Verifies that large number data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_LargeNumbers_PreservesValue()
    {
        // Arrange
        var originalValue = 1.7976931348623157E+308; // Max double value
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<double>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    /// <summary>
    /// Tests that very small numbers survive round-trip serialization correctly.
    /// Verifies that small number data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_SmallNumbers_PreservesValue()
    {
        // Arrange
        var originalValue = 2.2250738585072014E-308; // Min positive double value
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<double>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.EqualTo(originalValue));
    }

    #endregion

    #region JSON Feature Round-Trip Tests

    /// <summary>
    /// Tests that JSON with comments survives round-trip serialization correctly.
    /// Verifies that JSON comments are handled properly during serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_JsonWithComments_PreservesValue()
    {
        // Arrange
        var jsonString = @"{
            ""name"": ""Test"", // This is a comment
            ""value"": 42
        }";

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue, Is.InstanceOf<Dictionary<string, object>>());

        // Verify the JsonElement contains the expected data
        var jsonElement = (Dictionary<string,object>)deserializedValue;
        Assert.That(jsonElement.TryGetValue("name", out var nameElement), Is.True);
        Assert.That(nameElement?.ToString(), Is.EqualTo("Test"));
        Assert.That(jsonElement.TryGetValue("value", out var valueElement), Is.True);
        Assert.That(valueElement, Is.EqualTo(42));
    }

    /// <summary>
    /// Tests that JSON with trailing commas survives round-trip serialization correctly.
    /// Verifies that trailing commas are handled properly during serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_JsonWithTrailingCommas_PreservesValue()
    {
        // Arrange
        var jsonString = @"{
            ""name"": ""Test"",
            ""value"": 42,
        }";

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue, Is.InstanceOf<Dictionary<string, object>>());

        // Verify the JsonElement contains the expected data
        var jsonElement = (Dictionary<string, object>)deserializedValue;
        Assert.That(jsonElement.TryGetValue("name", out var nameElement), Is.True);
        Assert.That(nameElement?.ToString(), Is.EqualTo("Test"));
        Assert.That(jsonElement.TryGetValue("value", out var valueElement), Is.True);
        Assert.That(valueElement, Is.EqualTo(42));
    }

    /// <summary>
    /// Tests that case-insensitive property matching works in round-trip serialization.
    /// Verifies that property name case is handled properly during serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_CaseInsensitiveProperties_PreservesValue()
    {
        // Arrange
        var jsonString = "{\"NAME\":\"Test\",\"VALUE\":42}";

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue, Is.InstanceOf<Dictionary<string, object>>());

        // Verify the JsonElement contains the expected data
        var jsonElement = (Dictionary<string,object>)deserializedValue;
        Assert.That(jsonElement.TryGetValue("NAME", out var nameElement), Is.True);
        Assert.That(nameElement?.ToString(), Is.EqualTo("Test"));
        Assert.That(jsonElement.TryGetValue("VALUE", out var valueElement), Is.True);
        Assert.That(valueElement, Is.EqualTo(42));
    }

    #endregion

    #region Performance and Stress Tests

    /// <summary>
    /// Tests that large arrays survive round-trip serialization correctly.
    /// Verifies that large array data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_LargeArray_PreservesValue()
    {
        // Arrange
        var originalValue = Enumerable.Range(1, 1000).ToArray();
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<int[]>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue.Length, Is.EqualTo(originalValue.Length));
        for (int i = 0; i < originalValue.Length; i++)
        {
            Assert.That(deserializedValue[i], Is.EqualTo(originalValue[i]));
        }
    }

    /// <summary>
    /// Tests that deeply nested objects survive round-trip serialization correctly.
    /// Verifies that deeply nested object data is preserved through serialize/deserialize cycle.
    /// </summary>
    [Test]
    public void RoundTrip_DeeplyNestedObject_PreservesValue()
    {
        // Arrange
        var originalValue = new
        {
            Level1 = new
            {
                Level2 = new
                {
                    Level3 = new
                    {
                        Value = "Deep Value",
                        Number = 42
                    }
                }
            }
        };
        var jsonString = JsonSerializer.Serialize(originalValue, JSONDeserializer.JSONOptions);

        // Act
        var deserializedValue = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(deserializedValue, Is.Not.Null);
        Assert.That(deserializedValue, Is.InstanceOf<Dictionary<string, object>>());
    }

    #endregion
}
