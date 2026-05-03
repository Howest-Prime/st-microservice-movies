using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Application.Movies.Exceptions;
using UnitTests.Application.Shared;

namespace UnitTests.Application.Movies;

public sealed class FindMovieByIdTests
{
    [Fact]
    public async Task Execute_WithExistingMovie_ShouldReturnMovieData()
    {
        // Arrange
        MovieData movie = new()
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
        FakeSearchMovieCatalogQuery query = new([movie]);
        FakeAuthorizationService authorizationService = new();
        FindMovieById useCase = new(query, authorizationService);

        // Act
        Optional<MovieData> result = await useCase.Execute(new FindMovieByIdInput("Admin", movie.Id));

        // Assert
        Assert.True(result.HasValue);
        Assert.Equal(movie.Id, result.Value.Id);
        Assert.Equal("Admin", authorizationService.LastRole);
        Assert.Equal(nameof(FindMovieById), authorizationService.LastUseCaseName);
    }

    [Fact]
    public async Task Execute_WithMissingMovie_ShouldThrow()
    {
        // Arrange
        FindMovieById useCase = new(new FakeSearchMovieCatalogQuery([]), new FakeAuthorizationService());

        // Act
        Task act = useCase.Execute(new FindMovieByIdInput("Admin", Guid.NewGuid()));

        // Assert
        await Assert.ThrowsAsync<MovieNotFoundException>(() => act);
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
