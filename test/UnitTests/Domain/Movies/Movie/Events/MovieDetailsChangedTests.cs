using Howestprime.Movies.Domain.Movies.Movie.Events;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.Events;

public sealed class MovieDetailsChangedTests
{
    [Fact]
    public void Constructor_WithValidInput_ShouldInitializeAllProperties()
    {
        // Arrange
        MovieId movieId = new(Guid.NewGuid());
        string title = "Inception";
        string description = "A thief who enters dreams.";
        int releaseYear = 2010;
        IReadOnlyList<string> genres = ["Sci-Fi", "Thriller"];
        IReadOnlyList<string> actors = ["Leonardo DiCaprio"];
        int ageRating = 13;
        string posterUrl = "https://example.com/poster.jpg";

        // Act
        MovieDetailsChanged domainEvent = new(
            movieId,
            title,
            description,
            releaseYear,
            genres,
            actors,
            ageRating,
            posterUrl
        );

        // Assert
        Assert.Equal(movieId, domainEvent.MovieId);
        Assert.Equal(title, domainEvent.Title);
        Assert.Equal(description, domainEvent.Description);
        Assert.Equal(releaseYear, domainEvent.ReleaseYear);
        Assert.Equal(genres, domainEvent.Genres);
        Assert.Equal(actors, domainEvent.Actors);
        Assert.Equal(ageRating, domainEvent.AgeRating);
        Assert.Equal(posterUrl, domainEvent.PosterUrl);
    }

    [Fact]
    public void Constructor_ShouldGenerateExpectedFqdn()
    {
        // Arrange
        MovieId movieId = new(Guid.NewGuid());

        // Act
        MovieDetailsChanged domainEvent = new(
            movieId, "Title", "Desc", 2020, [], [], 12, "https://example.com/poster.jpg"
        );

        // Assert
        Assert.Equal("Howestprime.Movies.Movie.MovieDetailsChanged", domainEvent.FQDN);
    }

    [Fact]
    public void Constructor_ShouldSetOccurredOn()
    {
        // Arrange
        DateTimeOffset before = DateTimeOffset.UtcNow;

        // Act
        MovieDetailsChanged domainEvent = new(
            new MovieId(Guid.NewGuid()), "Title", "Desc", 2020, [], [], 12, "https://example.com/poster.jpg"
        );
        DateTimeOffset after = DateTimeOffset.UtcNow;

        // Assert
        Assert.InRange(domainEvent.OccurredOn, before, after);
    }
}
