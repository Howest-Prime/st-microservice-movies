

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
    [FromServices] IUseCase<ScheduleMovieEventInput, ScheduleMovieEventOutput> useCase
);
public static class ScheduleMovieEventController
{
    public static async Task<IResult> Invoke(
        [FromBody] ScheduleMovieEventBody body,
        [FromServices] IUseCase<ScheduleMovieEventInput, ScheduleMovieEventOutput> useCase
    )
    {
        ScheduleMovieEventInput input = new ScheduleMovieEventInput(
            body.MovieId,
            body.RoomId,
            body.ShowTime
        );
        ScheduleMovieEventOutput output = await useCase.Execute(input);
        return Results.Created($"/movieevents/{output.MovieEventID}", output);
    }
}

