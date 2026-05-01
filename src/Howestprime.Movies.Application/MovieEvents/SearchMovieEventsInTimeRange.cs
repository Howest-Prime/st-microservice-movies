using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record SearchMovieEventsInTimeRangeInput(
    DateTime From,
    DateTime To
);

public sealed class SearchMovieEventsInTimeRange(
    ISearchMovieEventsInTimeRangeQuery searchMovieEventsInTimeRangeQuery
) : IUseCase<SearchMovieEventsInTimeRangeInput, IReadOnlyList<MovieEventData>>
{
    public Task<IReadOnlyList<MovieEventData>> Execute(SearchMovieEventsInTimeRangeInput input)
    {
        return searchMovieEventsInTimeRangeQuery.Fetch(
            MovieEventDataFilters.InTimeRange(input.From, input.To)
        );
    }
}