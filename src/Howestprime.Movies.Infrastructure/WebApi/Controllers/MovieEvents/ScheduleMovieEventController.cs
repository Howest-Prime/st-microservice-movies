

using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;

public sealed record ScheduleMovieEventBody(
    Guid MovieId,
    Guid RoomId,
    DateTime ShowTime
);

public sealed record ScheduleMovieEventRequest(
    [FromBody] ScheduleMovieEventBody Body,
    [FromServices] IUseCase<ScheduleMovieEventInput, ScheduleMovieEventOutput> UseCase
);
public static class ScheduleMovieEventController
{
    public static async Task<IResult> Invoke(
        [AsParameters] ScheduleMovieEventRequest request
    )
    {
        ScheduleMovieEventInput input = new ScheduleMovieEventInput(
            request.Body.MovieId,
            request.Body.RoomId,
            request.Body.ShowTime
        );
        ScheduleMovieEventOutput output = await request.UseCase.Execute(input);
        return Results.Created($"/movieevents/{output.MovieEventId}", output);
    }
}

