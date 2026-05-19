using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.Events;

public sealed class BookingOpened : MovieEventDomainEvent
{
    public BookingId BookingId { get; private init; }
    public Guid MovieId { get; private init; }
    public Guid RoomId { get; private init; }
    public DateTimeOffset ShowTime { get; private init; }
    public int StandardVisitors { get; private init; }
    public int DiscountVisitors { get; private init; }
    public IReadOnlyList<int> SeatNumbers { get; private init; }

    private BookingOpened(
        BookingId bookingId,
        Guid movieId,
        Guid roomId,
        DateTimeOffset showTime,
        int standardVisitors,
        int discountVisitors,
        IReadOnlyList<int> bookingIds
    ) : base(nameof(BookingOpened))
    {
        BookingId = bookingId;
        MovieId = movieId;
        RoomId = roomId;
        ShowTime = showTime;
        StandardVisitors = standardVisitors;
        DiscountVisitors = discountVisitors;
        SeatNumbers = bookingIds;
    }

    public static BookingOpened Create(
        BookingId bookingId,
        Guid movieId,
        Guid roomId,
        DateTimeOffset showTime,
        int standardVisitors,
        int discountVisitors,
        IReadOnlyList<int> seatNumbers
    )
    {
        return new BookingOpened(
            bookingId,
            movieId,
            roomId,
            showTime,
            standardVisitors,
            discountVisitors,
            seatNumbers
        );
    }
}
