using Howestprime.Movies.Domain.Movies.Movie.Exceptions;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class AgeRatingTests
{
    [Fact]
    public void From_WithUnsupportedRating_ShouldThrowInvalidAgeRatingException()
    {
        // Arrange
        int unsupportedRating = 7;
        Action act = () => AgeRating.From(unsupportedRating);

        // Act
        InvalidAgeRatingException exception = Assert.Throws<InvalidAgeRatingException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void From_WithNegativeBoundary_ShouldThrowInvalidAgeRatingException()
    {
        // Arrange
        int unsupportedRating = -1;
        Action act = () => AgeRating.From(unsupportedRating);

        // Act
        InvalidAgeRatingException exception = Assert.Throws<InvalidAgeRatingException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithValidCategory_ShouldSetValueCorrectly()
    {
        // Arrange
        AgeRatingCategory ratingCategory = AgeRatingCategory.Twelve;

        // Act
        AgeRating ageRating = AgeRating.Create(ratingCategory);

        // Assert
        Assert.Equal(ratingCategory, ageRating.Value);
    }

    [Fact]
    public void From_WithMinimumBoundary_ShouldReturnAllCategory()
    {
        // Arrange
        int minimumBoundaryValue = 0;

        // Act
        AgeRating ageRating = AgeRating.From(minimumBoundaryValue);

        // Assert
        Assert.Equal(AgeRatingCategory.All, ageRating.Value);
    }

    [Fact]
    public void From_WithMaximumBoundary_ShouldReturnEighteenCategory()
    {
        // Arrange
        int maximumBoundaryValue = 18;

        // Act
        AgeRating ageRating = AgeRating.From(maximumBoundaryValue);

        // Assert
        Assert.Equal(AgeRatingCategory.Eighteen, ageRating.Value);
    }

    [Fact]
    public void From_WithValidIntermediateValue_ShouldReturnExpectedCategory()
    {
        // Arrange
        int intermediateValue = 14;

        // Act
        AgeRating ageRating = AgeRating.From(intermediateValue);

        // Assert
        Assert.Equal(AgeRatingCategory.Fourteen, ageRating.Value);
    }

    [Fact]
    public void All_WithStaticFactory_ShouldReturnAllCategory()
    {
        // Arrange
        AgeRating expectedAgeRating = AgeRating.Create(AgeRatingCategory.All);

        // Act
        AgeRating actualAgeRating = AgeRating.ALL;

        // Assert
        Assert.Equal(expectedAgeRating, actualAgeRating);
    }

    [Fact]
    public void Six_WithStaticFactory_ShouldReturnSixCategory()
    {
        // Arrange
        AgeRating expectedAgeRating = AgeRating.Create(AgeRatingCategory.Six);

        // Act
        AgeRating actualAgeRating = AgeRating.SIX;

        // Assert
        Assert.Equal(expectedAgeRating, actualAgeRating);
    }

    [Fact]
    public void Nine_WithStaticFactory_ShouldReturnNineCategory()
    {
        // Arrange
        AgeRating expectedAgeRating = AgeRating.Create(AgeRatingCategory.Nine);

        // Act
        AgeRating actualAgeRating = AgeRating.NINE;

        // Assert
        Assert.Equal(expectedAgeRating, actualAgeRating);
    }

    [Fact]
    public void Twelve_WithStaticFactory_ShouldReturnTwelveCategory()
    {
        // Arrange
        AgeRating expectedAgeRating = AgeRating.Create(AgeRatingCategory.Twelve);

        // Act
        AgeRating actualAgeRating = AgeRating.TWELVE;

        // Assert
        Assert.Equal(expectedAgeRating, actualAgeRating);
    }

    [Fact]
    public void Fourteen_WithStaticFactory_ShouldReturnFourteenCategory()
    {
        // Arrange
        AgeRating expectedAgeRating = AgeRating.Create(AgeRatingCategory.Fourteen);

        // Act
        AgeRating actualAgeRating = AgeRating.FOURTEEN;

        // Assert
        Assert.Equal(expectedAgeRating, actualAgeRating);
    }

    [Fact]
    public void Sixteen_WithStaticFactory_ShouldReturnSixteenCategory()
    {
        // Arrange
        AgeRating expectedAgeRating = AgeRating.Create(AgeRatingCategory.Sixteen);

        // Act
        AgeRating actualAgeRating = AgeRating.SIXTEEN;

        // Assert
        Assert.Equal(expectedAgeRating, actualAgeRating);
    }

    [Fact]
    public void Eighteen_WithStaticFactory_ShouldReturnEighteenCategory()
    {
        // Arrange
        AgeRating expectedAgeRating = AgeRating.Create(AgeRatingCategory.Eighteen);

        // Act
        AgeRating actualAgeRating = AgeRating.EIGHTEEN;

        // Assert
        Assert.Equal(expectedAgeRating, actualAgeRating);
    }
}
