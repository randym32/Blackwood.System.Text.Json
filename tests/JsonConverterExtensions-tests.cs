using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using Blackwood;

namespace Blackwood.System.Text.Json.Tests;

/// <summary>
/// Tests for <see cref="JsonConverterExtensions"/>.
/// Verifies that Skip() safely consumes values (objects, arrays, primitives)
/// without throwing and without mutating the caller's reader state.
/// </summary>
[TestFixture]
public class JsonConverterExtensionsTests
{
    private class DummyConverter : JsonConverter<object>
    {
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Not used in tests
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            // Not used in tests
            throw new NotImplementedException();
        }
    }

    private static Utf8JsonReader CreateReader(string json)
    {
        var data = Encoding.UTF8.GetBytes(json);
        var reader = new Utf8JsonReader(data);
        // Move to first token
        reader.Read();
        return reader;
    }

    /// <summary>
    /// Ensures Skip() handles a primitive token without throwing and without moving caller's reader.
    /// </summary>
    [Test]
    public void Skip_PrimitiveToken_NoThrow_NoReaderMutation()
    {
        var reader = CreateReader("123");
        var originalToken = reader.TokenType; // Number

        var conv = new DummyConverter();
        try { conv.Skip(reader); }
        catch (Exception ex) { Assert.Fail($"Skip threw for primitive: {ex.Message}"); }

        // Reader was passed by value, so caller's state should be unchanged
        Assert.That(reader.TokenType, Is.EqualTo(originalToken));
    }

    /// <summary>
    /// Ensures Skip() handles an empty object without throwing and without moving caller's reader.
    /// </summary>
    [Test]
    public void Skip_EmptyObject_NoThrow_NoReaderMutation()
    {
        var reader = CreateReader("{}\n");
        var originalDepth = reader.CurrentDepth; // At StartObject
        Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject));

        var conv = new DummyConverter();
        try { conv.Skip(reader); }
        catch (Exception ex) { Assert.Fail($"Skip threw for empty object: {ex.Message}"); }

        // Reader state unchanged in caller
        Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject));
        Assert.That(reader.CurrentDepth, Is.EqualTo(originalDepth));
    }

    /// <summary>
    /// Ensures Skip() handles a nested object/array structure without throwing.
    /// Confirms that the extension can iterate through nested tokens safely.
    /// </summary>
    [Test]
    public void Skip_NestedObjectAndArray_NoThrow()
    {
        var json = "{\"a\":[1,{\"b\":[2,3]},4],\"c\":{\"d\":{}}}";
        var reader = CreateReader(json);
        Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject));

        var conv = new DummyConverter();
        try { conv.Skip(reader); }
        catch (Exception ex) { Assert.Fail($"Skip threw for nested: {ex.Message}"); }

        // Caller state unchanged
        Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject));
    }

    /// <summary>
    /// Ensures Skip() handles an array root without throwing and without mutating caller state.
    /// </summary>
    [Test]
    public void Skip_ArrayRoot_NoThrow_NoReaderMutation()
    {
        var reader = CreateReader("[1,2,3]");
        Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartArray));

        var conv = new DummyConverter();
        try { conv.Skip(reader); }
        catch (Exception ex) { Assert.Fail($"Skip threw for array root: {ex.Message}"); }

        Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartArray));
    }
}


