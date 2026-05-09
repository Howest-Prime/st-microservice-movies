using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Shared.Exceptions;
using UnitTests.Application.Shared;

namespace UnitTests.Application.MovieEvents;

public sealed class CloseBookingTests : ApplicationTestBase
{
    private readonly CloseBooking _closeBooking;

    public CloseBookingTests()
    {
        _closeBooking = new CloseBooking(UnitOfWork, CreateLogger<CloseBooking>());
    }

    [Theory]
    [InlineData("payment successful", PaymentStatus.Success, 1)]
    [InlineData("payment failed", PaymentStatus.Failed, 0)]
    [InlineData("PAYMENT SUCCESSFUL", PaymentStatus.Success, 1)]
    [InlineData("PAYMENT FAILED", PaymentStatus.Failed, 0)]
    [InlineData("Payment Successful", PaymentStatus.Success, 1)]
    [InlineData("Payment Failed", PaymentStatus.Failed, 0)]
    public async Task Execute_WithValidReason_ShouldCloseBooking(
        string reason,
        PaymentStatus expectedPaymentStatus,
        int expectedSeatCount
    )
    {
        // Arrange
        MovieEvent movieEvent = MovieEvent.Create(
            EntityId.New<MovieEventId>(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            MovieEventShowTime.Create(NextShowTime()),
            Capacity.Create(10));
        BookingId bookingId = movieEvent.Book(standardVisitors: 1, discountVisitors: 0);
        await MovieEventRepository.Save(movieEvent);

        // Act
        await _closeBooking.Execute(new CloseBookingInput(bookingId.Value, reason));

        // Assert
        Booking booking = movieEvent.Bookings.Single();
        Assert.Equal(BookingStatus.Closed, booking.BookingStatus);
        Assert.Equal(expectedPaymentStatus, booking.PaymentStatus);
        Assert.Equal(expectedSeatCount, booking.SeatNumbers.Count);
    }

    [Fact]
    public async Task Execute_WithUnknownBooking_ShouldThrow()
    {
        // Arrange
        CloseBookingInput input = new(Guid.NewGuid(), "payment successful");

        // Act
        Task act = _closeBooking.Execute(input);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => act);
    }

    [Fact]
    public async Task Execute_WithInvalidReason_ShouldThrow()
    {
        // Arrange
        MovieEvent movieEvent = MovieEvent.Create(
            EntityId.New<MovieEventId>(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            MovieEventShowTime.Create(NextShowTime()),
            Capacity.Create(10));
        BookingId bookingId = movieEvent.Book(standardVisitors: 1, discountVisitors: 0);
        await MovieEventRepository.Save(movieEvent);

        // Act
        Task act = _closeBooking.Execute(new CloseBookingInput(bookingId.Value, "cancelled"));

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(() => act);
    }

    private static DateTime NextShowTime()
    {
        DateTime showTime = DateTime.Now.Date.AddDays(1).AddHours(15);
        return showTime <= DateTime.Now ? showTime.AddDays(1) : showTime;
    }
}
