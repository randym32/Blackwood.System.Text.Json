using Blackwood;
namespace Blackwood.tests;

/// <summary>
/// Comprehensive test suite for the CasePreservingString class.
///
/// Key Points in These Tests:
/// 1. Constructor Test: Verifies that the constructor correctly sets the text property.
/// 2. ToString Test: Ensures that the ToString method returns the original text.
/// 3. GetHashCode Test: Ensures that the GetHashCode method returns the same
///    hash code for strings that are equal in a case-insensitive manner.
/// 4. Equals Test: Checks that the Equals method correctly identifies equal
///    and non-equal CasePreservingString objects based on case-insensitive comparison.
/// 5. Implicit Conversion Test: Verifies that the implicit conversion from
///    string to CasePreservingString works correctly.
///
/// The CasePreservingString class is designed to maintain the original case of strings
/// while providing case-insensitive equality and hashing for use in collections and comparisons.
/// </summary>
[TestFixture]
public class CasePreservingStringTests
{
    /// <summary>
    /// Tests that the constructor properly initializes the text property with the provided string value.
    /// This is a fundamental test to ensure the basic construction of CasePreservingString objects works correctly.
    /// </summary>
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

    /// <summary>
    /// Tests that the ToString method returns the original text exactly as it was provided.
    /// This ensures that the case-preserving nature of the class is maintained when converting back to string.
    /// </summary>
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

    /// <summary>
    /// Tests that GetHashCode returns the same hash code for strings that differ only in case.
    /// This is crucial for proper functioning in hash-based collections like Dictionary and HashSet.
    /// </summary>
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

    /// <summary>
    /// Tests that the Equals method returns true for strings that differ only in case.
    /// This verifies the core case-insensitive equality behavior of CasePreservingString.
    /// </summary>
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

    /// <summary>
    /// Tests case-insensitive equality with all uppercase vs mixed case strings.
    /// This provides additional coverage for the case-insensitive comparison logic.
    /// </summary>
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

    /// <summary>
    /// Tests that the Equals method returns false for completely different strings.
    /// This ensures that the equality comparison doesn't incorrectly match unrelated strings.
    /// </summary>
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

    /// <summary>
    /// Tests that the Equals method returns true for identical strings.
    /// This verifies the basic equality behavior for strings that are exactly the same.
    /// </summary>
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

    /// <summary>
    /// Tests the implicit conversion from string to CasePreservingString.
    /// This verifies that strings can be automatically converted to CasePreservingString objects.
    /// </summary>
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

    /// <summary>
    /// Tests the implicit conversion from CasePreservingString to string.
    /// This verifies that CasePreservingString objects can be automatically converted back to strings.
    /// </summary>
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

    /// <summary>
    /// Tests that GetHashCode returns identical hash codes for strings that differ only in case.
    /// This is essential for proper behavior in hash-based collections where case-insensitive keys are needed.
    /// </summary>
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

    /// <summary>
    /// Tests that GetHashCode returns different hash codes for completely different strings.
    /// This ensures that unrelated strings don't collide in hash-based collections.
    /// </summary>
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

    /// <summary>
    /// Tests that GetHashCode works correctly with empty strings.
    /// This verifies that edge cases with empty strings are handled properly.
    /// </summary>
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

    /// <summary>
    /// Tests that GetHashCode handles null strings gracefully.
    /// This verifies that null string inputs don't cause exceptions in hash code generation.
    /// </summary>
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

    /// <summary>
    /// Tests that ToString preserves the original case of the input string.
    /// This is the core functionality of CasePreservingString - maintaining case while providing case-insensitive equality.
    /// </summary>
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

    /// <summary>
    /// Tests that ToString correctly handles empty strings.
    /// This verifies that edge cases with empty strings are handled properly.
    /// </summary>
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

    /// <summary>
    /// Tests that ToString correctly handles null strings.
    /// This verifies that null string inputs are handled gracefully.
    /// </summary>
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

    /// <summary>
    /// Tests that Equals works correctly with strings containing special characters.
    /// This verifies that case-insensitive comparison works with non-alphabetic characters.
    /// </summary>
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

    /// <summary>
    /// Tests that Equals works correctly with strings containing numbers.
    /// This verifies that case-insensitive comparison works with alphanumeric strings.
    /// </summary>
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

    /// <summary>
    /// Tests that Equals works correctly with Unicode characters.
    /// This verifies that case-insensitive comparison works with international characters.
    /// </summary>
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

    /// <summary>
    /// Tests that GetHashCode returns consistent hash codes for Unicode strings that differ only in case.
    /// This verifies that hash code generation works correctly with international characters.
    /// </summary>
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

    /// <summary>
    /// Tests that the constructor correctly handles empty strings.
    /// This verifies that edge cases with empty strings are handled properly during construction.
    /// </summary>
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

    /// <summary>
    /// Tests that the constructor correctly handles null strings.
    /// This verifies that null string inputs are handled gracefully during construction.
    /// </summary>
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

    /// <summary>
    /// Tests that the constructor correctly handles whitespace-only strings.
    /// This verifies that strings containing only whitespace characters are handled properly.
    /// </summary>
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

    /// <summary>
    /// Tests that Equals works correctly with strings containing whitespace.
    /// This verifies that case-insensitive comparison works with strings that include spaces.
    /// </summary>
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

    /// <summary>
    /// Tests that GetHashCode returns consistent hash codes for strings with whitespace that differ only in case.
    /// This verifies that hash code generation works correctly with strings containing spaces.
    /// </summary>
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

    /// <summary>
    /// Tests the implicit conversion from string to CasePreservingString with a different test name.
    /// This provides additional coverage for the implicit conversion functionality.
    /// </summary>
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

    /// <summary>
    /// Tests the implicit conversion from CasePreservingString to string with a different test name.
    /// This provides additional coverage for the implicit conversion functionality.
    /// </summary>
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

    /// <summary>
    /// Tests that implicit conversion from CasePreservingString to string preserves the original case.
    /// This verifies that the case-preserving nature is maintained through the conversion process.
    /// </summary>
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