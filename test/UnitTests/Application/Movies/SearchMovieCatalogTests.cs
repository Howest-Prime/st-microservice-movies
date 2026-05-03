using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;
using UnitTests.Application.Shared;

namespace UnitTests.Application.Movies;

public sealed class SearchMovieCatalogTests
{
    [Fact]
    public async Task Execute_WithTitleAndGenre_ShouldReturnMatchingMovies()
    {
        // Arrange
        MovieData inception = new()
        {
            Id = Guid.NewGuid(),
            Title = "Inception",
            Description = "A skilled thief steals corporate secrets through dream-sharing technology.",
            ReleaseYear = 2010,
            Duration = 148,
            Genres = [new GenreData { Value = "Sci-Fi" }],
            Actors = [new ActorData { Name = "Leonardo DiCaprio" }],
            AgeRating = 12,
            PosterUrl = "https://example.com/inception.jpg"
        };
        MovieData drama = inception with
        {
            Id = Guid.NewGuid(),
            Title = "A Quiet Drama",
            Genres = [new GenreData { Value = "Drama" }]
        };
        FakeSearchMovieCatalogQuery query = new([inception, drama]);
        FakeAuthorizationService authorizationService = new();
        SearchMovieCatalog useCase = new(query, authorizationService);

        // Act
        Optional<IReadOnlyList<MovieData>> result = await useCase.Execute(
            new SearchMovieCatalogInput("Admin", "incep", ["Sci-Fi"]));

        // Assert
        Assert.True(result.HasValue);
        MovieData movie = Assert.Single(result.Value);
        Assert.Equal(inception.Id, movie.Id);
        Assert.Equal("Admin", authorizationService.LastRole);
        Assert.Equal(nameof(SearchMovieCatalog), authorizationService.LastUseCaseName);
    }

    [Fact]
    public async Task Execute_WithInvalidGenre_ShouldThrow()
    {
        // Arrange
        SearchMovieCatalog useCase = new(new FakeSearchMovieCatalogQuery([]), new FakeAuthorizationService());

        // Act
        Task act = useCase.Execute(new SearchMovieCatalogInput("Admin", null, [" "]));

        // Assert
        await Assert.ThrowsAsync<InvalidMovieGenreException>(() => act);
    }

    private sealed class FakeSearchMovieCatalogQuery(IReadOnlyList<MovieData> movies) : ISearchMovieCatalogQuery
    {
        public Task<Optional<IReadOnlyList<MovieData>>> Fetch(
            System.Linq.Expressions.Expression<Func<MovieData, bool>>? filter = null
        )
        {
            IReadOnlyList<MovieData> results = filter == null
                ? movies
                : movies.Where(filter.Compile()).ToList();

            return Task.FromResult(results.Count == 0 ? Optional.Empty : Optional.Of(results));
        }
    }

    private sealed class FakeAuthorizationService : IAuthorizationService
    {
        public string? LastRole { get; private set; }
        public string? LastUseCaseName { get; private set; }

        public Task Authorize(string role, string useCaseName)
        {
            LastRole = role;
            LastUseCaseName = useCaseName;
            return Task.CompletedTask;
        }
    }
}
