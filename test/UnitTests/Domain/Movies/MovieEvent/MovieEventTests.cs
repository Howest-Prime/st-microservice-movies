using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.Events;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Shared.Exceptions;

namespace UnitTests.Domain.Movies.MovieEvent;

public sealed class MovieEventTests
{
    [Fact]
    public void Create_WithValidInput_ShouldInitializeState()
    {
        // Arrange
        MovieEventId id = new(Guid.NewGuid());
        Guid movieId = Guid.NewGuid();
        Guid roomId = Guid.NewGuid();
        MovieEventShowTime showTime = MovieEventShowTime.Create(ShowTimeInDays(1));
        Capacity capacity = Capacity.Create(100);

        // Act
        Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent movieEvent =
            Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent.Create(id, movieId, roomId, showTime, capacity);

        // Assert
        Assert.Equal(id, movieEvent.Id);
        Assert.Equal(movieId, movieEvent.MovieId);
        Assert.Equal(roomId, movieEvent.RoomId);
        Assert.Equal(showTime, movieEvent.ShowTime);
        Assert.Equal(capacity, movieEvent.Capacity);
        Assert.Empty(movieEvent.Bookings);
        Assert.Equal(0, movieEvent.Visitors);
    }

    [Fact]
    public void Book_WithAvailableCapacity_ShouldAddBookingAndRaiseDomainEvent()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        Guid roomId = Guid.NewGuid();
        MovieEventShowTime showTime = MovieEventShowTime.Create(ShowTimeInDays(1));
        Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent movieEvent =
            CreateMovieEvent(movieId, roomId, showTime, capacity: 10);

        // Act
        BookingId bookingId = movieEvent.Book(standardVisitors: 2, discountVisitors: 1);

        // Assert
        Booking booking = Assert.Single(movieEvent.Bookings);
        Assert.Equal(bookingId, booking.Id);
        Assert.Equal(3, movieEvent.Visitors);
        BookingOpened domainEvent = Assert.IsType<BookingOpened>(Assert.Single(movieEvent.DomainEvents));
        Assert.Equal(bookingId, domainEvent.BookingId);
        Assert.Equal(movieId, domainEvent.MovieId);
        Assert.Equal(roomId, domainEvent.RoomId);
        Assert.Equal(showTime, domainEvent.ShowTime);
        Assert.Equal(2, domainEvent.StandardVisitors);
        Assert.Equal(1, domainEvent.DiscountVisitors);
    }

    [Fact]
    public void Book_WhenCapacityWouldBeExceeded_ShouldThrow()
    {
        // Arrange
        Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent movieEvent =
            CreateMovieEvent(Guid.NewGuid(), Guid.NewGuid(), MovieEventShowTime.Create(ShowTimeInDays(1)), capacity: 2);

        // Act
        Action act = () => movieEvent.Book(standardVisitors: 2, discountVisitors: 1);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Book_WhenShowTimeIsBeyondReservationWindow_ShouldThrow()
    {
        // Arrange
        Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent movieEvent =
            CreateMovieEvent(Guid.NewGuid(), Guid.NewGuid(), MovieEventShowTime.Create(ShowTimeInDays(15)), capacity: 10);

        // Act
        Action act = () => movieEvent.Book(standardVisitors: 1, discountVisitors: 0);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void CloseBooking_WithExistingBooking_ShouldCloseBooking()
    {
        // Arrange
        Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent movieEvent =
            CreateMovieEvent(Guid.NewGuid(), Guid.NewGuid(), MovieEventShowTime.Create(ShowTimeInDays(1)), capacity: 10);
        BookingId bookingId = movieEvent.Book(standardVisitors: 1, discountVisitors: 0);

        // Act
        Booking booking = movieEvent.CloseBooking(bookingId, PaymentStatus.Success);

        // Assert
        Assert.Equal(BookingStatus.Closed, booking.BookingStatus);
        Assert.Equal(PaymentStatus.Success, booking.PaymentStatus);
    }

    [Fact]
    public void CloseBooking_WithMissingBooking_ShouldThrow()
    {
        // Arrange
        Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent movieEvent =
            CreateMovieEvent(Guid.NewGuid(), Guid.NewGuid(), MovieEventShowTime.Create(ShowTimeInDays(1)), capacity: 10);

        // Act
        Action act = () => movieEvent.CloseBooking(new BookingId(Guid.NewGuid()), PaymentStatus.Success);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Fact]
    public void Create_ViaPrivateConstructor_ShouldInstantiate()
    {
        var movieEvent = (Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent)
            Activator.CreateInstance(typeof(Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent), nonPublic: true)!;
        Assert.NotNull(movieEvent);
    }

    [Fact]
    public void UpdateMovie_WithGuid_ShouldChangeMovieId()
    {
        // Arrange
        Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent movieEvent =
            CreateMovieEvent(Guid.NewGuid(), Guid.NewGuid(), MovieEventShowTime.Create(ShowTimeInDays(1)), capacity: 10);
        Guid newMovieId = Guid.NewGuid();

        // Act
        movieEvent.UpdateMovie(newMovieId);

        // Assert
        Assert.Equal(newMovieId, movieEvent.MovieId);
    }

  

    private static Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent CreateMovieEvent(
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime,
        int capacity
    )
    {
        return Howestprime.Movies.Domain.Movies.MovieEvent.MovieEvent.Create(
            new MovieEventId(Guid.NewGuid()),
            movieId,
            roomId,
            showTime,
            Capacity.Create(capacity));
    }

    private static DateTime ShowTimeInDays(int days)
    {
        return DateTime.Now.Date.AddDays(days).AddHours(15);
    }
}
