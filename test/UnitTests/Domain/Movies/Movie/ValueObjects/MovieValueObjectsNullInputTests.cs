using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class MovieValueObjectsNullInputTests
{
    [Fact]
    public void Actor_Create_WithNullInput_ShouldThrowInvalidActorException()
    {
        // Arrange
        string? value = null;

        // Act
        Action act = () => Actor.Create(value!);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void MovieTitle_Create_WithNullInput_ShouldThrowInvalidMovieTitleException()
    {
        // Arrange
        string? value = null;

        // Act
        Action act = () => MovieTitle.Create(value!);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void MovieDescription_Create_WithNullInput_ShouldThrowInvalidMovieDescriptionException()
    {
        // Arrange
        string? value = null;

        // Act
        Action act = () => MovieDescription.Create(value!);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void MovieGenre_Create_WithNullInput_ShouldThrowInvalidMovieGenreException()
    {
        // Arrange
        string? value = null;

        // Act
        Action act = () => MovieGenre.Create(value!);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void PosterUrl_Create_WithNullInput_ShouldThrowInvalidMoviePosterUrlException()
    {
        // Arrange
        string? value = null;

        // Act
        Action act = () => PosterUrl.Create(value!);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}
