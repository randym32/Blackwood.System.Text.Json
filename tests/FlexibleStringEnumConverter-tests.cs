using System.Text.Json;
using System.Text.Json.Serialization;
using Blackwood;

namespace Blackwood.System.Text.Json.tests;

/// <summary>
/// Tests for <see cref="FlexibleStringEnumConverter{TEnum}"/>.
/// </summary>
[TestFixture]
public class FlexibleStringEnumConverterTests
{
    public enum FalloffMode
    {
        None,
        InverseSquare,
    }

    public enum SnakeCaseMemberEnum
    {
        inverse_square,
        none,
    }

    private static JsonSerializerOptions Options<TEnum>()
        where TEnum : struct, Enum =>
        new() { Converters = { new FlexibleStringEnumConverter<TEnum>() } };

    [TestCase("\"InverseSquare\"", FalloffMode.InverseSquare)]
    [TestCase("\"inverseSquare\"", FalloffMode.InverseSquare)]
    [TestCase("\"inverse_square\"", FalloffMode.InverseSquare)]
    [TestCase("\"INVERSE_SQUARE\"", FalloffMode.InverseSquare)]
    [TestCase("\"None\"", FalloffMode.None)]
    public void Deserialize_AcceptsFlexibleNaming(string json, FalloffMode expected)
    {
        var result = JsonSerializer.Deserialize<FalloffMode>(json, Options<FalloffMode>());
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Serialize_WritesClrMemberName()
    {
        var json = JsonSerializer.Serialize(FalloffMode.InverseSquare, Options<FalloffMode>());
        Assert.That(json, Is.EqualTo("\"InverseSquare\""));
    }

    [Test]
    public void Serialize_None_WritesPascalCaseNotSnakeCase()
    {
        var json = JsonSerializer.Serialize(FalloffMode.None, Options<FalloffMode>());
        Assert.That(json, Is.EqualTo("\"None\""));
    }

    [TestCase("inverseSquare", SnakeCaseMemberEnum.inverse_square)]
    [TestCase("InverseSquare", SnakeCaseMemberEnum.inverse_square)]
    public void Deserialize_CamelCaseJsonMatchesSnakeCaseMember(string jsonValue, SnakeCaseMemberEnum expected)
    {
        var result = JsonSerializer.Deserialize<SnakeCaseMemberEnum>($"\"{jsonValue}\"", Options<SnakeCaseMemberEnum>());
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Deserialize_UnknownValue_ThrowsJsonException()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<FalloffMode>("\"not_a_member\"", Options<FalloffMode>()));
    }

    [Test]
    public void Deserialize_NonStringToken_ThrowsJsonException()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<FalloffMode>("42", Options<FalloffMode>()));
    }

    [TestCase("inverse_square", "InverseSquare")]
    [TestCase("plain", "plain")]
    public void SnakeCaseToPascalCase_ConvertsExpectedly(string input, string expected)
    {
        Assert.That(
            FlexibleStringEnumConverter<FalloffMode>.SnakeCaseToPascalCase(input),
            Is.EqualTo(expected));
    }

    [TestCase("inverseSquare", "inverse_square")]
    [TestCase("InverseSquare", "inverse_square")]
    [TestCase("inverse_square", "inverse_square")]
    [TestCase("", "")]
    public void CamelCaseToSnakeCase_ConvertsExpectedly(string input, string expected)
    {
        Assert.That(
            FlexibleStringEnumConverter<FalloffMode>.CamelCaseToSnakeCase(input),
            Is.EqualTo(expected));
    }
}
