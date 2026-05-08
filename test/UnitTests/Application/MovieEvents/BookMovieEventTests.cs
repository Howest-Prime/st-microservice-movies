using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Shared.Exceptions;
using UnitTests.Application.Shared;

namespace UnitTests.Application.MovieEvents;

public sealed class BookMovieEventTests : ApplicationTestBase
{
    private readonly BookMovieEvent _bookMovieEvent;

    public BookMovieEventTests()
    {
        _bookMovieEvent = new BookMovieEvent(UnitOfWork, CreateLogger<BookMovieEvent>());
    }

    [Fact]
    public async Task Execute_WithExistingMovieEvent_ShouldCreateBooking()
    {
        // Arrange
        MovieEvent movieEvent = MovieEvent.Create(
            EntityId.New<MovieEventId>(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            MovieEventShowTime.Create(NextShowTime()),
            Capacity.Create(10));
        await MovieEventRepository.Save(movieEvent);
        BookMovieEventInput input = new(movieEvent.Id.Value, StandardVisitors: 2, DiscountVisitors: 1);

        // Act
        BookMovieEventOutput output = await _bookMovieEvent.Execute(input);

        // Assert
        MovieEvent updatedMovieEvent = (await MovieEventRepository.ById(movieEvent.Id)).Value;
        Booking booking = Assert.Single(updatedMovieEvent.Bookings);
        Assert.Equal(output.BookingId, booking.Id.Value);
        Assert.Equal(3, updatedMovieEvent.Visitors);
    }

    [Fact]
    public async Task Execute_WithMissingMovieEvent_ShouldThrow()
    {
        // Arrange
        BookMovieEventInput input = new(Guid.NewGuid(), StandardVisitors: 1, DiscountVisitors: 0);

        // Act
        Task act = _bookMovieEvent.Execute(input);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => act);
    }

    [Fact]
    public async Task Execute_WhenCapacityExceeded_ShouldThrow()
    {
        // Arrange
        MovieEvent movieEvent = MovieEvent.Create(
            EntityId.New<MovieEventId>(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            MovieEventShowTime.Create(NextShowTime()),
            Capacity.Create(2));
        await MovieEventRepository.Save(movieEvent);
        BookMovieEventInput input = new(movieEvent.Id.Value, StandardVisitors: 2, DiscountVisitors: 1);

        // Act
        Task act = _bookMovieEvent.Execute(input);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(() => act);
    }

    private static DateTime NextShowTime()
    {
        DateTime showTime = DateTime.Now.Date.AddDays(1).AddHours(15);
        return showTime <= DateTime.Now ? showTime.AddDays(1) : showTime;
    }
}
