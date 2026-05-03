using Aornis;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

namespace UnitTests.Application.Mocks;

public sealed class InMemoryMovieEventRepository : IMovieEventRepository
{
    private readonly HashSet<MovieEvent> _movieEvents = [];

    public Task<MovieEvent?> ByBookingId(BookingId bookingId)
    {
        return Task.FromResult(
            _movieEvents.FirstOrDefault(movieEvent =>
                movieEvent.Bookings.Any(booking => booking.Id.Equals(bookingId)))
        );
    }

    public Task<Optional<MovieEvent>> ById(MovieEventId id)
    {
        return Task.FromResult(
            Optional.Of(_movieEvents.FirstOrDefault(movieEvent => movieEvent.Id.Equals(id)))
        );
    }

    public Task<MovieEvent?> ByRoomIdAndShowTime(Guid roomId, MovieEventShowTime showTime)
    {
        return Task.FromResult(
            _movieEvents.FirstOrDefault(movieEvent =>
                movieEvent.RoomId == roomId && movieEvent.ShowTime == showTime)
        );
    }

    public Task<bool> Exists(MovieEventId id)
    {
        return Task.FromResult(
            _movieEvents.Any(movieEvent => movieEvent.Id.Equals(id))
        );
    }

    public Task Remove(MovieEvent aggregateRoot)
    {
        return Task.FromResult(
            _movieEvents.Remove(aggregateRoot)
        );
    }

    public Task Save(MovieEvent aggregateRoot)
    {
        _movieEvents.Add(aggregateRoot);
        return Task.CompletedTask;
    }
}
