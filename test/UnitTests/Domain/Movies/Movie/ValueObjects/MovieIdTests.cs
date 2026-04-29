using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class MovieIdTests
{
    [Fact]
    public void Constructor_WithValidGuid_ShouldSetValue()
    {
        // Arrange
        Guid idValue = Guid.NewGuid();

        // Act
        MovieId movieId = new(idValue);

        // Assert
        Assert.Equal(idValue, movieId.Value);
    }

    [Fact]
    public void Constructor_WithEmptyGuid_ShouldSetValue()
    {
        // Arrange
        Guid idValue = Guid.Empty;

        // Act
        MovieId movieId = new(idValue);

        // Assert
        Assert.Equal(idValue, movieId.Value);
    }
}
