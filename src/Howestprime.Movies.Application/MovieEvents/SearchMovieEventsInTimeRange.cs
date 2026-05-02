using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record SearchMovieEventsInTimeRangeInput(
    DateTime From,
    DateTime To
);

public sealed class SearchMovieEventsInTimeRange(
    IMovieEventQuery movieEventQuery
) : IUseCase<SearchMovieEventsInTimeRangeInput, Optional<IReadOnlyList<MovieEventData>>>
{
    public Task<Optional<IReadOnlyList<MovieEventData>>> Execute(SearchMovieEventsInTimeRangeInput input)
    {
        return movieEventQuery.Fetch(
            MovieEventDataFilters.InTimeRange(input.From, input.To)
        );
    }
}