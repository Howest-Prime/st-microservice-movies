
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents.Exceptions;
using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Domain.MovieEvent.ValueObject;
using Howestprime.Movies.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record BookMovieEventInput(
    Guid MovieEventId,
    int StandardVisitors,
    int DiscountVisitors
);

public sealed record BookMovieEventOutput(
    Guid BookingId
);

public sealed class BookMovieEvent(
    IUnitOfWork UoW,
    ILogger<BookMovieEvent> logger
) : IUseCase<BookMovieEventInput, BookMovieEventOutput>
{
    public async Task<BookMovieEventOutput> Execute(BookMovieEventInput input)
    {
        MovieEventID movieEventId = EntityId.New<MovieEventID>(input.MovieEventId);
        MovieEvent movieEvent = (await UoW.Repo<IMovieEventRepository>().ById(movieEventId))
                                .OrElseThrow(()=> throw new MovieEventNotfoundException(movieEventId.Value));
        
        BookingID bookingId = movieEvent.Book(input.StandardVisitors, input.DiscountVisitors);

        await UoW.Save<IMovieEventRepository>(movieEvent);
        await UoW.Do();

        logger.LogInformation($"Booking with id: {bookingId} added to Movie Event with id: {movieEvent.Id}");

        return new BookMovieEventOutput(bookingId.Value);
    }
}