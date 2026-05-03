using Aornis;
using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Application.Mocks;

public sealed class InMemoryMovieRepository : IMovieRepository
{
    private readonly HashSet<Movie> _movies = [];

    public Task<Optional<Movie>> ById(MovieId id)
    {
        return Task.FromResult(
            Optional.Of(_movies.FirstOrDefault(movie => movie.Id.Equals(id)))
        );
    }

    public Task<bool> Exists(MovieId id)
    {
        return Task.FromResult(
            _movies.Any(movie => movie.Id.Equals(id))
        );
    }

    public Task Remove(Movie aggregateRoot)
    {
        return Task.FromResult(
            _movies.Remove(aggregateRoot)
        );
    }

    public Task Save(Movie aggregateRoot)
    {
        _movies.Add(aggregateRoot);
        return Task.CompletedTask;
    }
}
