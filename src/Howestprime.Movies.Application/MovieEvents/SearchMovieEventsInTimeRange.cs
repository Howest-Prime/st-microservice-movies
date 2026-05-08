using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record SearchMovieEventsInTimeRangeInput(
    DateTimeOffset From,
    DateTimeOffset To
);

public sealed class SearchMovieEventsInTimeRange(
    IMovieEventQuery movieEventQuery
) : IUseCase<SearchMovieEventsInTimeRangeInput, Optional<IReadOnlyList<MovieEventData>>>
{
    public Task<Optional<IReadOnlyList<MovieEventData>>> Execute(SearchMovieEventsInTimeRangeInput input)
    {
        if (input.From > input.To)
        {
            throw new ArgumentException("'From' must be before 'To'.");
        }

        return movieEventQuery.Fetch(
            MovieEventDataFilters.InTimeRange(input.From, input.To)
        );
    }
}