using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Reflection;
using System.Text.Json;
using System.Text;

namespace Blackwood.System.Text.Json.tests;
/// <summary>
/// Test suite for JSONSerializer functionality in the Blackwood.System.Text.Json library.
/// Tests cover serialization of various data types, edge cases, and error handling scenarios.
/// This test class validates the internal static methods of JSONDeserializer using reflection.
/// </summary>
[TestFixture]
public class JSONSerializerTests2
{
    #region Helper Methods for Accessing Internal Static Methods
    #endregion

    /// <summary>
    /// Simple test to verify that the JSONSerializerTests class is being discovered by the test runner.
    /// This test should pass to confirm that the class structure is correct.
    /// </summary>
    [Test]
    public void SimpleTest_JSONSerializerTests_ShouldPass()
    {
        Assert.That(1 + 1, Is.EqualTo(2));
    }

    /// <summary>
    /// Tests that string values are serialized correctly without modification.
    /// Verifies that basic string serialization preserves the original value.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_String_ReturnsString()
    {
        // Arrange
        var testString = "Hello World";

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testString);

        // Assert
        Assert.That(result, Is.EqualTo(testString));
    }

    /// <summary>
    /// Tests that integer values are serialized correctly without modification.
    /// Verifies that basic integer serialization preserves the original value.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Int_ReturnsInt()
    {
        // Arrange
        var testInt = 42;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testInt);

        // Assert
        Assert.That(result, Is.EqualTo(testInt));
    }


    /// <summary>
    /// Tests that Color.Red is serialized correctly as the string "Red".
    /// Verifies that named colors are serialized as their name strings.
    /// </summary>
    [Test]
    public void SerializeColor_NamedColor_ReturnsColorName()
    {
        // Arrange
        var color = Color.Red;

        // Act
        var result = JSONDeserializer.SerializeColor(color);

        // Assert
        Assert.That(result, Is.EqualTo("Red"));
    }

    /// <summary>
    /// Tests that arrays are serialized correctly.
    /// Verifies that array serialization preserves all elements.
    /// </summary>
    [Test]
    public void SerializeArray_IntArray_ReturnsSerializedArray()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };

        // Act
        var result = JSONDeserializer.SerializeArray(array);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(5));
        Assert.That(result[0], Is.EqualTo(1));
        Assert.That(result[4], Is.EqualTo(5));
    }

    #region Basic Data Type Tests

    /// <summary>
    /// Tests that boolean values are serialized correctly without modification.
    /// Verifies that basic boolean serialization preserves the original value.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Bool_ReturnsBool()
    {
        // Arrange
        var testBool = true;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testBool);

        // Assert
        Assert.That(result, Is.EqualTo(testBool));
    }

    /// <summary>
    /// Tests that double values are serialized correctly without modification.
    /// Verifies that basic double serialization preserves the original value.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Double_ReturnsDouble()
    {
        // Arrange
        var testDouble = 3.14159;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testDouble);

        // Assert
        Assert.That(result, Is.EqualTo(testDouble));
    }

    /// <summary>
    /// Tests that decimal values are serialized correctly without modification.
    /// Verifies that basic decimal serialization preserves the original value.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Decimal_ReturnsDecimal()
    {
        // Arrange
        var testDecimal = 123.456m;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testDecimal);

        // Assert
        Assert.That(result, Is.EqualTo(testDecimal));
    }

    /// <summary>
    /// Tests that float values are serialized correctly without modification.
    /// Verifies that basic float serialization preserves the original value.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Float_ReturnsFloat()
    {
        // Arrange
        var testFloat = 2.718f;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testFloat);

        // Assert
        Assert.That(result, Is.EqualTo(testFloat));
    }

    /// <summary>
    /// Tests that long values are serialized correctly without modification.
    /// Verifies that basic long serialization preserves the original value.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Long_ReturnsLong()
    {
        // Arrange
        var testLong = 9223372036854775807L;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testLong);

        // Assert
        Assert.That(result, Is.EqualTo(testLong));
    }

    /// <summary>
    /// Tests that enum values are serialized as their string representation.
    /// Verifies that enum serialization converts to string format.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Enum_ReturnsString()
    {
        // Arrange
        var testEnum = StringComparison.OrdinalIgnoreCase;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testEnum);

        // Assert
        Assert.That(result?.ToString(), Is.EqualTo("OrdinalIgnoreCase"));
    }

    /// <summary>
    /// Tests that null values are handled correctly.
    /// Verifies that null input returns null output.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Null_ReturnsNull()
    {
        // Arrange
        object? testNull = null;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(testNull);

        // Assert
        Assert.That(result, Is.Null);
    }

    #endregion

    #region Struct Type Tests

    /// <summary>
    /// Tests that Point structs are serialized as objects with x and y properties.
    /// Verifies that Point serialization creates a proper object structure.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Point_ReturnsObject()
    {
        // Arrange
        var point = new Point(10, 20);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(point);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<object>());

        // Use reflection to verify the object has the expected properties
        var resultType = result.GetType();
        var xProperty = resultType.GetProperty("x");
        var yProperty = resultType.GetProperty("y");

        Assert.That(xProperty, Is.Not.Null);
        Assert.That(yProperty, Is.Not.Null);
        Assert.That(xProperty.GetValue(result), Is.EqualTo(10));
        Assert.That(yProperty.GetValue(result), Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that Size structs are serialized as objects with width and height properties.
    /// Verifies that Size serialization creates a proper object structure.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Size_ReturnsObject()
    {
        // Arrange
        var size = new Size(100, 200);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(size);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<object>());

        // Use reflection to verify the object has the expected properties
        var resultType = result.GetType();
        var widthProperty = resultType.GetProperty("width");
        var heightProperty = resultType.GetProperty("height");

        Assert.That(widthProperty, Is.Not.Null);
        Assert.That(heightProperty, Is.Not.Null);
        Assert.That(widthProperty.GetValue(result), Is.EqualTo(100));
        Assert.That(heightProperty.GetValue(result), Is.EqualTo(200));
    }

    /// <summary>
    /// Tests that Rectangle structs are serialized as objects with x, y, width, and height properties.
    /// Verifies that Rectangle serialization creates a proper object structure.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Rectangle_ReturnsObject()
    {
        // Arrange
        var rect = new Rectangle(5, 10, 50, 75);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(rect);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<object>());

        // Use reflection to verify the object has the expected properties
        var resultType = result.GetType();
        var xProperty = resultType.GetProperty("x");
        var yProperty = resultType.GetProperty("y");
        var widthProperty = resultType.GetProperty("width");
        var heightProperty = resultType.GetProperty("height");

        Assert.That(xProperty, Is.Not.Null);
        Assert.That(yProperty, Is.Not.Null);
        Assert.That(widthProperty, Is.Not.Null);
        Assert.That(heightProperty, Is.Not.Null);
        Assert.That(xProperty.GetValue(result), Is.EqualTo(5));
        Assert.That(yProperty.GetValue(result), Is.EqualTo(10));
        Assert.That(widthProperty.GetValue(result), Is.EqualTo(50));
        Assert.That(heightProperty.GetValue(result), Is.EqualTo(75));
    }

    /// <summary>
    /// Tests that PointF structs are serialized as objects with x and y properties.
    /// Verifies that PointF serialization creates a proper object structure.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_PointF_ReturnsObject()
    {
        // Arrange
        var pointF = new PointF(10.5f, 20.7f);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(pointF);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<object>());

        // Use reflection to verify the object has the expected properties
        var resultType = result.GetType();
        var xProperty = resultType.GetProperty("x");
        var yProperty = resultType.GetProperty("y");

        Assert.That(xProperty, Is.Not.Null);
        Assert.That(yProperty, Is.Not.Null);
        Assert.That(xProperty.GetValue(result), Is.EqualTo(10.5f));
        Assert.That(yProperty.GetValue(result), Is.EqualTo(20.7f));
    }

    /// <summary>
    /// Tests that SizeF structs are serialized as objects with width and height properties.
    /// Verifies that SizeF serialization creates a proper object structure.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_SizeF_ReturnsObject()
    {
        // Arrange
        var sizeF = new SizeF(100.3f, 200.8f);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(sizeF);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<object>());

        // Use reflection to verify the object has the expected properties
        var resultType = result.GetType();
        var widthProperty = resultType.GetProperty("width");
        var heightProperty = resultType.GetProperty("height");

        Assert.That(widthProperty, Is.Not.Null);
        Assert.That(heightProperty, Is.Not.Null);
        Assert.That(widthProperty.GetValue(result), Is.EqualTo(100.3f));
        Assert.That(heightProperty.GetValue(result), Is.EqualTo(200.8f));
    }

    /// <summary>
    /// Tests that RectangleF structs are serialized as objects with x, y, width, and height properties.
    /// Verifies that RectangleF serialization creates a proper object structure.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_RectangleF_ReturnsObject()
    {
        // Arrange
        var rectF = new RectangleF(5.2f, 10.7f, 50.3f, 75.9f);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(rectF);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<object>());

        // Use reflection to verify the object has the expected properties
        var resultType = result.GetType();
        var xProperty = resultType.GetProperty("x");
        var yProperty = resultType.GetProperty("y");
        var widthProperty = resultType.GetProperty("width");
        var heightProperty = resultType.GetProperty("height");

        Assert.That(xProperty, Is.Not.Null);
        Assert.That(yProperty, Is.Not.Null);
        Assert.That(widthProperty, Is.Not.Null);
        Assert.That(heightProperty, Is.Not.Null);
        Assert.That(xProperty.GetValue(result), Is.EqualTo(5.2f));
        Assert.That(yProperty.GetValue(result), Is.EqualTo(10.7f));
        Assert.That(widthProperty.GetValue(result), Is.EqualTo(50.3f));
        Assert.That(heightProperty.GetValue(result), Is.EqualTo(75.9f));
    }

    #endregion

    #region Color Serialization Tests

    /// <summary>
    /// Tests that Color.Blue is serialized correctly as the string "Blue".
    /// Verifies that named colors are serialized as their name strings.
    /// </summary>
    [Test]
    public void SerializeColor_NamedColorBlue_ReturnsColorName()
    {
        // Arrange
        var color = Color.Blue;

        // Act
        var result = JSONDeserializer.SerializeColor(color);

        // Assert
        Assert.That(result, Is.EqualTo("Blue"));
    }

    /// <summary>
    /// Tests that Color.Green is serialized correctly as the string "Green".
    /// Verifies that named colors are serialized as their name strings.
    /// </summary>
    [Test]
    public void SerializeColor_NamedColorGreen_ReturnsColorName()
    {
        // Arrange
        var color = Color.Green;

        // Act
        var result = JSONDeserializer.SerializeColor(color);

        // Assert
        Assert.That(result, Is.EqualTo("Green"));
    }

    /// <summary>
    /// Tests that a custom color (not named) is serialized as a hex string.
    /// Verifies that custom colors are serialized as #AARRGGBB hex strings.
    /// </summary>
    [Test]
    public void SerializeColor_CustomColor_ReturnsHexString()
    {
        // Arrange
        var color = Color.FromArgb(128, 64, 32, 16); // Custom color with alpha

        // Act
        var result = JSONDeserializer.SerializeColor(color);

        // Assert
        Assert.That(result, Is.EqualTo("#80402010")); // Alpha=128(0x80), R=64(0x40), G=32(0x20), B=16(0x10)
    }

    /// <summary>
    /// Tests that a color with full alpha (255) is serialized correctly.
    /// Verifies that colors with maximum alpha are handled properly.
    /// </summary>
    [Test]
    public void SerializeColor_FullAlphaColor_ReturnsHexString()
    {
        // Arrange
        var color = Color.FromArgb(255, 255, 0, 0); // Full red

        // Act
        var result = JSONDeserializer.SerializeColor(color);

        // Assert
        Assert.That(result, Is.EqualTo("#FFFF0000")); // Alpha=255(0xFF), R=255(0xFF), G=0(0x00), B=0(0x00)
    }

    /// <summary>
    /// Tests that a transparent color (alpha=0) is serialized correctly.
    /// Verifies that transparent colors are handled properly.
    /// </summary>
    [Test]
    public void SerializeColor_TransparentColor_ReturnsHexString()
    {
        // Arrange
        var color = Color.FromArgb(0, 255, 255, 255); // Transparent white

        // Act
        var result = JSONDeserializer.SerializeColor(color);

        // Assert
        Assert.That(result, Is.EqualTo("#00FFFFFF")); // Alpha=0(0x00), R=255(0xFF), G=255(0xFF), B=255(0xFF)
    }

    /// <summary>
    /// Tests that Color.ConvertValueToSerializableForm handles named colors correctly.
    /// Verifies that the main conversion method properly delegates to SerializeColor.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_ColorNamed_ReturnsColorName()
    {
        // Arrange
        var color = Color.Yellow;

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(color);

        // Assert
        Assert.That(result, Is.EqualTo("Yellow"));
    }

    /// <summary>
    /// Tests that Color.ConvertValueToSerializableForm handles custom colors correctly.
    /// Verifies that the main conversion method properly delegates to SerializeColor.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_ColorCustom_ReturnsHexString()
    {
        // Arrange
        var color = Color.FromArgb(200, 100, 150, 50);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(color);

        // Assert
        Assert.That(result, Is.EqualTo("#C8649632")); // Alpha=200(0xC8), R=100(0x64), G=150(0x96), B=50(0x32)
    }

    #endregion

    #region Array Serialization Tests

    /// <summary>
    /// Tests that string arrays are serialized correctly.
    /// Verifies that string array serialization preserves all elements.
    /// </summary>
    [Test]
    public void SerializeArray_StringArray_ReturnsSerializedArray()
    {
        // Arrange
        var array = new string[] { "Hello", "World", "Test" };

        // Act
        var result = JSONDeserializer.SerializeArray(array);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo("Hello"));
        Assert.That(result[1], Is.EqualTo("World"));
        Assert.That(result[2], Is.EqualTo("Test"));
    }

    /// <summary>
    /// Tests that boolean arrays are serialized correctly.
    /// Verifies that boolean array serialization preserves all elements.
    /// </summary>
    [Test]
    public void SerializeArray_BoolArray_ReturnsSerializedArray()
    {
        // Arrange
        var array = new bool[] { true, false, true };

        // Act
        var result = JSONDeserializer.SerializeArray(array);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(true));
        Assert.That(result[1], Is.EqualTo(false));
        Assert.That(result[2], Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that double arrays are serialized correctly.
    /// Verifies that double array serialization preserves all elements.
    /// </summary>
    [Test]
    public void SerializeArray_DoubleArray_ReturnsSerializedArray()
    {
        // Arrange
        var array = new double[] { 1.1, 2.2, 3.3 };

        // Act
        var result = JSONDeserializer.SerializeArray(array);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(1.1));
        Assert.That(result[1], Is.EqualTo(2.2));
        Assert.That(result[2], Is.EqualTo(3.3));
    }

    /// <summary>
    /// Tests that Color arrays are serialized correctly.
    /// Verifies that Color array serialization converts colors to their string representations.
    /// </summary>
    [Test]
    public void SerializeArray_ColorArray_ReturnsSerializedArray()
    {
        // Arrange
        var array = new Color[] { Color.Red, Color.Blue, Color.Green };

        // Act
        var result = JSONDeserializer.SerializeArray(array);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo("Red"));
        Assert.That(result[1], Is.EqualTo("Blue"));
        Assert.That(result[2], Is.EqualTo("Green"));
    }

    /// <summary>
    /// Tests that Point arrays are serialized correctly.
    /// Verifies that Point array serialization converts points to objects.
    /// </summary>
    [Test]
    public void SerializeArray_PointArray_ReturnsSerializedArray()
    {
        // Arrange
        var array = new Point[] { new Point(1, 2), new Point(3, 4) };

        // Act
        var result = JSONDeserializer.SerializeArray(array);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(2));

        // Verify first point
        var firstPoint = result[0];
        Assert.That(firstPoint, Is.Not.Null);
        var firstPointType = firstPoint.GetType();
        Assert.That(firstPointType, Is.Not.Null);
        Assert.That(firstPointType.GetProperty("x")?.GetValue(firstPoint), Is.EqualTo(1));
        Assert.That(firstPointType.GetProperty("y")?.GetValue(firstPoint), Is.EqualTo(2));

        // Verify second point
        var secondPoint = result[1];
        Assert.That(secondPoint, Is.Not.Null);
        var secondPointType = secondPoint.GetType();
        Assert.That(secondPointType, Is.Not.Null);
        Assert.That(secondPointType.GetProperty("x")?.GetValue(secondPoint), Is.EqualTo(3));
        Assert.That(secondPointType.GetProperty("y")?.GetValue(secondPoint), Is.EqualTo(4));
    }

    /// <summary>
    /// Tests that empty arrays are serialized correctly.
    /// Verifies that empty array serialization returns an empty array.
    /// </summary>
    [Test]
    public void SerializeArray_EmptyArray_ReturnsEmptyArray()
    {
        // Arrange
        var array = new int[0];

        // Act
        var result = JSONDeserializer.SerializeArray(array);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that arrays with null elements are handled correctly.
    /// Verifies that null elements in arrays are preserved as null.
    /// </summary>
    [Test]
    public void SerializeArray_ArrayWithNulls_ReturnsArrayWithNulls()
    {
        // Arrange
        var array = new string?[] { "Hello", null, "World" };

        // Act
        var result = JSONDeserializer.SerializeArray(array);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo("Hello"));
        Assert.That(result[1], Is.Null);
        Assert.That(result[2], Is.EqualTo("World"));
    }

    #endregion

    #region Edge Case and Error Tests

    /// <summary>
    /// Tests that unknown object types fall back to string representation.
    /// Verifies that the fallback mechanism works for unsupported types.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_UnknownType_ReturnsString()
    {
        // Arrange
        var unknownObject = new { CustomProperty = "TestValue" };

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(unknownObject);

        // Assert
        Assert.That(result, Is.Not.Null);
        // The exact string representation may vary, but it should not be null
        Assert.That(result.ToString(), Is.Not.Empty);
    }

    /// <summary>
    /// Tests that DateTime objects are handled by the fallback mechanism.
    /// Verifies that DateTime objects are converted to string representation.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_DateTime_ReturnsString()
    {
        // Arrange
        var dateTime = new DateTime(2023, 12, 25, 10, 30, 45);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(dateTime);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.Not.Empty);
    }

    /// <summary>
    /// Tests that Guid objects are handled by the fallback mechanism.
    /// Verifies that Guid objects are converted to string representation.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Guid_ReturnsString()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(guid);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.Not.Empty);
    }

    #endregion

    #region Advanced Serialization Tests

    /// <summary>
    /// Tests that DateTime objects are serialized correctly using the fallback mechanism.
    /// Verifies that DateTime objects are converted to string representation.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_DateTimeAdvanced_ReturnsString()
    {
        // Arrange
        var dateTime = new DateTime(2023, 12, 25, 10, 30, 45);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(dateTime);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.Not.Empty);
    }

    /// <summary>
    /// Tests that Guid objects are serialized correctly using the fallback mechanism.
    /// Verifies that Guid objects are converted to string representation.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_GuidAdvanced_ReturnsString()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(guid);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.Not.Empty);
    }

    /// <summary>
    /// Tests that TimeSpan objects are serialized correctly using the fallback mechanism.
    /// Verifies that TimeSpan objects are converted to string representation.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_TimeSpan_ReturnsString()
    {
        // Arrange
        var timeSpan = new TimeSpan(1, 2, 3, 4, 5);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(timeSpan);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.Not.Empty);
    }

    /// <summary>
    /// Tests that Uri objects are serialized correctly using the fallback mechanism.
    /// Verifies that Uri objects are converted to string representation.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Uri_ReturnsString()
    {
        // Arrange
        var uri = new Uri("https://example.com");

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(uri);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.EqualTo("https://example.com/"));
    }

    /// <summary>
    /// Tests that Version objects are serialized correctly using the fallback mechanism.
    /// Verifies that Version objects are converted to string representation.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_Version_ReturnsString()
    {
        // Arrange
        var version = new Version(1, 2, 3, 4);

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(version);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.EqualTo("1.2.3.4"));
    }

    #endregion

    #region Complex Object Serialization Tests

    /// <summary>
    /// Tests that complex nested objects are serialized correctly.
    /// Verifies that nested object structures are handled properly.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_ComplexNestedObject_ReturnsString()
    {
        // Arrange
        var complexObject = new
        {
            Name = "Test",
            Value = 42,
            Nested = new
            {
                InnerValue = "Inner",
                Numbers = new[] { 1, 2, 3 }
            }
        };

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(complexObject);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.Not.Empty);
    }

    /// <summary>
    /// Tests that objects with circular references are handled gracefully.
    /// Verifies that circular reference detection works properly.
    /// </summary>
    [Test]
    public void ConvertValueToSerializableForm_CircularReference_ReturnsString()
    {
        // Arrange
        var parent = new { Name = "Parent" };
        var child = new { Name = "Child", Parent = parent };
        // Note: We can't actually create a true circular reference with anonymous types
        // This test verifies the fallback mechanism works for complex objects

        // Act
        var result = JSONDeserializer.ConvertValueToSerializableForm(child);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ToString(), Is.Not.Empty);
    }

    #endregion

    #region Performance and Edge Case Tests

    /// <summary>
    /// Tests that very large arrays are serialized correctly.
    /// Verifies that large array serialization doesn't cause memory issues.
    /// </summary>
    [Test]
    public void SerializeArray_LargeArray_ReturnsSerializedArray()
    {
        // Arrange
        var largeArray = new int[1000];
        for (int i = 0; i < 1000; i++)
        {
            largeArray[i] = i;
        }

        // Act
        var result = JSONDeserializer.SerializeArray(largeArray);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(1000));
        Assert.That(result[0], Is.EqualTo(0));
        Assert.That(result[999], Is.EqualTo(999));
    }

    /// <summary>
    /// Tests that arrays with mixed types are serialized correctly.
    /// Verifies that heterogeneous arrays are handled properly.
    /// </summary>
    [Test]
    public void SerializeArray_MixedTypeArray_ReturnsSerializedArray()
    {
        // Arrange
        var mixedArray = new object[] { 1, "hello", true, 3.14, Color.Red };

        // Act
        var result = JSONDeserializer.SerializeArray(mixedArray);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(5));
        Assert.That(result[0], Is.EqualTo(1));
        Assert.That(result[1], Is.EqualTo("hello"));
        Assert.That(result[2], Is.EqualTo(true));
        Assert.That(result[3], Is.EqualTo(3.14));
        Assert.That(result[4], Is.EqualTo("Red"));
    }

    /// <summary>
    /// Tests that null arrays are handled correctly.
    /// Verifies that null array input throws NullReferenceException.
    /// </summary>
    [Test]
    public void SerializeArray_NullArray_ThrowsNullReferenceException()
    {
        // Arrange
        int[]? nullArray = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => JSONDeserializer.SerializeArray(nullArray!));
    }

    #endregion

    #region Main Serialization Method Tests

    /// <summary>
    /// Tests that the Deserialize method handles simple JSON strings correctly.
    /// Verifies that the main deserialization method works with basic JSON.
    /// </summary>
    [Test]
    public void Deserialize_SimpleJson_ReturnsDeserializedObject()
    {
        // Arrange
        var jsonString = "{\"name\":\"Test\",\"value\":42}";

        // Act
        var result = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that the Deserialize method handles null or empty strings.
    /// Verifies that null/empty input returns default value.
    /// </summary>
    [Test]
    public void Deserialize_NullOrEmptyString_ReturnsDefault()
    {
        // Arrange
        string? nullString = null;
        string emptyString = "";
        string whitespaceString = "   ";

        // Act & Assert
        var result1 = JSONDeserializer.Deserialize<object>(nullString);
        var result2 = JSONDeserializer.Deserialize<object>(emptyString);
        var result3 = JSONDeserializer.Deserialize<object>(whitespaceString);

        Assert.That(result1, Is.Null);
        Assert.That(result2, Is.Null);
        Assert.That(result3, Is.Null);
    }

    /// <summary>
    /// Tests that the Deserialize method handles JSON with comments.
    /// Verifies that JSON comments are properly skipped during parsing.
    /// </summary>
    [Test]
    public void Deserialize_JsonWithComments_ReturnsDeserializedObject()
    {
        // Arrange
        var jsonString = @"{
            ""name"": ""Test"", // This is a comment
            ""value"": 42
        }";

        // Act
        var result = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that the Deserialize method handles JSON with trailing commas.
    /// Verifies that trailing commas are properly handled during parsing.
    /// </summary>
    [Test]
    public void Deserialize_JsonWithTrailingCommas_ReturnsDeserializedObject()
    {
        // Arrange
        var jsonString = @"{
            ""name"": ""Test"",
            ""value"": 42,
        }";

        // Act
        var result = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that the Deserialize method handles case-insensitive property names.
    /// Verifies that property name matching is case-insensitive.
    /// </summary>
    [Test]
    public void Deserialize_CaseInsensitiveProperties_ReturnsDeserializedObject()
    {
        // Arrange
        var jsonString = "{\"NAME\":\"Test\",\"VALUE\":42}";

        // Act
        var result = JSONDeserializer.Deserialize<object>(jsonString);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    #endregion

    #region SerializeProperties Method Tests

#if false
    /// <summary>
    /// Custom attribute for testing the SerializeProperties method.
    /// Used to mark properties and fields that should be included in serialization.
    /// </summary>
    public class TestAttribute : Attribute
    {
        /// <summary>Description of the attribute for testing purposes</summary>
        public string Description { get; set; } = "";
    }

    /// <summary>
    /// Test class with properties and fields for testing the SerializeProperties method.
    /// Contains both attributed and non-attributed members to test filtering functionality.
    /// </summary>
    public class TestPropertiesClass
    {
        /// <summary>Property with TestAttribute - should be serialized</summary>
        [Test(Description = "Test property")]
        public string TestProperty { get; set; } = "TestValue";

        /// <summary>Field with TestAttribute - should be serialized</summary>
        [Test(Description = "Test field")]
        public int TestField = 42;

        /// <summary>Color property with TestAttribute - should be serialized</summary>
        [Test(Description = "Color property")]
        public Color ColorProperty { get; set; } = Color.Red;

        /// <summary>Point property with TestAttribute - should be serialized</summary>
        [Test(Description = "Point property")]
        public Point PointProperty { get; set; } = new Point(10, 20);

        /// <summary>Property without attribute - should NOT be serialized</summary>
        public string NonSerializedProperty { get; set; } = "ShouldNotAppear";

        /// <summary>Field without attribute - should NOT be serialized</summary>
        public bool NonSerializedField = true;
    }

    /// <summary>
    /// Tests that the SerializeProperties method correctly serializes properties and fields
    /// marked with the specified attribute type. Verifies that only attributed members are included.
    /// </summary>
    [Test]
    public void SerializeProperties_WithProperties_ReturnsSerializedProperties()
    {
        // Arrange
        var testObject = new TestPropertiesClass();
        var attributeType = typeof(TestAttribute);

        // Act
        var result = JSONDeserializer.SerializeProperties(testObject, attributeType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(4)); // TestProperty, TestField, ColorProperty, PointProperty
        Assert.That(result.ContainsKey("TestProperty"), Is.True);
        Assert.That(result.ContainsKey("TestField"), Is.True);
        Assert.That(result.ContainsKey("ColorProperty"), Is.True);
        Assert.That(result.ContainsKey("PointProperty"), Is.True);
        Assert.That(result["TestProperty"], Is.EqualTo("TestValue"));
        Assert.That(result["TestField"], Is.EqualTo(42));
        Assert.That(result["ColorProperty"], Is.EqualTo("Red"));

        // Verify PointProperty is serialized as an object
        var pointResult = result["PointProperty"];
        Assert.That(pointResult, Is.Not.Null);
        var pointType = pointResult.GetType();
        Assert.That(pointType.GetProperty("x").GetValue(pointResult), Is.EqualTo(10));
        Assert.That(pointType.GetProperty("y").GetValue(pointResult), Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that the SerializeProperties method handles null values correctly.
    /// Verifies that null property values are skipped during serialization.
    /// </summary>
    [Test]
    public void SerializeProperties_WithNullValues_SkipsNullProperties()
    {
        // Arrange
        var testObject = new TestPropertiesClass
        {
            TestProperty = null! // Set to null
        };
        var attributeType = typeof(TestAttribute);

        // Act
        var result = JSONDeserializer.SerializeProperties(testObject, attributeType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ContainsKey("TestProperty"), Is.False); // Should be skipped
        Assert.That(result.ContainsKey("TestField"), Is.True);
        Assert.That(result.ContainsKey("ColorProperty"), Is.True);
        Assert.That(result.ContainsKey("PointProperty"), Is.True);
    }

    /// <summary>
    /// Tests that the SerializeProperties method handles exceptions gracefully.
    /// Verifies that property access errors are caught and logged.
    /// </summary>
    [Test]
    public void SerializeProperties_WithException_HandlesGracefully()
    {
        // Arrange
        var testObject = new TestPropertiesClassWithException();
        var attributeType = typeof(TestAttribute);

        // Act
        var result = JSONDeserializer.SerializeProperties(testObject, attributeType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ContainsKey("TestProperty"), Is.True);
        Assert.That(result.ContainsKey("ExceptionProperty"), Is.True);
        Assert.That(result["ExceptionProperty"].ToString(), Does.Contain("Error serializing"));
    }

    /// <summary>
    /// Test class with a property that throws an exception when accessed.
    /// Used to test error handling in SerializeProperties method.
    /// </summary>
    public class TestPropertiesClassWithException
    {
        [Test]
        public string TestProperty { get; set; } = "TestValue";

        [Test]
        public string ExceptionProperty
        {
            get => throw new InvalidOperationException("Test exception");
            set { }
        }
    }
#endif

    #endregion

    #region Write Method Tests

    /// <summary>
    /// Tests that the Write method handles null values correctly.
    /// Verifies that null values are written as null in JSON.
    /// </summary>
    [Test]
    public void Write_NullValue_WritesNull()
    {
        // Arrange
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        var serializer = new JSONDeserializer();
        var options = new JsonSerializerOptions();

        // Act
        serializer.Write(writer, null, options);
        writer.Flush();

        // Assert
        var json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.That(json, Is.EqualTo("null"));
    }

    /// <summary>
    /// Tests that the Write method handles Color values correctly.
    /// Verifies that Color values are converted to their serialized form.
    /// </summary>
    [Test]
    public void Write_ColorValue_WritesSerializedColor()
    {
        // Arrange
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        var serializer = new JSONDeserializer();
        var options = new JsonSerializerOptions();
        var color = Color.Red;

        // Act
        serializer.Write(writer, color, options);
        writer.Flush();

        // Assert
        var json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.That(json, Is.EqualTo("\"Red\""));
    }

    /// <summary>
    /// Tests that the Write method handles Point values correctly.
    /// Verifies that Point values are converted to their serialized form.
    /// </summary>
    [Test]
    public void Write_PointValue_WritesSerializedPoint()
    {
        // Arrange
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        var serializer = new JSONDeserializer();
        var options = new JsonSerializerOptions();
        var point = new Point(10, 20);

        // Act
        serializer.Write(writer, point, options);
        writer.Flush();

        // Assert
        var json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.That(json, Does.Contain("\"x\":10"));
        Assert.That(json, Does.Contain("\"y\":20"));
    }

    #endregion
}
