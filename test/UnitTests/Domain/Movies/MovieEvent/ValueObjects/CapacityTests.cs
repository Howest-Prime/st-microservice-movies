using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

namespace UnitTests.Domain.Movies.MovieEvent.ValueObjects;

public sealed class CapacityTests
{
    [Fact]
    public void Create_WithPositiveValue_ShouldInitializeValue()
    {
        // Act
        Capacity capacity = Capacity.Create(120);

        // Assert
        Assert.Equal(120, capacity.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WithNonPositiveValue_ShouldThrow(int value)
    {
        // Act
        Action act = () => Capacity.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}
