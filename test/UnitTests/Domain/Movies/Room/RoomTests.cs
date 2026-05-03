using Howestprime.Movies.Domain.Movies.Room;
using Howestprime.Movies.Domain.Movies.Room.ValueObjects;
using RoomAggregate = Howestprime.Movies.Domain.Movies.Room.Room;

namespace UnitTests.Domain.Movies.Room;

public sealed class RoomTests
{
    [Fact]
    public void Create_WithValidInput_ShouldInitializeState()
    {
        // Arrange
        RoomId id = new(Guid.NewGuid());

        // Act
        RoomAggregate room = RoomAggregate.Create(id, "Room A", 120);

        // Assert
        Assert.Equal(id, room.Id);
        Assert.Equal("Room A", room.Name);
        Assert.Equal(120, room.Capacity);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        // Arrange
        RoomId id = new(Guid.NewGuid());

        // Act
        Action act = () => RoomAggregate.Create(id, " ", 120);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Create_WithNonPositiveCapacity_ShouldThrow()
    {
        // Arrange
        RoomId id = new(Guid.NewGuid());

        // Act
        Action act = () => RoomAggregate.Create(id, "Room A", 0);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}
