using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Shared.Exceptions;
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
    IUnitOfWork uow,
    ILogger<BookMovieEvent> logger
) : IUseCase<BookMovieEventInput, BookMovieEventOutput>
{
    public async Task<BookMovieEventOutput> Execute(BookMovieEventInput input)
    {
        MovieEventId movieEventId = EntityId.New<MovieEventId>(input.MovieEventId);
        MovieEvent? movieEvent = (await uow.Repo<IMovieEventRepository>().ById(movieEventId))
                                .OrElseThrow(()=> new NotFoundException($"Movie event with id {movieEventId.Value} not found."));
        
        BookingId bookingId = movieEvent.Book(input.StandardVisitors, input.DiscountVisitors);

        await uow.Save<IMovieEventRepository>(movieEvent);
        await uow.Do();

        logger.LogInformation($"Booking with id: {bookingId} added to Movie Event with id: {movieEvent.Id}");

        return new BookMovieEventOutput(bookingId.Value);
    }
}