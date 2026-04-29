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
        MovieTitle title = MovieTitle.Create("Inception");
        List<MovieGenre> genres = [MovieGenre.Create("Sci-Fi")];
        List<Actor> actors = [Actor.Create("Leonardo DiCaprio")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2010);
        MovieDuration duration = MovieDuration.Create(148);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/poster.jpg");

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
        MovieTitle title = MovieTitle.Create("Movie");
        List<MovieGenre> genres = [];
        List<Actor> actors = [];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/poster.jpg");

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
        Assert.Equal("Howestprime.Movies.Movie.MovieRegistered", domainEvent.FQDN);
    }
}
