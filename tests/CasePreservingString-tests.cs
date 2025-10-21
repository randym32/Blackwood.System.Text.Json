using Blackwood;
namespace Blackwood.tests;

/// <summary>
/// Key Points in These Tests:
/// 1. Constructor Test: Verifies that the constructor correctly sets the text,
/// property.
///
/// 2. ToString Test: Ensures that the ToString method returns the original text.
///
/// 3. GetHashCode Test: Ensures that the GetHashCode method returns the same
/// hash code for strings that are equal in a case-insensitive manner..
///
/// 4. Equals Test: Checks that the Equals method correctly identifies equal
/// and non-equal CasePreservingString objects based on case-insensitive comparison.
///
/// 5. Implicit Conversion Test: Verifies that the implicit conversion from
/// string to CasePreservingString works correctly.
/// </summary>
[TestFixture]
public class CasePreservingStringTests
{
    [Test]
    public void Constructor_SetsTextProperty()
    {
        // Arrange
        string expectedText = "TestString";

        // Act
        CasePreservingString cps = new CasePreservingString(expectedText);

        // Assert
        Assert.That(expectedText, Is.EqualTo(cps.text));
    }

    [Test]
    public void ToString_ReturnsOriginalText()
    {
        // Arrange
        string expectedText = "TestString";
        CasePreservingString cps = new CasePreservingString(expectedText);

        // Act
        string result = cps.ToString();

        // Assert
        Assert.That(expectedText, Is.EqualTo(result));
    }

    [Test]
    public void GetHashCode_CaselessHashCode()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("TestString");
        CasePreservingString cps2 = new CasePreservingString("teststring");

        // Act
        int hash1 = cps1.GetHashCode();
        int hash2 = cps2.GetHashCode();

