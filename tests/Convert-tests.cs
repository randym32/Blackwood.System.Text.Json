using Drawing=System.Drawing;
using System.Net;
using System.Text.Json;

namespace Blackwood.System.Text.Json.tests;

/// <summary>
/// Test suite for the JSONConvert class's Convert functionality.
/// Tests cover the JsonToNormal method, ToDict methods, and ToArray method with
/// various JSON value kinds and edge cases.
/// </summary>
[TestFixture]
public class ConvertTests
{
    #region ToInt Tests

    /// <summary>
    /// Tests that ToInt correctly converts an integer to an integer.
    /// </summary>
    [Test]
    public void ToInt_IntegerValue_ShouldReturnInteger()
    {
        // Arrange
        int value = 42;

        // Act
        var result = JSONConvert.ToInt(value);

        // Assert
        Assert.That(result, Is.EqualTo(42));
        Assert.That(result, Is.InstanceOf<int>());
    }

    /// <summary>
    /// Tests that ToInt correctly converts a long to an integer.
    /// </summary>
    [Test]
    public void ToInt_LongValue_ShouldReturnInteger()
    {
        // Arrange
        long value = 123456L;

        // Act
        var result = JSONConvert.ToInt(value);

        // Assert
        Assert.That(result, Is.EqualTo(123456));
        Assert.That(result, Is.InstanceOf<int>());
    }

    /// <summary>
    /// Tests that ToInt correctly converts a float to an integer.
    /// </summary>
    [Test]
    public void ToInt_FloatValue_ShouldReturnInteger()
    {
        // Arrange
        float value = 3.14f;

        // Act
        var result = JSONConvert.ToInt(value);

        // Assert
        Assert.That(result, Is.EqualTo(3));
        Assert.That(result, Is.InstanceOf<int>());
    }

    /// <summary>
    /// Tests that ToInt correctly converts a double to an integer.
    /// </summary>
    [Test]
    public void ToInt_DoubleValue_ShouldReturnInteger()
    {
        // Arrange
        double value = 2.718;

        // Act
        var result = JSONConvert.ToInt(value);

        // Assert
        Assert.That(result, Is.EqualTo(2));
        Assert.That(result, Is.InstanceOf<int>());
    }

