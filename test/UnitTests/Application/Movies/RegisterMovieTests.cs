using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using UnitTests.Application.Shared;

namespace UnitTests.Application.Movies;

public sealed class RegisterMovieTests : ApplicationTestBase
{
    private readonly RegisterMovie _registerMovie;

    public RegisterMovieTests()
    {
        _registerMovie = new RegisterMovie(UnitOfWork, CreateLogger<RegisterMovie>());
    }

    [Fact]
    public async Task Execute_WithValidInput_ShouldCreateMovie()
    {
        // Arrange
        RegisterMovieInput input = new(
            "Inception",
            "A skilled thief steals corporate secrets through dream-sharing technology.",
            148,
            ["Sci-Fi", "Thriller"],
            2010,
            ["Leonardo DiCaprio", "Joseph Gordon-Levitt"],
            12,
            "https://example.com/inception.jpg");

        // Act
        RegisterMovieOutput output = await _registerMovie.Execute(input);

        // Assert
        Movie createdMovie = (await MovieRepository.ById(new MovieId(output.Id))).Value;
        Assert.Equal(input.Title, createdMovie.Title.Value);
        Assert.Equal(input.Description, createdMovie.Description.Value);
        Assert.Equal(input.Duration, createdMovie.Duration.Value);
        Assert.Equal(input.ReleaseYear, createdMovie.ReleaseYear.Value);
        Assert.Equal(input.Genres, createdMovie.Genres.Select(genre => genre.Value).ToList());
        Assert.Equal(input.Actors, createdMovie.Actors.Select(actor => actor.Name).ToList());
        Assert.True(await UnitOfWork.Repo<IMovieRepository>().Exists(new MovieId(output.Id)));
    }

    [Fact]
    public async Task Execute_WithInvalidTitle_ShouldThrow()
    {
        // Arrange
        RegisterMovieInput input = new(
            string.Empty,
            "A skilled thief steals corporate secrets through dream-sharing technology.",
            148,
            ["Sci-Fi", "Thriller"],
            2010,
            ["Leonardo DiCaprio", "Joseph Gordon-Levitt"],
            12,
            "https://example.com/inception.jpg");

        // Act
        Task act = _registerMovie.Execute(input);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(() => act);
    }
}