        // Assert
        Assert.That(hash1 == hash2);
    }

    [Test]
    public void Equals_CaselessComparison_ReturnsTrue()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("TestString");
        CasePreservingString cps2 = new CasePreservingString("teststring");

        // Act
        bool result = cps1.Equals(cps2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Equals_CaseInsensitiveComparison_ReturnsTrue()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Hello");
        CasePreservingString cps2 = new CasePreservingString("HELLO");

        // Act
        bool result = cps1.Equals(cps2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Equals_DifferentStrings_ReturnsFalse()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Hello");
        CasePreservingString cps2 = new CasePreservingString("World");

        // Act
        bool result = cps1.Equals(cps2);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Equals_SameString_ReturnsTrue()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Test");
        CasePreservingString cps2 = new CasePreservingString("Test");

        // Act
        bool result = cps1.Equals(cps2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ImplicitConversion_FromString_WorksCorrectly()
    {
        // Arrange
        string originalString = "TestString";

        // Act
        CasePreservingString cps = originalString;

        // Assert
        Assert.That(cps.text, Is.EqualTo(originalString));
    }

    [Test]
    public void ImplicitConversion_ToString_WorksCorrectly()
    {
        // Arrange
        CasePreservingString cps = new CasePreservingString("TestString");

        // Act
        string result = cps;

        // Assert
        Assert.That(result, Is.EqualTo("TestString"));
    }

    [Test]
    public void GetHashCode_SameStringDifferentCase_ReturnsSameHashCode()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("TestString");
        CasePreservingString cps2 = new CasePreservingString("teststring");

        // Act
        int hash1 = cps1.GetHashCode();
        int hash2 = cps2.GetHashCode();

        // Assert
        Assert.That(hash1, Is.EqualTo(hash2));
    }

    [Test]
    public void GetHashCode_DifferentStrings_ReturnsDifferentHashCodes()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Hello");
        CasePreservingString cps2 = new CasePreservingString("World");

        // Act
        int hash1 = cps1.GetHashCode();
        int hash2 = cps2.GetHashCode();

        // Assert
        Assert.That(hash1, Is.Not.EqualTo(hash2));
    }

    [Test]
    public void GetHashCode_EmptyString_ReturnsHashCode()
    {
        // Arrange
        CasePreservingString cps = new CasePreservingString("");

        // Act
        int hash = cps.GetHashCode();

        // Assert
        Assert.That(hash, Is.EqualTo(StringComparer.InvariantCultureIgnoreCase.GetHashCode("")));
    }

    [Test]
    public void GetHashCode_NullString_ReturnsHashCode()
    {
        // Arrange
        CasePreservingString cps = new CasePreservingString(null!);

        // Act
        int hash = cps.GetHashCode();

        // Assert
        Assert.That(hash, Is.EqualTo(0)); // Empty string hash code
    }

    [Test]
    public void ToString_PreservesOriginalCase()
    {
        // Arrange
        string originalString = "TestString";
        CasePreservingString cps = new CasePreservingString(originalString);

        // Act
        string result = cps.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(originalString));
    }

    [Test]
    public void ToString_EmptyString_ReturnsEmptyString()
    {
        // Arrange
        CasePreservingString cps = new CasePreservingString("");

        // Act
        string result = cps.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void ToString_NullString_ReturnsNull()
    {
        // Arrange
        CasePreservingString cps = new CasePreservingString(null!);

        // Act
        string result = cps.ToString();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Equals_WithSpecialCharacters_WorksCorrectly()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Test@#$%");
        CasePreservingString cps2 = new CasePreservingString("test@#$%");

        // Act
        bool result = cps1.Equals(cps2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Equals_WithNumbers_WorksCorrectly()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Test123");
        CasePreservingString cps2 = new CasePreservingString("TEST123");

        // Act
        bool result = cps1.Equals(cps2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Equals_WithUnicode_WorksCorrectly()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Test世界");
        CasePreservingString cps2 = new CasePreservingString("TEST世界");

        // Act
        bool result = cps1.Equals(cps2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GetHashCode_WithUnicode_ReturnsConsistentHashCode()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Test世界");
        CasePreservingString cps2 = new CasePreservingString("TEST世界");

        // Act
        int hash1 = cps1.GetHashCode();
        int hash2 = cps2.GetHashCode();

        // Assert
        Assert.That(hash1, Is.EqualTo(hash2));
    }

    [Test]
    public void Constructor_WithEmptyString_SetsTextProperty()
    {
        // Arrange
        string expectedText = "";

        // Act
        CasePreservingString cps = new CasePreservingString(expectedText);

        // Assert
        Assert.That(cps.text, Is.EqualTo(expectedText));
    }

    [Test]
    public void Constructor_WithNullString_SetsTextProperty()
    {
        // Arrange
        string expectedText = null!;

        // Act
        CasePreservingString cps = new CasePreservingString(expectedText);

        // Assert
        Assert.That(cps.text, Is.EqualTo(expectedText));
    }

    [Test]
    public void Constructor_WithWhitespaceString_SetsTextProperty()
    {
        // Arrange
        string expectedText = "   ";

        // Act
        CasePreservingString cps = new CasePreservingString(expectedText);

        // Assert
        Assert.That(cps.text, Is.EqualTo(expectedText));
    }

    [Test]
    public void Equals_WithWhitespace_WorksCorrectly()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Test String");
        CasePreservingString cps2 = new CasePreservingString("TEST STRING");

        // Act
        bool result = cps1.Equals(cps2);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GetHashCode_WithWhitespace_ReturnsConsistentHashCode()
    {
        // Arrange
        CasePreservingString cps1 = new CasePreservingString("Test String");
        CasePreservingString cps2 = new CasePreservingString("TEST STRING");

        // Act
        int hash1 = cps1.GetHashCode();
        int hash2 = cps2.GetHashCode();

        // Assert
        Assert.That(hash1, Is.EqualTo(hash2));
    }

    [Test]
    public void ImplicitConversion_FromStringToCasePreservingString()
    {
        // Arrange
        string text = "TestString";

        // Act
        CasePreservingString cps = text;

        // Assert
        Assert.That(text, Is.EqualTo(cps.text));
    }

    [Test]
    public void ImplicitConversion_FromCasePreservingStringToString()
    {
        // Arrange
        CasePreservingString cps = new CasePreservingString("TestString");

        // Act
        string text = cps;

        // Assert
        Assert.That(text, Is.EqualTo("TestString"));
    }

    [Test]
    public void ImplicitConversion_FromCasePreservingStringToString_PreservesCase()
    {
        // Arrange
        CasePreservingString cps = new CasePreservingString("TestSTRING");

        // Act
        string text = cps;

        // Assert
        Assert.That(text, Is.EqualTo("TestSTRING"));
    }
}