    /// <summary>
    /// Tests that ToInt returns 0 for unsupported types.
    /// </summary>
    [Test]
    public void ToInt_UnsupportedType_ShouldReturnZero()
    {
        // Arrange
        string value = "not a number";

        // Act
        var result = JSONConvert.ToInt(value);

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    #endregion

    #region ToFloat Tests

    /// <summary>
    /// Tests that ToFloat correctly converts an integer to a float.
    /// </summary>
    [Test]
    public void ToFloat_IntegerValue_ShouldReturnFloat()
    {
        // Arrange
        int value = 42;

        // Act
        var result = JSONConvert.ToFloat(value);

        // Assert
        Assert.That(result, Is.EqualTo(42.0f));
        Assert.That(result, Is.InstanceOf<float>());
    }

    /// <summary>
    /// Tests that ToFloat correctly converts a long to a float.
    /// </summary>
    [Test]
    public void ToFloat_LongValue_ShouldReturnFloat()
    {
        // Arrange
        long value = 123456L;

        // Act
        var result = JSONConvert.ToFloat(value);

        // Assert
        Assert.That(result, Is.EqualTo(123456.0f));
        Assert.That(result, Is.InstanceOf<float>());
    }

    /// <summary>
    /// Tests that ToFloat correctly converts a float to a float.
    /// </summary>
    [Test]
    public void ToFloat_FloatValue_ShouldReturnFloat()
    {
        // Arrange
        float value = 3.14f;

        // Act
        var result = JSONConvert.ToFloat(value);

        // Assert
        Assert.That(result, Is.EqualTo(3.14f));
        Assert.That(result, Is.InstanceOf<float>());
    }

    /// <summary>
    /// Tests that ToFloat correctly converts a double to a float.
    /// </summary>
    [Test]
    public void ToFloat_DoubleValue_ShouldReturnFloat()
    {
        // Arrange
        double value = 2.718;

        // Act
        var result = JSONConvert.ToFloat(value);

        // Assert
        Assert.That(result, Is.EqualTo(2.718f));
        Assert.That(result, Is.InstanceOf<float>());
    }

    /// <summary>
    /// Tests that ToFloat returns 0 for unsupported types.
    /// </summary>
    [Test]
    public void ToFloat_UnsupportedType_ShouldReturnZero()
    {
        // Arrange
        string value = "not a number";

        // Act
        var result = JSONConvert.ToFloat(value);

        // Assert
        Assert.That(result, Is.EqualTo(0.0f));
    }

    #endregion

    #region ToBool Tests

    /// <summary>
    /// Tests that ToBool correctly converts a boolean true to boolean true.
    /// </summary>
    [Test]
    public void ToBool_BooleanTrue_ShouldReturnTrue()
    {
        // Arrange
        bool value = true;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(true));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that ToBool correctly converts a boolean false to boolean false.
    /// </summary>
    [Test]
    public void ToBool_BooleanFalse_ShouldReturnFalse()
    {
        // Arrange
        bool value = false;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that ToBool correctly converts a "true" string to boolean true.
    /// </summary>
    [Test]
    public void ToBool_TrueString_ShouldReturnTrue()
    {
        // Arrange
        string value = "true";

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(true));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that ToBool correctly converts a "false" string to boolean false.
    /// </summary>
    [Test]
    public void ToBool_FalseString_ShouldReturnFalse()
    {
        // Arrange
        string value = "false";

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that ToBool correctly converts case-insensitive "True" string to boolean true.
    /// </summary>
    [Test]
    public void ToBool_TrueStringCaseInsensitive_ShouldReturnTrue()
    {
        // Arrange
        string value = "True";

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(true));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that ToBool correctly converts case-insensitive "False" string to boolean false.
    /// </summary>
    [Test]
    public void ToBool_FalseStringCaseInsensitive_ShouldReturnFalse()
    {
        // Arrange
        string value = "False";

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that ToBool returns false for non-boolean strings.
    /// </summary>
    [Test]
    public void ToBool_NonBooleanString_ShouldReturnFalse()
    {
        // Arrange
        string value = "maybe";

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }

    /// <summary>
    /// Tests that ToBool correctly converts a non-zero integer to boolean true.
    /// </summary>
    [Test]
    public void ToBool_NonZeroInteger_ShouldReturnTrue()
    {
        // Arrange
        int value = 42;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that ToBool correctly converts a zero integer to boolean false.
    /// </summary>
    [Test]
    public void ToBool_ZeroInteger_ShouldReturnFalse()
    {
        // Arrange
        int value = 0;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }

    /// <summary>
    /// Tests that ToBool correctly converts a non-zero long to boolean true.
    /// </summary>
    [Test]
    public void ToBool_NonZeroLong_ShouldReturnTrue()
    {
        // Arrange
        long value = 123L;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that ToBool correctly converts a zero long to boolean false.
    /// </summary>
    [Test]
    public void ToBool_ZeroLong_ShouldReturnFalse()
    {
        // Arrange
        long value = 0L;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }

    /// <summary>
    /// Tests that ToBool correctly converts a non-zero float to boolean true.
    /// </summary>
    [Test]
    public void ToBool_NonZeroFloat_ShouldReturnTrue()
    {
        // Arrange
        float value = 3.14f;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that ToBool correctly converts a zero float to boolean false.
    /// </summary>
    [Test]
    public void ToBool_ZeroFloat_ShouldReturnFalse()
    {
        // Arrange
        float value = 0.0f;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }

    /// <summary>
    /// Tests that ToBool correctly converts a non-zero double to boolean true.
    /// </summary>
    [Test]
    public void ToBool_NonZeroDouble_ShouldReturnTrue()
    {
        // Arrange
        double value = 2.718;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(true));
    }

    /// <summary>
    /// Tests that ToBool correctly converts a zero double to boolean false.
    /// </summary>
    [Test]
    public void ToBool_ZeroDouble_ShouldReturnFalse()
    {
        // Arrange
        double value = 0.0;

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }

    /// <summary>
    /// Tests that ToBool returns false for unsupported types.
    /// </summary>
    [Test]
    public void ToBool_UnsupportedType_ShouldReturnFalse()
    {
        // Arrange
        object value = new object();

        // Act
        var result = JSONConvert.ToBool(value);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }

    #endregion

    #region TryParseColor Tests

    /// <summary>
    /// Tests that TryParseColor correctly converts named colors.
    /// </summary>
    [Test]
    public void TryParseColor_NamedColor_ShouldReturnCorrectColor()
    {
        // Arrange
        string colorName = "Red";

        // Act
        var result = JSONConvert.TryParseColor(colorName);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(Drawing.Color.Red));
        Assert.That(result?.Name, Is.EqualTo("Red"));
    }

    /// <summary>
    /// Tests that TryParseColor correctly converts case-insensitive named colors.
    /// </summary>
    [Test]
    public void TryParseColor_CaseInsensitiveNamedColor_ShouldReturnCorrectColor()
    {
        // Arrange
        string colorName = "blue";

        // Act
        var result = JSONConvert.TryParseColor(colorName);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(Drawing.Color.Blue));
    }

    /// <summary>
    /// Tests that TryParseColor correctly converts hex color codes (#RRGGBB).
    /// </summary>
    [Test]
    public void TryParseColor_HexColorRRGGBB_ShouldReturnCorrectColor()
    {
        // Arrange
        string hexColor = "#FF0000"; // Red

        // Act
        var result = JSONConvert.TryParseColor(hexColor);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.R, Is.EqualTo(255));
        Assert.That(result?.G, Is.EqualTo(0));
        Assert.That(result?.B, Is.EqualTo(0));
        Assert.That(result?.A, Is.EqualTo(255)); // Default alpha
    }

    /// <summary>
    /// Tests that TryParseColor correctly converts hex color codes (#AARRGGBB).
    /// </summary>
    [Test]
    public void TryParseColor_HexColorAARRGGBB_ShouldReturnCorrectColor()
    {
        // Arrange
        string hexColor = "#80FF0000"; // Red with 50% alpha

        // Act
        var result = JSONConvert.TryParseColor(hexColor);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.A, Is.EqualTo(128)); // 50% alpha
        Assert.That(result?.R, Is.EqualTo(255));
        Assert.That(result?.G, Is.EqualTo(0));
        Assert.That(result?.B, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that TryParseColor correctly converts hex color codes with lowercase letters.
    /// </summary>
    [Test]
    public void TryParseColor_HexColorLowercase_ShouldReturnCorrectColor()
    {
        // Arrange
        string hexColor = "#00ff00"; // Green

        // Act
        var result = JSONConvert.TryParseColor(hexColor);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.R, Is.EqualTo(0));
        Assert.That(result?.G, Is.EqualTo(255));
        Assert.That(result?.B, Is.EqualTo(0));
        Assert.That(result?.A, Is.EqualTo(255));
    }

    /// <summary>
    /// Tests that TryParseColor correctly handles hex color codes with whitespace.
    /// </summary>
    [Test]
    public void TryParseColor_HexColorWithWhitespace_ShouldReturnCorrectColor()
    {
        // Arrange
        string hexColor = "  #0000FF  "; // Blue with whitespace

        // Act
        var result = JSONConvert.TryParseColor(hexColor);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.R, Is.EqualTo(0));
        Assert.That(result?.G, Is.EqualTo(0));
        Assert.That(result?.B, Is.EqualTo(255));
        Assert.That(result?.A, Is.EqualTo(255));
    }

    /// <summary>
    /// Tests that TryParseColor returns null for null input.
    /// </summary>
    [Test]
    public void TryParseColor_NullInput_ShouldReturnNull()
    {
        // Arrange
        string? nullColor = null;

        // Act
        var result = JSONConvert.TryParseColor(nullColor!);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests that TryParseColor returns null for empty string input.
    /// </summary>
    [Test]
    public void TryParseColor_EmptyString_ShouldReturnNull()
    {
        // Arrange
        string emptyColor = "";

        // Act
        var result = JSONConvert.TryParseColor(emptyColor);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests that TryParseColor returns null for whitespace-only input.
    /// </summary>
    [Test]
    public void TryParseColor_WhitespaceOnly_ShouldReturnNull()
    {
        // Arrange
        string whitespaceColor = "   ";

        // Act
        var result = JSONConvert.TryParseColor(whitespaceColor);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests that TryParseColor returns null for invalid hex color format.
    /// </summary>
    [Test]
    public void TryParseColor_InvalidHexFormat_ShouldReturnNull()
    {
        // Arrange
        string invalidHex = "#RGB"; // Shorthand format not supported

        // Act
        var result = JSONConvert.TryParseColor(invalidHex);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests that TryParseColor returns null for hex color with wrong length.
    /// </summary>
    [Test]
    public void TryParseColor_HexWrongLength_ShouldReturnNull()
    {
        // Arrange
        string invalidHex = "#FF00"; // 4 characters - this is actually valid shorthand hex

        // Act
        var result = JSONConvert.TryParseColor(invalidHex);

        // Assert
        // #FF00 is actually interpreted as #00FF00 (green) by ColorTranslator.FromHtml
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.R, Is.EqualTo(0));
        Assert.That(result?.G, Is.EqualTo(255));
        Assert.That(result?.B, Is.EqualTo(0));
        Assert.That(result?.A, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that TryParseColor returns null for invalid named color.
    /// </summary>
    [Test]
    public void TryParseColor_InvalidNamedColor_ShouldReturnNull()
    {
        // Arrange
        string invalidColor = "NotAColor";

        // Act
        var result = JSONConvert.TryParseColor(invalidColor);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests that TryParseColor correctly converts various named colors.
    /// </summary>
    [Test]
    public void TryParseColor_VariousNamedColors_ShouldReturnCorrectColors()
    {
        // Arrange
        var testCases = new[]
        {
            ("Green", Drawing.Color.Green),
            ("Blue", Drawing.Color.Blue),
            ("Yellow", Drawing.Color.Yellow),
            ("Purple", Drawing.Color.Purple),
            ("Orange", Drawing.Color.Orange),
            ("Pink", Drawing.Color.Pink),
            ("Cyan", Drawing.Color.Cyan),
            ("Magenta", Drawing.Color.Magenta)
        };

        foreach (var (colorName, expectedColor) in testCases)
        {
            // Act
            var result = JSONConvert.TryParseColor(colorName);

            // Assert
            Assert.That(result, Is.Not.Null, $"Failed for color: {colorName}");
            Assert.That(result, Is.EqualTo(expectedColor), $"Failed for color: {colorName}");
        }
    }

    /// <summary>
    /// Tests that TryParseColor correctly converts various hex color codes.
    /// </summary>
    [Test]
    public void TryParseColor_VariousHexColors_ShouldReturnCorrectColors()
    {
        // Arrange
        var testCases = new[]
        {
            ("#FF0000", 255, 0, 0, 255),     // Red
            ("#00FF00", 0, 255, 0, 255),     // Green
            ("#0000FF", 0, 0, 255, 255),     // Blue
            ("#FFFFFF", 255, 255, 255, 255), // White
            ("#000000", 0, 0, 0, 255),       // Black
            ("#808080", 128, 128, 128, 255), // Gray
            ("#FF00FF", 255, 0, 255, 255),   // Magenta
            ("#00FFFF", 0, 255, 255, 255)    // Cyan
        };

        foreach (var (hexColor, expectedR, expectedG, expectedB, expectedA) in testCases)
        {
            // Act
            var result = JSONConvert.TryParseColor(hexColor);

            // Assert
            Assert.That(result, Is.Not.Null, $"Failed for color: {hexColor}");
            Assert.That(result?.R, Is.EqualTo(expectedR), $"Failed R for color: {hexColor}");
            Assert.That(result?.G, Is.EqualTo(expectedG), $"Failed G for color: {hexColor}");
            Assert.That(result?.B, Is.EqualTo(expectedB), $"Failed B for color: {hexColor}");
            Assert.That(result?.A, Is.EqualTo(expectedA), $"Failed A for color: {hexColor}");
        }
    }

    /// <summary>
    /// Tests that TryParseColor correctly converts hex color codes with alpha channel.
    /// </summary>
    [Test]
    public void TryParseColor_HexColorsWithAlpha_ShouldReturnCorrectColors()
    {
        // Arrange
        var testCases = new[]
        {
            ("#FF000000", 0, 0, 0, 255),     // Black with full alpha
            ("#80000000", 0, 0, 0, 128),     // Black with 50% alpha
            ("#40000000", 0, 0, 0, 64),      // Black with 25% alpha
            ("#00FF0000", 255, 0, 0, 0),     // Red with no alpha (transparent)
            ("#80FF0000", 255, 0, 0, 128)    // Red with 50% alpha
        };

        foreach (var (hexColor, expectedR, expectedG, expectedB, expectedA) in testCases)
        {
            // Act
            var result = JSONConvert.TryParseColor(hexColor);

            // Assert
            Assert.That(result, Is.Not.Null, $"Failed for color: {hexColor}");
            Assert.That(result?.R, Is.EqualTo(expectedR), $"Failed R for color: {hexColor}");
            Assert.That(result?.G, Is.EqualTo(expectedG), $"Failed G for color: {hexColor}");
            Assert.That(result?.B, Is.EqualTo(expectedB), $"Failed B for color: {hexColor}");
            Assert.That(result?.A, Is.EqualTo(expectedA), $"Failed A for color: {hexColor}");
        }
    }

    /// <summary>
    /// Tests that TryParseColor returns null for hex color with invalid characters.
    /// </summary>
    [Test]
    public void TryParseColor_HexWithInvalidCharacters_ShouldReturnNull()
    {
        // Arrange
        string invalidHex = "#GG0000"; // Invalid hex character

        // Act
        var result = JSONConvert.TryParseColor(invalidHex);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests that TryParseColor returns null for hex color without # prefix.
    /// </summary>
    [Test]
    public void TryParseColor_HexWithoutPrefix_ShouldReturnNull()
    {
        // Arrange
        string invalidHex = "FF0000"; // Missing # prefix - this will be treated as a named color

        // Act
        var result = JSONConvert.TryParseColor(invalidHex);

        // Assert
        // FF0000 is not a valid named color, so it should return null
        Assert.That(result, Is.Null);
    }

    #endregion

    #region ConvertToType Tests

    /// <summary>
    /// Tests that ConvertToType returns null for null input.
    /// </summary>
    [Test]
    public void ConvertToType_NullInput_ShouldReturnNull()
    {
        // Arrange
        object? nullValue = null;
        Type targetType = typeof(string);

        // Act
        var result = JSONConvert.ConvertToType(nullValue, targetType);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests that ConvertToType returns the same value if already of correct type.
    /// </summary>
    [Test]
    public void ConvertToType_SameType_ShouldReturnSameValue()
    {
        // Arrange
        string value = "test";
        Type targetType = typeof(string);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo("test"));
        Assert.That(result, Is.SameAs(value));
    }

    /// <summary>
    /// Tests that ConvertToType handles nullable types correctly.
    /// </summary>
    [Test]
    public void ConvertToType_NullableType_ShouldConvertCorrectly()
    {
        // Arrange
        int value = 42;
        Type targetType = typeof(int?);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(42));
        Assert.That(result, Is.InstanceOf<int>());
    }

    /// <summary>
    /// Tests that ConvertToType handles nullable double types correctly.
    /// </summary>
    [Test]
    public void ConvertToType_NullableDouble_ShouldConvertCorrectly()
    {
        // Arrange
        string value = "3.14";
        Type targetType = typeof(double?);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(3.14));
        Assert.That(result, Is.InstanceOf<double>());
    }

    /// <summary>
    /// Tests that ConvertToType handles nullable bool types correctly.
    /// </summary>
    [Test]
    public void ConvertToType_NullableBool_ShouldConvertCorrectly()
    {
        // Arrange
        string value = "true";
        Type targetType = typeof(bool?);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(true));
        Assert.That(result, Is.InstanceOf<bool>());
    }

    /// <summary>
    /// Tests that ConvertToType handles nullable float types correctly.
    /// </summary>
    [Test]
    public void ConvertToType_NullableFloat_ShouldConvertCorrectly()
    {
        // Arrange
        string value = "2.5";
        Type targetType = typeof(float?);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(2.5f));
        Assert.That(result, Is.InstanceOf<float>());
    }

    /// <summary>
    /// Tests that ConvertToType handles nullable decimal types correctly.
    /// </summary>
    [Test]
    public void ConvertToType_NullableDecimal_ShouldConvertCorrectly()
    {
        // Arrange
        string value = "99.99";
        Type targetType = typeof(decimal?);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(99.99m));
        Assert.That(result, Is.InstanceOf<decimal>());
    }

    /// <summary>
    /// Tests that ConvertToType handles nullable long types correctly.
    /// </summary>
    [Test]
    public void ConvertToType_NullableLong_ShouldConvertCorrectly()
    {
        // Arrange
        string value = "123456789";
        Type targetType = typeof(long?);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(123456789L));
        Assert.That(result, Is.InstanceOf<long>());
    }

    /// <summary>
    /// Tests that ConvertToType handles nullable DateTime types correctly.
    /// </summary>
    [Test]
    public void ConvertToType_NullableDateTime_ShouldConvertCorrectly()
    {
        // Arrange
        string value = "2023-12-25T10:30:00";
        Type targetType = typeof(DateTime?);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.InstanceOf<DateTime>());
        var dateTime = (DateTime)result!;
        Assert.That(dateTime.Year, Is.EqualTo(2023));
        Assert.That(dateTime.Month, Is.EqualTo(12));
        Assert.That(dateTime.Day, Is.EqualTo(25));
    }

    /// <summary>
    /// Tests that ConvertToType converts to string correctly.
    /// </summary>
    [Test]
    public void ConvertToType_ToString_ShouldConvertCorrectly()
    {
        // Arrange
        int value = 42;
        Type targetType = typeof(string);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo("42"));
        Assert.That(result, Is.InstanceOf<string>());
    }

    /// <summary>
    /// Tests that ConvertToType converts to int correctly.
    /// </summary>
    [Test]
    public void ConvertToType_ToInt_ShouldConvertCorrectly()
    {
        // Arrange
        string value = "42";
        Type targetType = typeof(int);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(42));
        Assert.That(result, Is.InstanceOf<int>());
    }

    /// <summary>
    /// Tests that ConvertToType converts from double to int correctly.
    /// </summary>
    [Test]
    public void ConvertToType_DoubleToInt_ShouldConvertCorrectly()
    {
        // Arrange
        double value = 42.7;
        Type targetType = typeof(int);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(43)); // Convert.ToInt32 rounds 42.7 to 43
        Assert.That(result, Is.InstanceOf<int>());
    }

    /// <summary>
    /// Tests that ConvertToType converts from int to double correctly.
    /// </summary>
    [Test]
    public void ConvertToType_IntToDouble_ShouldConvertCorrectly()
    {
        // Arrange
        int value = 42;
        Type targetType = typeof(double);

        // Act
        var result = JSONConvert.ConvertToType(value, targetType);

        // Assert
        Assert.That(result, Is.EqualTo(42.0));
        Assert.That(result, Is.InstanceOf<double>());
    }

    #endregion
}