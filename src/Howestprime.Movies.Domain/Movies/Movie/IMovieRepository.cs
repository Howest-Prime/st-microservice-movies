using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Domain.Movies.Movie.Repositories;

public interface IMovieRepository: IRepository<Movie, MovieId>
{
}