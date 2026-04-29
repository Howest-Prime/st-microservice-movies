using Howestprime.Movies.Domain.Movies.Movie.Exceptions;
using Howestprime.Movies.Domain.Shared;

namespace UnitTests.Domain.Movies.Movie.Exceptions;

public sealed class MovieExceptionsTests
{
    [Fact]
    public void InvalidMovieTitleException_ShouldInheritDomainExceptionAndSetMessage()
    {
        // Arrange
        const string title = "Invalid";

        // Act
        InvalidMovieTitleException exception = new(title, 50);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.Contains(title, exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void InvalidMovieDescriptionException_ShouldInheritDomainExceptionAndSetMessage()
    {
        // Arrange
        const string description = "Invalid";

        // Act
        InvalidMovieDescriptionException exception = new(description, 300);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.Contains(description, exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void InvalidMovieDurationException_ShouldInheritDomainExceptionAndSetMessage()
    {
        // Arrange
        const int duration = 0;

        // Act
        InvalidMovieDurationException exception = new(duration);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.Contains(duration.ToString(), exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void InvalidMovieGenreException_ShouldInheritDomainExceptionAndSetMessage()
    {
        // Arrange
        const string genre = "Invalid";

        // Act
        InvalidMovieGenreException exception = new(genre, 50);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.Contains(genre, exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void InvalidMovieReleaseYearException_ShouldInheritDomainExceptionAndSetMessage()
    {
        // Arrange
        const int invalidYear = 1800;

        // Act
        InvalidMovieReleaseYearException exception = new(invalidYear, 1888, 2030);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.Contains(invalidYear.ToString(), exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void InvalidActorException_ShouldInheritDomainExceptionAndSetMessage()
    {
        // Arrange
        const string actor = "";

        // Act
        InvalidActorException exception = new(actor);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.Contains("actor", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void InvalidAgeRatingException_ShouldInheritDomainExceptionAndSetMessage()
    {
        // Arrange
        const string rating = "99";
        string[] validRatings = ["0", "6", "9", "12", "14", "16", "18"];

        // Act
        InvalidAgeRatingException exception = new(rating, validRatings);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.Contains(rating, exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void InvalidMoviePosterUrlException_ShouldInheritDomainExceptionAndSetMessage()
    {
        // Arrange
        const string posterUrl = "";

        // Act
        InvalidMoviePosterUrlException exception = new(posterUrl);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.Contains("poster", exception.Message, StringComparison.OrdinalIgnoreCase);
    }
}
