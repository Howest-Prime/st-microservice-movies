using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Domain.Movies.Movie.Repositories;

public interface IMovieRepository: IRepository<Movie, MovieId>
{
    Task AddAsync(Movie movie);
    Task<Movie?> ByIdAsync(MovieId id);
    Task UpdateAsync(Movie movie);
    Task DeleteAsync(MovieId id);
}