

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

public sealed record GetHowestPrimeScheduleRequest(
    [FromServices] IUseCase<SearchMovieEventsInTimeRangeInput, Optional<IReadOnlyList<MovieEventData>>> UseCase
);


public static class GetHowestPrimeScheduleController
{
    public static async Task<Results<Ok<IReadOnlyList<HowestPrimeScheduleResponse>>, NotFound>> Invoke([AsParameters] GetHowestPrimeScheduleRequest getHowestPrimeScheduleRequest)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset inTwoWeeks = now.AddDays(14);

        Optional<IReadOnlyList<MovieEventData>> movieEvents = await getHowestPrimeScheduleRequest.UseCase.Execute(
            new SearchMovieEventsInTimeRangeInput(now, inTwoWeeks)
        );

        if (!movieEvents.HasValue)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(movieEvents.Value.ToHowestPrimeScheduleResponseList());
    }
}