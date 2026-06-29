using System.Text.Json;
using Blackwood;

namespace Blackwood.System.Text.Json.tests;

[TestFixture]
public class JsonElementExtensionsTests
{
    static JsonElement Parse(string json)
    {
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.Clone();
    }

    [TestCase("OutputColorTransforms")]
    [TestCase("outputColorTransforms")]
    public void TryGetProperty_FindsPascalOrCamelAlias(string key)
    {
        var root = Parse($$"""{ "{{key}}": [ { "type": "remap" } ] }""");

        Assert.That(
            root.TryGetPropertyIgnoreCase("outputColorTransforms", out var value),
            Is.True);
        Assert.That(value.ValueKind, Is.EqualTo(JsonValueKind.Array));
    }

    [Test]
    public void TryGetProperty_ReturnsFalseWhenNeitherAliasPresent()
    {
        var root = Parse("""{ "time": 0 }""");

        Assert.That(
            root.TryGetPropertyIgnoreCase("background", out _),
            Is.False);
    }

    [TestCase("strength")]
    [TestCase("Strength")]
    [TestCase("STRENGTH")]
    public void TryGetPropertyIgnoreCase_MatchesMixedCaseKeys(string key)
    {
        var root = Parse($$"""{ "{{key}}": 2.5 }""");

        Assert.That(JsonElementExtensions.TryGetPropertyIgnoreCase(root, "strength", out var value), Is.True);
        Assert.That(value.GetSingle(), Is.EqualTo(2.5f));
    }

    [Test]
    public void TryGetPropertyIgnoreCase_ReturnsFalseForNonObject()
    {
        var root = Parse("""[1, 2, 3]""");

        Assert.That(JsonElementExtensions.TryGetPropertyIgnoreCase(root, "type", out _), Is.False);
    }
}
