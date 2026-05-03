using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Application.Movies.Exceptions;
using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using UnitTests.Application.Shared;

namespace UnitTests.Application.Movies;

public sealed class ChangeMovieDetailsTests : ApplicationTestBase
{
    private readonly ChangeMovieDetails _changeMovieDetails;

    public ChangeMovieDetailsTests()
    {
        _changeMovieDetails = new ChangeMovieDetails(UnitOfWork, CreateLogger<ChangeMovieDetails>());
    }

    [Fact]
    public async Task Execute_WithExistingMovie_ShouldUpdateDetails()
    {
        // Arrange
        Movie movie = Movie.Create(
            EntityId.New<MovieId>(),
            MovieTitle.Create("Inception"),
            MovieDescription.Create("A skilled thief steals corporate secrets through dream-sharing technology."),
            [MovieGenre.Create("Sci-Fi"), MovieGenre.Create("Thriller")],
            [Actor.Create("Leonardo DiCaprio"), Actor.Create("Joseph Gordon-Levitt")],
            MovieReleaseYear.Create(2010),
            MovieDuration.Create(148),
            AgeRating.From(12),
            PosterUrl.Create("https://example.com/inception.jpg"));
        await MovieRepository.Save(movie);
        ChangeMovieDetailsInput input = new(
            movie.Id.Value,
            "Interstellar",
            "Explorers travel through a wormhole in space.",
            2014,
            ["Sci-Fi", "Drama"],
            ["Matthew McConaughey", "Anne Hathaway"],
            9,
            "https://example.com/interstellar.jpg");

        // Act
        ChangeMovieDetailsOutput output = await _changeMovieDetails.Execute(input);

        // Assert
        Movie updatedMovie = (await MovieRepository.ById(new MovieId(output.MovieId))).Value;
        Assert.Equal(input.Title, output.Title);
        Assert.Equal(input.Description, updatedMovie.Description.Value);
        Assert.Equal(input.ReleaseYear, updatedMovie.ReleaseYear.Value);
        Assert.Equal(input.Genre, updatedMovie.Genres.Select(genre => genre.Value).ToArray());
        Assert.Equal(input.Actors, updatedMovie.Actors.Select(actor => actor.Name).ToArray());
        Assert.Equal("Nine", output.AgeRating);
        Assert.Equal(input.PosterUrl, updatedMovie.PosterUrl.Value);
    }

    [Fact]
    public async Task Execute_WithMissingMovie_ShouldThrow()
    {
        // Arrange
        ChangeMovieDetailsInput input = new(
            Guid.NewGuid(),
            "Interstellar",
            "Explorers travel through a wormhole in space.",
            2014,
            ["Sci-Fi"],
            ["Matthew McConaughey"],
            9,
            "https://example.com/interstellar.jpg");

        // Act
        Task act = _changeMovieDetails.Execute(input);

        // Assert
        await Assert.ThrowsAsync<MovieNotFoundException>(() => act);
    }
}
