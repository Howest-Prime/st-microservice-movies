using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.Exceptions;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

namespace UnitTests.Domain.Movies.MovieEvent;

public sealed class BookingTests
{
    [Fact]
    public void Create_WithVisitors_ShouldInitializeOpenPendingBooking()
    {
        // Arrange
        BookingId id = new(Guid.NewGuid());

        // Act
        Booking booking = Booking.Create(id, standardVisitors: 2, discountVisitors: 1, startSeatNumber: 5);

        // Assert
        Assert.Equal(id, booking.Id);
        Assert.Equal(BookingStatus.Open, booking.BookingStatus);
        Assert.Equal(PaymentStatus.Pending, booking.PaymentStatus);
        Assert.Equal(3, booking.TotalVisitors);
        Assert.Equal(["5", "6", "7"], booking.SeatNumbers);
    }

    [Fact]
    public void Create_WithoutVisitors_ShouldThrow()
    {
        // Arrange
        BookingId id = new(Guid.NewGuid());

        // Act
        Action act = () => Booking.Create(id, standardVisitors: 0, discountVisitors: 0, startSeatNumber: 1);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Close_WithSuccessfulPayment_ShouldCloseAndKeepSeatNumbers()
    {
        // Arrange
        Booking booking = Booking.Create(new BookingId(Guid.NewGuid()), 1, 1, 1);

        // Act
        Booking closedBooking = booking.Close(PaymentStatus.Success);

        // Assert
        Assert.Same(booking, closedBooking);
        Assert.Equal(BookingStatus.Closed, booking.BookingStatus);
        Assert.Equal(PaymentStatus.Success, booking.PaymentStatus);
        Assert.Equal(["1", "2"], booking.SeatNumbers);
    }

    [Fact]
    public void Close_WithFailedPayment_ShouldCloseAndReleaseSeatNumbers()
    {
        // Arrange
        Booking booking = Booking.Create(new BookingId(Guid.NewGuid()), 1, 1, 1);

        // Act
        booking.Close(PaymentStatus.Failed);

        // Assert
        Assert.Equal(BookingStatus.Closed, booking.BookingStatus);
        Assert.Equal(PaymentStatus.Failed, booking.PaymentStatus);
        Assert.Empty(booking.SeatNumbers);
    }

    [Fact]
    public void Close_WhenAlreadyClosed_ShouldThrow()
    {
        // Arrange
        Booking booking = Booking.Create(new BookingId(Guid.NewGuid()), 1, 0, 1);
        booking.Close(PaymentStatus.Success);

        // Act
        Action act = () => booking.Close(PaymentStatus.Success);

        // Assert
        Assert.Throws<BookingAlreadyClosedException>(act);
    }
}
