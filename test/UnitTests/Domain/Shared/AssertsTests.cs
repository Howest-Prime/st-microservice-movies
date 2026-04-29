using Howestprime.Movies.Domain.Shared;

namespace UnitTests.Domain.Shared;

public sealed class AssertsTests
{
    // ============== EnsureNotEmpty(string) Tests ==============

    [Fact]
    public void EnsureNotEmpty_WithEmptyString_ShouldThrow()
    {
        // Arrange
        const string value = "";

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureNotEmpty_WithWhitespaceString_ShouldThrow()
    {
        // Arrange
        const string value = "   ";

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureNotEmpty_WithNullString_ShouldThrow()
    {
        // Arrange
        string? value = null;

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value!);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureNotEmpty_WithValidString_ShouldNotThrow()
    {
        // Arrange
        const string value = "ok";

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureNotEmpty_WithStringContainingOnlySpace_ShouldThrow()
    {
        // Arrange
        const string value = " ";

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureNotEmpty_WithSingleCharacterString_ShouldNotThrow()
    {
        // Arrange
        const string value = "a";

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    // ============== EnsureNotEmpty(object) Tests ==============

    [Fact]
    public void EnsureNotEmpty_WithNullObject_ShouldThrow()
    {
        // Arrange
        object? value = null;

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value!);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureNotEmpty_WithValidObject_ShouldNotThrow()
    {
        // Arrange
        object value = new();

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureNotEmpty_WithStringObject_ShouldNotThrow()
    {
        // Arrange
        object value = "test";

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureNotEmpty_WithIntObject_ShouldNotThrow()
    {
        // Arrange
        object value = 42;

        // Act
        Action act = () => Asserts.EnsureNotEmpty(value);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    // ============== EnsureGreaterThan Tests ==============

    [Fact]
    public void EnsureGreaterThan_WithValueLessThanThreshold_ShouldThrow()
    {
        // Arrange
        const int value = 5;
        const int threshold = 10;

        // Act
        Action act = () => Asserts.EnsureGreaterThan(value, threshold);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureGreaterThan_WithValueEqualToThreshold_ShouldThrow()
    {
        // Arrange
        const int value = 10;
        const int threshold = 10;

        // Act
        Action act = () => Asserts.EnsureGreaterThan(value, threshold);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureGreaterThan_WithValueGreaterThanThreshold_ShouldNotThrow()
    {
        // Arrange
        const int value = 15;
        const int threshold = 10;

        // Act
        Action act = () => Asserts.EnsureGreaterThan(value, threshold);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureGreaterThan_WithBoundaryJustAboveThreshold_ShouldNotThrow()
    {
        // Arrange
        const int value = 11;
        const int threshold = 10;

        // Act
        Action act = () => Asserts.EnsureGreaterThan(value, threshold);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureGreaterThan_WithNegativeValue_ShouldThrow()
    {
        // Arrange
        const int value = -5;
        const int threshold = 0;

        // Act
        Action act = () => Asserts.EnsureGreaterThan(value, threshold);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureGreaterThan_WithBothNegativeValues_ShouldThrow()
    {
        // Arrange
        const int value = -5;
        const int threshold = -3;

        // Act
        Action act = () => Asserts.EnsureGreaterThan(value, threshold);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureGreaterThan_WithNegativeValueGreaterThanNegativeThreshold_ShouldNotThrow()
    {
        // Arrange
        const int value = -3;
        const int threshold = -5;

        // Act
        Action act = () => Asserts.EnsureGreaterThan(value, threshold);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureGreaterThan_WithZeroThreshold_ShouldThrow()
    {
        // Arrange
        const int value = 0;
        const int threshold = 0;

        // Act
        Action act = () => Asserts.EnsureGreaterThan(value, threshold);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    // ============== EnsureNotNegative Tests ==============

    [Fact]
    public void EnsureNotNegative_WithNegativeValue_ShouldThrow()
    {
        // Arrange
        const int value = -5;

        // Act
        Action act = () => Asserts.EnsureNotNegative(value);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureNotNegative_WithZero_ShouldNotThrow()
    {
        // Arrange
        const int value = 0;

        // Act
        Action act = () => Asserts.EnsureNotNegative(value);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureNotNegative_WithPositiveValue_ShouldNotThrow()
    {
        // Arrange
        const int value = 10;

        // Act
        Action act = () => Asserts.EnsureNotNegative(value);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureNotNegative_WithMinusOne_ShouldThrow()
    {
        // Arrange
        const int value = -1;

        // Act
        Action act = () => Asserts.EnsureNotNegative(value);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EnsureNotNegative_WithLargePositiveValue_ShouldNotThrow()
    {
        // Arrange
        const int value = 1000000;

        // Act
        Action act = () => Asserts.EnsureNotNegative(value);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void EnsureNotNegative_WithLargeNegativeValue_ShouldThrow()
    {
        // Arrange
        const int value = -1000000;

        // Act
        Action act = () => Asserts.EnsureNotNegative(value);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}
