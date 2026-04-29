using Howestprime.Movies.Domain.Movies.Movie.Exceptions;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class MovieDurationTests
{
    [Fact]
    public void Create_WithZeroDuration_ShouldThrowInvalidMovieDurationException()
    {
        // Arrange
        int duration = 0;
        Action act = () => MovieDuration.Create(duration);

        // Act
        InvalidMovieDurationException exception = Assert.Throws<InvalidMovieDurationException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithNegativeDuration_ShouldThrowInvalidMovieDurationException()
    {
        // Arrange
        int duration = -1;
        Action act = () => MovieDuration.Create(duration);

        // Act
        InvalidMovieDurationException exception = Assert.Throws<InvalidMovieDurationException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithValidDuration_ShouldSetValue()
    {
        // Arrange
        int duration = 120;

        // Act
        MovieDuration movieDuration = MovieDuration.Create(duration);

        // Assert
        Assert.Equal(duration, movieDuration.Value);
    }

    [Fact]
    public void Create_WithBoundaryDuration_ShouldSetValue()
    {
        // Arrange
        int duration = 1;

        // Act
        MovieDuration movieDuration = MovieDuration.Create(duration);

        // Assert
        Assert.Equal(duration, movieDuration.Value);
    }
}
