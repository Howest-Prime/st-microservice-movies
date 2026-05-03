using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Data.Room;

namespace UnitTests.Application.Contracts.Data;

public sealed class MovieEventDataTests
{
    [Fact]
    public void Create_WithDefaults_ShouldInitializeNestedData()
    {
        // Arrange
        MovieEventData data = new();

        // Act
        MovieData movie = data.Movie;
        RoomData room = data.Room;

        // Assert
        Assert.Equal(Guid.Empty, data.Id);
        Assert.Equal(default, data.ShowTime);
        Assert.Equal(0, data.Capacity);
        Assert.NotNull(movie);
        Assert.NotNull(room);
    }

    [Fact]
    public void Create_WithValues_ShouldSetAllProperties()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        DateTime showTime = DateTime.UtcNow.AddDays(1);
        MovieData movie = new() { Id = Guid.NewGuid(), Title = "Inception" };
        RoomData room = new() { Id = Guid.NewGuid(), Name = "Room A", Capacity = 100 };
        MovieEventData data = new()
        {
            Id = id,
            ShowTime = showTime,
            Capacity = 100,
            Movie = movie,
            Room = room
        };

        // Act
        MovieData resultMovie = data.Movie;
        RoomData resultRoom = data.Room;

        // Assert
        Assert.Equal(id, data.Id);
        Assert.Equal(showTime, data.ShowTime);
        Assert.Equal(100, data.Capacity);
        Assert.Equal(movie.Id, resultMovie.Id);
        Assert.Equal(room.Id, resultRoom.Id);
    }
}
