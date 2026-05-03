using Howestprime.Movies.Application.Contracts.Data.Movies;

namespace UnitTests.Application.Contracts.Data;

public sealed class MovieDataTests
{
    [Fact]
    public void Create_WithDefaults_ShouldInitializeCollectionsAndStrings()
    {
        // Arrange
        MovieData data = new();

        // Act
        IReadOnlyList<GenreData> genres = data.Genres;
        IReadOnlyList<ActorData> actors = data.Actors;

        // Assert
        Assert.Equal(Guid.Empty, data.Id);
        Assert.Equal(string.Empty, data.Title);
        Assert.Equal(string.Empty, data.Description);
        Assert.Equal(0, data.ReleaseYear);
        Assert.Equal(0, data.Duration);
        Assert.NotNull(genres);
        Assert.NotNull(actors);
        Assert.Empty(genres);
        Assert.Empty(actors);
        Assert.Equal(0, data.AgeRating);
        Assert.Equal(string.Empty, data.PosterUrl);
    }

    [Fact]
    public void Create_WithValues_ShouldSetAllProperties()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        GenreData genre = new() { Value = "Sci-Fi" };
        ActorData actor = new() { Name = "Leonardo DiCaprio" };
        MovieData data = new()
        {
            Id = id,
            Title = "Inception",
            Description = "A movie about dreams.",
            ReleaseYear = 2010,
            Duration = 148,
            Genres = new List<GenreData> { genre },
            Actors = new List<ActorData> { actor },
            AgeRating = 12,
            PosterUrl = "https://example.com/inception.jpg"
        };

        // Act
        IReadOnlyList<GenreData> genres = data.Genres;
        IReadOnlyList<ActorData> actors = data.Actors;

        // Assert
        Assert.Equal(id, data.Id);
        Assert.Equal("Inception", data.Title);
        Assert.Equal("A movie about dreams.", data.Description);
        Assert.Equal(2010, data.ReleaseYear);
        Assert.Equal(148, data.Duration);
        Assert.Single(genres);
        Assert.Single(actors);
        Assert.Equal(genre.Value, genres[0].Value);
        Assert.Equal(actor.Name, actors[0].Name);
        Assert.Equal(12, data.AgeRating);
        Assert.Equal("https://example.com/inception.jpg", data.PosterUrl);
    }
}
