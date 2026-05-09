using Aornis;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Howestprime.Movies.Infrastructure.WebApi.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;

public sealed record GetMovieEventsByMonthRequest(
    [FromQuery] int Year,
    [FromQuery] int Month,
    [FromServices] IUseCase<SearchMovieEventsInTimeRangeInput, Optional<IReadOnlyList<MovieEventData>>> UseCase
);

public static class FindAllMovieEventsByMonthController
{
    public static async Task<Results<Ok<IReadOnlyList<MovieEventResponse>>, BadRequest>> Invoke([AsParameters] GetMovieEventsByMonthRequest request)
    {
        (DateTimeOffset startOfMonth, DateTimeOffset endOfMonth) = GetMonthRange(request.Year, request.Month);

        Optional<IReadOnlyList<MovieEventData>> movieEvents = await request.UseCase.Execute(
            new SearchMovieEventsInTimeRangeInput(startOfMonth, endOfMonth)
        );

        return TypedResults.Ok(movieEvents.Value.ToMovieEventResponseList());

    }

    private static (DateTimeOffset Start, DateTimeOffset End) GetMonthRange(int year, int month)
    {
        DateTimeOffset start = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);
        DateTimeOffset end = new DateTimeOffset(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, TimeSpan.Zero);

        return (start, end);
    }
}