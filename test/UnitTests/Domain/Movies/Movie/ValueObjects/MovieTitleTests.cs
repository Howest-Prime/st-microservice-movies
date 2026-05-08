using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class MovieTitleTests
{
    [Fact]
    public void Create_WithNullTitle_ShouldThrowInvalidMovieTitleException()
    {
        // Arrange
        string title = null!;
        Action act = () => MovieTitle.Create(title);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithTitleLongerThanMaxLength_ShouldThrowInvalidMovieTitleException()
    {
        // Arrange
        string title = new('A', 51);
        Action act = () => MovieTitle.Create(title);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithValidTitle_ShouldSetTrimmedValue()
    {
        // Arrange
        string title = "  Inception  ";
        string expectedValue = "Inception";

        // Act
        MovieTitle movieTitle = MovieTitle.Create(title);

        // Assert
        Assert.Equal(expectedValue, movieTitle.Value);
    }

    [Fact]
    public void Create_WithBoundaryLength_ShouldSetValue()
    {
        // Arrange
        string title = new('B', 50);

        // Act
        MovieTitle movieTitle = MovieTitle.Create(title);

        // Assert
        Assert.Equal(title, movieTitle.Value);
    }
}
