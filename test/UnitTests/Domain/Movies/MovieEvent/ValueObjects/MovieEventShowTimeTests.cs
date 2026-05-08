using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

namespace UnitTests.Domain.Movies.MovieEvent.ValueObjects;

public sealed class MovieEventShowTimeTests
{
    [Theory]
    [InlineData(15)]
    [InlineData(19)]
    public void Create_WithAllowedFutureHour_ShouldNormalizeToUtc(int hour)
    {
        // Arrange
        DateTime showTime = NextShowTimeAt(hour);

        // Act
        MovieEventShowTime result = MovieEventShowTime.Create(showTime);

        // Assert
        Assert.Equal(showTime.ToUniversalTime(), result.Value);
    }

    [Fact]
    public void Create_WithUnsupportedHour_ShouldThrow()
    {
        // Arrange
        DateTime showTime = NextShowTimeAt(17);

        // Act
        Action act = () => MovieEventShowTime.Create(showTime);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Create_WithPastShowTime_ShouldThrow()
    {
        // Arrange
        DateTime showTime = DateTime.Now.Date.AddDays(-1).AddHours(15);

        // Act
        Action act = () => MovieEventShowTime.Create(showTime);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    private static DateTime NextShowTimeAt(int hour)
    {
        DateTime showTime = DateTime.Now.Date.AddDays(1).AddHours(hour);
        return showTime <= DateTime.Now ? showTime.AddDays(1) : showTime;
    }
}
