

using System.Globalization;
using Howestprime.Movies.Application.Contracts.Data;
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
    [FromServices] IUseCase<SearchMovieEventsInTimeRangeInput, IReadOnlyList<MovieEventData>> UseCase
);



public static class FindAllMovieEventsByMonthController
{
    public static async Task<IResult> Invoke( [AsParameters] GetMovieEventsByMonthRequest request)
    {
        
        (DateTime startOfMonth, DateTime endOfMonth) = GetMonthRange(request.Year, request.Month);

        IReadOnlyList<MovieEventData> movieEvents = await request.UseCase.Execute(
            new SearchMovieEventsInTimeRangeInput(startOfMonth, endOfMonth)
        );

        return Results.Ok(movieEvents.ToHowestPrimeScheduleResponse());
    }

    private static (DateTime Start, DateTime End) GetMonthRange(int year, int month)
    {
        DateTime start = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime end = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, DateTimeKind.Utc);

        return (start, end);
    }
}