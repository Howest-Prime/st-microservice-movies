using Howestprime.Movies.Domain.Movies.MovieEvent.Events;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

namespace UnitTests.Domain.Movies.MovieEvent.Events;

public sealed class BookingOpenedTests
{
    [Fact]
    public void Create_WithValidInput_ShouldInitializeAllProperties()
    {
        // Arrange
        BookingId bookingId = new(Guid.NewGuid());
        Guid movieId = Guid.NewGuid();
        Guid roomId = Guid.NewGuid();
        DateTimeOffset showTime = DateTimeOffset.UtcNow.Date.AddDays(1).AddHours(15);
        IReadOnlyList<int> seatNumbers = [1, 2];

        // Act
        BookingOpened domainEvent = BookingOpened.Create(
            bookingId,
            movieId,
            roomId,
            showTime,
            standardVisitors: 1,
            discountVisitors: 1,
            seatNumbers
        );

        // Assert
        Assert.Equal(bookingId, domainEvent.BookingId);
        Assert.Equal(movieId, domainEvent.MovieId);
        Assert.Equal(roomId, domainEvent.RoomId);
        Assert.Equal(showTime, domainEvent.ShowTime);
        Assert.Equal(1, domainEvent.StandardVisitors);
        Assert.Equal(1, domainEvent.DiscountVisitors);
        Assert.Equal(seatNumbers, domainEvent.SeatNumbers);
        Assert.Equal("Howestprime.Movies.MovieEvent.BookingOpened", domainEvent.FQDN);
    }
}
