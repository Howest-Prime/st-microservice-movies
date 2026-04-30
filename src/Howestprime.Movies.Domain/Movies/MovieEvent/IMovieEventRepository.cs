

using Aornis;
using Howestprime.Movies.Domain.MovieEvent.ValueObject;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent;

public interface IMovieEventRepository : IRepository<MovieEvent, MovieEventID>
{
    Task<bool> ByIdAndShowTime(MovieId movieId, MovieEventShowTime showTime);
}