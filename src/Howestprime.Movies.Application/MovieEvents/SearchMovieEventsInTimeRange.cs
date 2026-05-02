using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies.MovieEvent.Exceptions;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record SearchMovieEventsInTimeRangeInput(
    DateTime? From,
    DateTime? To
);

public sealed class SearchMovieEventsInTimeRange(
    IMovieEventQuery movieEventQuery
) : IUseCase<SearchMovieEventsInTimeRangeInput, Optional<IReadOnlyList<MovieEventData>>>
{
    public Task<Optional<IReadOnlyList<MovieEventData>>> Execute(SearchMovieEventsInTimeRangeInput input)
    {

        DateTime now = DateTime.UtcNow;

        DateTime from = (input.From ?? now).ToUniversalTime();
        DateTime to   = (input.To ?? now).ToUniversalTime();

        if (from > to)
        {
            throw new InvalidTimeRangeException(from, to);
        }

        return movieEventQuery.Fetch(
            MovieEventDataFilters.InTimeRange(from, to)
        );
    }
}