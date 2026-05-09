using Aornis;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Data.Room;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using UnitTests.Application.Shared;

namespace UnitTests.Application.MovieEvents;

public sealed class SearchMovieEventsInTimeRangeTests
{
    [Fact]
    public async Task Execute_WithValidRange_ShouldReturnEventsInRange()
    {
        // Arrange
        DateTime from = DateTime.UtcNow.Date.AddDays(1);
        DateTime to = from.AddDays(2);
        MovieEventData inRange = new()
        {
            Id = Guid.NewGuid(),
            ShowTime = from.AddHours(15),
            Capacity = 100,
            Movie = new MovieData
            {
                Id = Guid.NewGuid(),
                Title = "Inception"
            },
            Room = new RoomData
            {
                Id = Guid.NewGuid(),
                Name = "Room A",
                Capacity = 100
            }
        };
        MovieEventData outOfRange = inRange with
        {
            Id = Guid.NewGuid(),
            ShowTime = to.AddDays(1).AddHours(15)
        };
        SearchMovieEventsInTimeRange useCase = new(new FakeMovieEventQuery([inRange, outOfRange]));

        // Act
        Optional<IReadOnlyList<MovieEventData>> result = await useCase.Execute(
            new SearchMovieEventsInTimeRangeInput(from, to));

        // Assert
        Assert.True(result.HasValue);
        MovieEventData movieEvent = Assert.Single(result.Value);
        Assert.Equal(inRange.Id, movieEvent.Id);
    }

    [Fact]
    public async Task Execute_WithFromEqualsTo_ShouldReturnEventsAtBoundary()
    {
        // Arrange
        DateTime boundary = DateTime.UtcNow.Date.AddDays(1).AddHours(15);
        MovieEventData atBoundary = new()
        {
            Id = Guid.NewGuid(),
            ShowTime = boundary,
            Capacity = 100,
            Movie = new MovieData { Id = Guid.NewGuid(), Title = "Inception" },
            Room = new RoomData { Id = Guid.NewGuid(), Name = "Room A", Capacity = 100 }
        };
        SearchMovieEventsInTimeRange useCase = new(new FakeMovieEventQuery([atBoundary]));

        // Act
        Optional<IReadOnlyList<MovieEventData>> result = await useCase.Execute(
            new SearchMovieEventsInTimeRangeInput(boundary, boundary));

        // Assert
        Assert.True(result.HasValue);
        MovieEventData movieEvent = Assert.Single(result.Value);
        Assert.Equal(atBoundary.Id, movieEvent.Id);
    }

    [Fact]
    public async Task Execute_WithNoEventsInRange_ShouldReturnEmpty()
    {
        // Arrange
        DateTime from = DateTime.UtcNow.Date.AddDays(1);
        DateTime to = from.AddDays(1);
        MovieEventData outOfRange = new()
        {
            Id = Guid.NewGuid(),
            ShowTime = to.AddDays(5),
            Capacity = 100,
            Movie = new MovieData { Id = Guid.NewGuid(), Title = "Inception" },
            Room = new RoomData { Id = Guid.NewGuid(), Name = "Room A", Capacity = 100 }
        };
        SearchMovieEventsInTimeRange useCase = new(new FakeMovieEventQuery([outOfRange]));

        // Act
        Optional<IReadOnlyList<MovieEventData>> result = await useCase.Execute(
            new SearchMovieEventsInTimeRangeInput(from, to));

        // Assert
        Assert.False(result.HasValue);
    }

    [Fact]
    public void Execute_WithFromAfterTo_ShouldThrow()
    {
        // Arrange
        DateTime from = DateTime.UtcNow.Date.AddDays(3);
        DateTime to = DateTime.UtcNow.Date.AddDays(1);
        SearchMovieEventsInTimeRange useCase = new(new FakeMovieEventQuery([]));

        // Act
        Action act = () => useCase.Execute(new SearchMovieEventsInTimeRangeInput(from, to));

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    private sealed class FakeMovieEventQuery(IReadOnlyList<MovieEventData> movieEvents) : IMovieEventQuery
    {
        public Task<Optional<IReadOnlyList<MovieEventData>>> Fetch(
            System.Linq.Expressions.Expression<Func<MovieEventData, bool>>? filter = null
        )
        {
            IReadOnlyList<MovieEventData> results = filter == null
                ? movieEvents
                : movieEvents.Where(filter.Compile()).ToList();

            return Task.FromResult(results.Count == 0 ? Optional.Empty : Optional.Of(results));
        }
    }
}
