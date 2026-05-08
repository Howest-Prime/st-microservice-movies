using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class MovieDescriptionTests
{
    [Fact]
    public void Create_WithNullDescription_ShouldThrowInvalidMovieDescriptionException()
    {
        // Arrange
        string description = null!;
        Action act = () => MovieDescription.Create(description);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithDescriptionLongerThanMaxLength_ShouldThrowInvalidMovieDescriptionException()
    {
        // Arrange
        string description = new('C', 301);
        Action act = () => MovieDescription.Create(description);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithValidDescription_ShouldSetValue()
    {
        // Arrange
        string description = "A mind-bending thriller";

        // Act
        MovieDescription movieDescription = MovieDescription.Create(description);

        // Assert
        Assert.Equal(description, movieDescription.Value);
    }

    [Fact]
    public void Create_WithBoundaryLength_ShouldSetValue()
    {
        // Arrange
        string description = new('D', 300);

        // Act
        MovieDescription movieDescription = MovieDescription.Create(description);

        // Assert
        Assert.Equal(description, movieDescription.Value);
    }
}
