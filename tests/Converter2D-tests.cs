using System.Drawing;
using System.Text.Json;
using NUnit.Framework;

namespace Blackwood.System.Text.Json.tests;

/// <summary>
/// Test suite for the Converter2D class.
/// Tests cover serialization and deserialization of 2D types (Point, PointF, Size, SizeF, Rectangle, RectangleF) to and from JSON.
/// </summary>
[TestFixture]
public class Converter2DTests
{
    private JsonSerializerOptions _options;

    [SetUp]
    public void SetUp()
    {
        _options = new JsonSerializerOptions
        {
            Converters = {
                new Converter2D<Point>(),
                new Converter2D<PointF>(),
                new Converter2D<Size>(),
                new Converter2D<SizeF>(),
                new Converter2D<Rectangle>(),
                new Converter2D<RectangleF>()
            }
        };
    }

    #region Point Tests

    /// <summary>
    /// Tests that a Point is serialized to JSON correctly.
    /// </summary>
    [Test]
    public void Serialize_Point_ReturnsCorrectJson()
    {
        // Arrange
        var point = new Point(10, 20);

        // Act
        var json = JsonSerializer.Serialize(point, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":10,\"y\":20}"));
    }

    /// <summary>
    /// Tests that a Point is deserialized from JSON correctly.
    /// </summary>
    [Test]
    public void Deserialize_Point_ReturnsCorrectPoint()
    {
        // Arrange
        var json = "{\"x\":10,\"y\":20}";

        // Act
        var point = JsonSerializer.Deserialize<Point>(json, _options);

        // Assert
        Assert.That(point.X, Is.EqualTo(10));
        Assert.That(point.Y, Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that Point.Empty is serialized correctly.
    /// </summary>
    [Test]
    public void Serialize_PointEmpty_ReturnsCorrectJson()
    {
        // Arrange
        var point = Point.Empty;

        // Act
        var json = JsonSerializer.Serialize(point, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":0,\"y\":0}"));
    }

    #endregion

    #region PointF Tests

    /// <summary>
    /// Tests that a PointF is serialized to JSON correctly.
    /// </summary>
    [Test]
    public void Serialize_PointF_ReturnsCorrectJson()
    {
        // Arrange
        var pointF = new PointF(10.5f, 20.75f);

        // Act
        var json = JsonSerializer.Serialize(pointF, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":10.5,\"y\":20.75}"));
    }

    /// <summary>
    /// Tests that a PointF is deserialized from JSON correctly.
    /// </summary>
    [Test]
    public void Deserialize_PointF_ReturnsCorrectPointF()
    {
        // Arrange
        var json = "{\"x\":10.5,\"y\":20.75}";

        // Act
        var pointF = JsonSerializer.Deserialize<PointF>(json, _options);

        // Assert
        Assert.That(pointF.X, Is.EqualTo(10.5f).Within(0.001f));
        Assert.That(pointF.Y, Is.EqualTo(20.75f).Within(0.001f));
    }


    #endregion

    #region Size Tests

    /// <summary>
    /// Tests that a Size is serialized to JSON correctly.
    /// </summary>
    [Test]
    public void Serialize_Size_ReturnsCorrectJson()
    {
        // Arrange
        var size = new Size(100, 200);

        // Act
        var json = JsonSerializer.Serialize(size, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"width\":100,\"height\":200}"));
    }

    /// <summary>
    /// Tests that a Size is deserialized from JSON correctly.
    /// </summary>
    [Test]
    public void Deserialize_Size_ReturnsCorrectSize()
    {
        // Arrange
        var json = "{\"width\":100,\"height\":200}";

        // Act
        var size = JsonSerializer.Deserialize<Size>(json, _options);

        // Assert
        Assert.That(size.Width, Is.EqualTo(100));
        Assert.That(size.Height, Is.EqualTo(200));
    }

    /// <summary>
    /// Tests that Size.Empty is serialized correctly.
    /// </summary>
    [Test]
    public void Serialize_SizeEmpty_ReturnsCorrectJson()
    {
        // Arrange
        var size = Size.Empty;

        // Act
        var json = JsonSerializer.Serialize(size, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"width\":0,\"height\":0}"));
    }

    #endregion

    #region SizeF Tests

    /// <summary>
    /// Tests that a SizeF is serialized to JSON correctly.
    /// </summary>
    [Test]
    public void Serialize_SizeF_ReturnsCorrectJson()
    {
        // Arrange
        var sizeF = new SizeF(100.5f, 200.75f);

        // Act
        var json = JsonSerializer.Serialize(sizeF, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"width\":100.5,\"height\":200.75}"));
    }

    /// <summary>
    /// Tests that a SizeF is deserialized from JSON correctly.
    /// </summary>
    [Test]
    public void Deserialize_SizeF_ReturnsCorrectSizeF()
    {
        // Arrange
        var json = "{\"width\":100.5,\"height\":200.75}";

        // Act
        var sizeF = JsonSerializer.Deserialize<SizeF>(json, _options);

        // Assert
        Assert.That(sizeF.Width, Is.EqualTo(100.5f).Within(0.001f));
        Assert.That(sizeF.Height, Is.EqualTo(200.75f).Within(0.001f));
    }

    #endregion

    #region Rectangle Tests

    /// <summary>
    /// Tests that a Rectangle is serialized to JSON correctly.
    /// </summary>
    [Test]
    public void Serialize_Rectangle_ReturnsCorrectJson()
    {
        // Arrange
        var rectangle = new Rectangle(10, 20, 100, 200);

        // Act
        var json = JsonSerializer.Serialize(rectangle, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":10,\"y\":20,\"width\":100,\"height\":200}"));
    }

    /// <summary>
    /// Tests that a Rectangle is deserialized from JSON correctly.
    /// </summary>
    [Test]
    public void Deserialize_Rectangle_ReturnsCorrectRectangle()
    {
        // Arrange
        var json = "{\"x\":10,\"y\":20,\"width\":100,\"height\":200}";

        // Act
        var rectangle = JsonSerializer.Deserialize<Rectangle>(json, _options);

        // Assert
        Assert.That(rectangle.X, Is.EqualTo(10));
        Assert.That(rectangle.Y, Is.EqualTo(20));
        Assert.That(rectangle.Width, Is.EqualTo(100));
        Assert.That(rectangle.Height, Is.EqualTo(200));
    }

    /// <summary>
    /// Tests that Rectangle.Empty is serialized correctly.
    /// </summary>
    [Test]
    public void Serialize_RectangleEmpty_ReturnsCorrectJson()
    {
        // Arrange
        var rectangle = Rectangle.Empty;

        // Act
        var json = JsonSerializer.Serialize(rectangle, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":0,\"y\":0,\"width\":0,\"height\":0}"));
    }

    #endregion

    #region RectangleF Tests

    /// <summary>
    /// Tests that a RectangleF is serialized to JSON correctly.
    /// </summary>
    [Test]
    public void Serialize_RectangleF_ReturnsCorrectJson()
    {
        // Arrange
        var rectangleF = new RectangleF(10.5f, 20.75f, 100.25f, 200.5f);

        // Act
        var json = JsonSerializer.Serialize(rectangleF, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":10.5,\"y\":20.75,\"width\":100.25,\"height\":200.5}"));
    }

    /// <summary>
    /// Tests that a RectangleF is deserialized from JSON correctly.
    /// </summary>
    [Test]
    public void Deserialize_RectangleF_ReturnsCorrectRectangleF()
    {
        // Arrange
        var json = "{\"x\":10.5,\"y\":20.75,\"width\":100.25,\"height\":200.5}";

        // Act
        var rectangleF = JsonSerializer.Deserialize<RectangleF>(json, _options);

        // Assert
        Assert.That(rectangleF.X, Is.EqualTo(10.5f).Within(0.001f));
        Assert.That(rectangleF.Y, Is.EqualTo(20.75f).Within(0.001f));
        Assert.That(rectangleF.Width, Is.EqualTo(100.25f).Within(0.001f));
        Assert.That(rectangleF.Height, Is.EqualTo(200.5f).Within(0.001f));
    }

    #endregion

    #region Edge Cases and Error Handling

    /// <summary>
    /// Tests that deserializing invalid JSON handles gracefully.
    /// </summary>
    [Test]
    public void Deserialize_InvalidJson_HandlesGracefully()
    {
        // Arrange
        var invalidJson = "{\"x\":\"invalid\",\"y\":20}";

        // Act
        var point = JsonSerializer.Deserialize<Point>(invalidJson, _options);

        // Assert
        // The Converter2D handles invalid string values gracefully by converting to 0
        Assert.That(point, Is.Not.Null);
        Assert.That(point.X, Is.EqualTo(0)); // Invalid string converts to 0
        Assert.That(point.Y, Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that deserializing non-object JSON throws appropriate exception.
    /// </summary>
    [Test]
    public void Deserialize_NonObjectJson_ThrowsJsonException()
    {
        // Arrange
        var invalidJson = "\"not an object\"";

        // Act & Assert
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Point>(invalidJson, _options));
    }

    /// <summary>
    /// Tests that deserializing array JSON throws appropriate exception.
    /// </summary>
    [Test]
    public void Deserialize_ArrayJson_ThrowsJsonException()
    {
        // Arrange
        var invalidJson = "[10, 20]";

        // Act & Assert
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Point>(invalidJson, _options));
    }

    /// <summary>
    /// Tests that deserializing with case-insensitive properties works correctly.
    /// </summary>
    [Test]
    public void Deserialize_PointWithCaseInsensitiveProperties_WorksCorrectly()
    {
        // Arrange
        var json = "{\"X\":10,\"Y\":20}";

        // Act
        var result = JsonSerializer.Deserialize<Point>(json, _options);

        // Assert
        Assert.That(result.X, Is.EqualTo(10));
        Assert.That(result.Y, Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that deserializing with mixed case properties works correctly.
    /// </summary>
    [Test]
    public void Deserialize_PointWithMixedCaseProperties_WorksCorrectly()
    {
        // Arrange
        var json = "{\"X\":10,\"y\":20}";

        // Act
        var result = JsonSerializer.Deserialize<Point>(json, _options);

        // Assert
        Assert.That(result.X, Is.EqualTo(10));
        Assert.That(result.Y, Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that deserializing with missing properties returns default values.
    /// </summary>
    [Test]
    public void Deserialize_PointWithMissingProperties_ReturnsDefaultValues()
    {
        // Arrange
        var json = "{}";

        // Act
        var point = JsonSerializer.Deserialize<Point>(json, _options);

        // Assert
        // The Converter2D handles missing properties by returning default values
        Assert.That(point, Is.Not.Null);
        Assert.That(point.X, Is.EqualTo(0));
        Assert.That(point.Y, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests that deserializing with only x property throws exception.
    /// </summary>
    [Test]
    public void Deserialize_PointWithOnlyXProperty_ThrowsException()
    {
        // Arrange
        var json = "{\"x\":10}";

        // Act & Assert
        // The Converter2D requires both x and y properties to be present
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Point>(json, _options));
    }

    /// <summary>
    /// Tests that deserializing with only y property throws exception.
    /// </summary>
    [Test]
    public void Deserialize_PointWithOnlyYProperty_ThrowsException()
    {
        // Arrange
        var json = "{\"y\":20}";

        // Act & Assert
        // The Converter2D requires both x and y properties to be present
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Point>(json, _options));
    }

    #endregion

    #region Extreme Values Tests

    /// <summary>
    /// Tests that extreme values are serialized correctly for Point.
    /// </summary>
    [Test]
    public void Serialize_PointWithExtremeValues_ReturnsCorrectJson()
    {
        // Arrange
        var point = new Point(int.MaxValue, int.MinValue);

        // Act
        var json = JsonSerializer.Serialize(point, _options);

        // Assert
        Assert.That(json, Is.EqualTo($"{{\"x\":{int.MaxValue},\"y\":{int.MinValue}}}"));
    }

    /// <summary>
    /// Tests that extreme values are serialized correctly for PointF.
    /// </summary>
    [Test]
    public void Serialize_PointFWithExtremeValues_ReturnsCorrectJson()
    {
        // Arrange
        var pointF = new PointF(float.MaxValue, float.MinValue);

        // Act
        var json = JsonSerializer.Serialize(pointF, _options);

        // Assert
        Assert.That(json, Is.EqualTo($"{{\"x\":{float.MaxValue},\"y\":{float.MinValue}}}"));
    }

    /// <summary>
    /// Tests that extreme values are serialized correctly for Size.
    /// </summary>
    [Test]
    public void Serialize_SizeWithExtremeValues_ReturnsCorrectJson()
    {
        // Arrange
        var size = new Size(int.MaxValue, int.MinValue);

        // Act
        var json = JsonSerializer.Serialize(size, _options);

        // Assert
        Assert.That(json, Is.EqualTo($"{{\"width\":{int.MaxValue},\"height\":{int.MinValue}}}"));
    }

    /// <summary>
    /// Tests that extreme values are serialized correctly for SizeF.
    /// </summary>
    [Test]
    public void Serialize_SizeFWithExtremeValues_ReturnsCorrectJson()
    {
        // Arrange
        var sizeF = new SizeF(float.MaxValue, float.MinValue);

        // Act
        var json = JsonSerializer.Serialize(sizeF, _options);

        // Assert
        Assert.That(json, Is.EqualTo($"{{\"width\":{float.MaxValue},\"height\":{float.MinValue}}}"));
    }

    /// <summary>
    /// Tests that extreme values are serialized correctly for Rectangle.
    /// </summary>
    [Test]
    public void Serialize_RectangleWithExtremeValues_ReturnsCorrectJson()
    {
        // Arrange
        var rectangle = new Rectangle(int.MaxValue, int.MinValue, int.MaxValue, int.MinValue);

        // Act
        var json = JsonSerializer.Serialize(rectangle, _options);

        // Assert
        Assert.That(json, Is.EqualTo($"{{\"x\":{int.MaxValue},\"y\":{int.MinValue},\"width\":{int.MaxValue},\"height\":{int.MinValue}}}"));
    }

    /// <summary>
    /// Tests that extreme values are serialized correctly for RectangleF.
    /// </summary>
    [Test]
    public void Serialize_RectangleFWithExtremeValues_ReturnsCorrectJson()
    {
        // Arrange
        var rectangleF = new RectangleF(float.MaxValue, float.MinValue, float.MaxValue, float.MinValue);

        // Act
        var json = JsonSerializer.Serialize(rectangleF, _options);

        // Assert
        Assert.That(json, Is.EqualTo($"{{\"x\":{float.MaxValue},\"y\":{float.MinValue},\"width\":{float.MaxValue},\"height\":{float.MinValue}}}"));
    }

    #endregion

    #region Negative Values Tests

    /// <summary>
    /// Tests that negative values are serialized correctly for Point.
    /// </summary>
    [Test]
    public void Serialize_PointWithNegativeValues_ReturnsCorrectJson()
    {
        // Arrange
        var point = new Point(-10, -20);

        // Act
        var json = JsonSerializer.Serialize(point, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":-10,\"y\":-20}"));
    }

    /// <summary>
    /// Tests that negative values are serialized correctly for PointF.
    /// </summary>
    [Test]
    public void Serialize_PointFWithNegativeValues_ReturnsCorrectJson()
    {
        // Arrange
        var pointF = new PointF(-10.5f, -20.75f);

        // Act
        var json = JsonSerializer.Serialize(pointF, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":-10.5,\"y\":-20.75}"));
    }

    /// <summary>
    /// Tests that negative values are serialized correctly for Size.
    /// </summary>
    [Test]
    public void Serialize_SizeWithNegativeValues_ReturnsCorrectJson()
    {
        // Arrange
        var size = new Size(-100, -200);

        // Act
        var json = JsonSerializer.Serialize(size, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"width\":-100,\"height\":-200}"));
    }

    /// <summary>
    /// Tests that negative values are serialized correctly for SizeF.
    /// </summary>
    [Test]
    public void Serialize_SizeFWithNegativeValues_ReturnsCorrectJson()
    {
        // Arrange
        var sizeF = new SizeF(-100.5f, -200.75f);

        // Act
        var json = JsonSerializer.Serialize(sizeF, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"width\":-100.5,\"height\":-200.75}"));
    }

    /// <summary>
    /// Tests that negative values are serialized correctly for Rectangle.
    /// </summary>
    [Test]
    public void Serialize_RectangleWithNegativeValues_ReturnsCorrectJson()
    {
        // Arrange
        var rectangle = new Rectangle(-10, -20, -100, -200);

        // Act
        var json = JsonSerializer.Serialize(rectangle, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":-10,\"y\":-20,\"width\":-100,\"height\":-200}"));
    }

    /// <summary>
    /// Tests that negative values are serialized correctly for RectangleF.
    /// </summary>
    [Test]
    public void Serialize_RectangleFWithNegativeValues_ReturnsCorrectJson()
    {
        // Arrange
        var rectangleF = new RectangleF(-10.5f, -20.75f, -100.25f, -200.5f);

        // Act
        var json = JsonSerializer.Serialize(rectangleF, _options);

        // Assert
        Assert.That(json, Is.EqualTo("{\"x\":-10.5,\"y\":-20.75,\"width\":-100.25,\"height\":-200.5}"));
    }

    #endregion
}
