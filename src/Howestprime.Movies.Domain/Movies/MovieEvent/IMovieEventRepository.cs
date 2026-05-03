

using Aornis;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent;

public interface IMovieEventRepository : IRepository<MovieEvent, MovieEventId>
{
    Task<MovieEvent?> ByRoomIdAndShowTime(Guid roomId, MovieEventShowTime showTime);

    Task<MovieEvent?> ByBookingId(BookingId bookingId);
}