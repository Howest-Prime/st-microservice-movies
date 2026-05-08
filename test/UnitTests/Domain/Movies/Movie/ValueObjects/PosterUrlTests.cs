using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class PosterUrlTests
{
    [Fact]
    public void Create_WithNullUrl_ShouldThrowInvalidMoviePosterUrlException()
    {
        // Arrange
        string posterUrl = null!;
        Action act = () => PosterUrl.Create(posterUrl);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithWhitespaceUrl_ShouldThrowInvalidMoviePosterUrlException()
    {
        // Arrange
        string posterUrl = "   ";
        Action act = () => PosterUrl.Create(posterUrl);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithValidUrl_ShouldSetTrimmedValue()
    {
        // Arrange
        string posterUrl = "  https://example.com/poster.jpg  ";
        string expectedValue = "https://example.com/poster.jpg";

        // Act
        PosterUrl createdPosterUrl = PosterUrl.Create(posterUrl);

        // Assert
        Assert.Equal(expectedValue, createdPosterUrl.Value);
    }
}
