using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class MovieGenreTests
{
    [Fact]
    public void Create_WithNullGenre_ShouldThrowInvalidMovieGenreException()
    {
        // Arrange
        string genre = null!;
        Action act = () => MovieGenre.Create(genre);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithGenreLongerThanMaxLength_ShouldThrowInvalidMovieGenreException()
    {
        // Arrange
        string genre = new('E', 51);
        Action act = () => MovieGenre.Create(genre);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithValidGenre_ShouldSetTrimmedValue()
    {
        // Arrange
        string genre = "  Sci-Fi  ";
        string expectedValue = "Sci-Fi";

        // Act
        MovieGenre movieGenre = MovieGenre.Create(genre);

        // Assert
        Assert.Equal(expectedValue, movieGenre.Value);
    }

    [Fact]
    public void Create_WithBoundaryLength_ShouldSetValue()
    {
        // Arrange
        string genre = new('F', 50);

        // Act
        MovieGenre movieGenre = MovieGenre.Create(genre);

        // Assert
        Assert.Equal(genre, movieGenre.Value);
    }
}
