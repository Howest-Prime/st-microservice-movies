using Howestprime.Movies.Domain.Movies.Movie.Events;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.Events;

public sealed class MovieRegisteredTests
{
    [Fact]
    public void Create_WithValidInput_ShouldInitializeAllProperties()
    {
        // Arrange
        MovieId movieId = new(Guid.NewGuid());
        string title = "Inception";
        IReadOnlyList<string> genres = ["Sci-Fi"];
        IReadOnlyList<string> actors = ["Leonardo DiCaprio"];
        int releaseYear = 2010;
        int duration = 148;
        int ageRating = 12;
        string posterUrl = "https://example.com/poster.jpg";

        // Act
        MovieRegistered domainEvent = MovieRegistered.Create(
            movieId,
            title,
            genres,
            actors,
            releaseYear,
            duration,
            ageRating,
            posterUrl
        );

        // Assert
        Assert.Equal(movieId, domainEvent.MovieId);
        Assert.Equal(title, domainEvent.Title);
        Assert.Equal(releaseYear, domainEvent.ReleaseYear);
        Assert.Equal(duration, domainEvent.Duration);
        Assert.Equal(ageRating, domainEvent.AgeRating);
        Assert.Equal(posterUrl, domainEvent.PosterUrl);
        Assert.Single(domainEvent.Genres);
        Assert.Single(domainEvent.Actors);
    }

    [Fact]
    public void Constructor_ShouldGenerateExpectedFqdn()
    {
        // Arrange
        MovieId movieId = new(Guid.NewGuid());

        // Act
        MovieRegistered domainEvent = MovieRegistered.Create(
            movieId,
            "Movie",
            [],
            [],
            2020,
            100,
            12,
            "https://example.com/poster.jpg"
        );

        // Assert
        Assert.Equal("Howestprime.Movies.Movie.MovieRegistered", domainEvent.FQDN);
    }
}
