using Howestprime.Movies.Application.Contracts.Data.Room;

namespace UnitTests.Application.Contracts.Data;

public sealed class RoomDataTests
{
    [Fact]
    public void Create_WithDefaults_ShouldInitializeStrings()
    {
        // Arrange
        RoomData data = new();

        // Act
        string name = data.Name;

        // Assert
        Assert.Equal(Guid.Empty, data.Id);
        Assert.Equal(string.Empty, name);
        Assert.Equal(0, data.Capacity);
    }

    [Fact]
    public void Create_WithValues_ShouldSetAllProperties()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        RoomData data = new()
        {
            Id = id,
            Name = "Room A",
            Capacity = 100
        };

        // Act
        string name = data.Name;

        // Assert
        Assert.Equal(id, data.Id);
        Assert.Equal("Room A", name);
        Assert.Equal(100, data.Capacity);
    }
}
