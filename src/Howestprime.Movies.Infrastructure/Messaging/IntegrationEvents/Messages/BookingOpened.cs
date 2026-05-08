namespace Howestprime.Movies.Infrastructure.Messaging.IntegrationEvents.Messages;

public sealed record BookingOpened(
    Guid BookingId,
    Guid MovieId,
    Guid RoomId,
    DateTimeOffset ShowTime,
    int StandardVisitors,
    int DiscountVisitors,
    IReadOnlyList<Booking> Bookings
);


public sealed record Booking(
    Guid BookingId,
    Guid MovieId,
    Guid RoomId,
    DateTimeOffset ShowTime,
    int StandardVisitors,
    int DiscountVisitors
);