using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.Events;

public sealed class BookingOpened : MovieEventDomainEvent
{
    public BookingID BookingId { get; private init; }
    public Guid MovieId { get; private init; }
    public Guid RoomId { get; private init; }
    public MovieEventShowTime ShowTime { get; private init; }
    public int StandardVisitors { get; private init; }
    public int DiscountVisitors { get; private init; }
    public IReadOnlyList<Booking> Bookings { get; private init; }
    


    private BookingOpened(
        BookingID bookingId,
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime,
        int standardVisitors,
        int discountVisitors,
        IReadOnlyList<Booking> bookings
    ) : base(nameof(BookingOpened) )
    {
        BookingId = bookingId;
        MovieId = movieId;
        RoomId = roomId;
        ShowTime = showTime;
        StandardVisitors = standardVisitors;
        DiscountVisitors = discountVisitors;
        Bookings = bookings;
    }

    public static BookingOpened Create(
        BookingID bookingId,
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime,
        int standardVisitors,
        int discountVisitors,
        IReadOnlyList<Booking> bookings
    )
    {
        return new BookingOpened(
            bookingId,
            movieId,
            roomId,
            showTime,
            standardVisitors,
            discountVisitors,
            bookings
        );
    }
    
}