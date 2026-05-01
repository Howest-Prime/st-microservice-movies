

using Aornis;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Howestprime.Movies.Infrastructure.WebApi.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;

public sealed record GetHowestPrimeScheduleRequest(
    [FromServices] IUseCase<SearchMovieEventsInTimeRangeInput, Optional<IReadOnlyList<MovieEventData>>> UseCase
);


public static class GetHowestPrimeScheduleController
{
    public static async Task<IResult> Invoke([AsParameters] GetHowestPrimeScheduleRequest getHowestPrimeScheduleRequest)
    {
        DateTime now = DateTime.UtcNow;
        DateTime inTwoWeeks = now.AddDays(14);

        Optional<IReadOnlyList<MovieEventData>> movieEvents = await getHowestPrimeScheduleRequest.UseCase.Execute(
            new SearchMovieEventsInTimeRangeInput(now, inTwoWeeks)
        );

        return Results.Ok(
            movieEvents.HasValue
                ? movieEvents.Value.ToHowestPrimeScheduleResponse()
                : Enumerable.Empty<MovieResponse>()
        );
    }
}