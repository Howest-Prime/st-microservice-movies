using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using System.Linq.Expressions;

namespace Howestprime.Movies.Application.Contracts.Ports;

public interface ISearchMovieCatalogQuery
{
    public  Task<Optional<IReadOnlyList<MovieData>>> Fetch(
        Expression<Func<MovieData, bool>>? filter = null
    );
}

public interface ISearchMovieEventsInTimeRangeQuery
{
    public Task<IReadOnlyList<MovieEventData>> Fetch(
        Expression<Func<MovieEventData, bool>>? filter = null
    );
}
