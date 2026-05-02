
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;

public sealed record BookEventRequestBody(
    int StandardVisitors,
    int DiscountVisitors
);

public sealed record BookEventRequest(
    [FromQuery] Guid MovieEventId,
    [FromBody] BookEventRequestBody Body,
    [FromServices] IUseCase<BookMovieEventInput, BookMovieEventOutput> UseCase
);

public  static class BookAMovieEventController
{
    public static async Task<IResult> Invoke([AsParameters] BookEventRequest request)
    {
        BookMovieEventInput input = new BookMovieEventInput(
            request.MovieEventId,
            request.Body.StandardVisitors,
            request.Body.DiscountVisitors
        );

        BookMovieEventOutput output = await request.UseCase.Execute(input);
        
        return Results.Created($"/bookings/{output.BookingId}", output);
    }
}