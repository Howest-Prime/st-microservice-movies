using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class MovieReleaseYearTests
{
    [Fact]
    public void Create_WithYearBeforeMinimum_ShouldThrowInvalidMovieReleaseYearException()
    {
        // Arrange
        int year = 1887;
        Action act = () => MovieReleaseYear.Create(year);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithYearAfterCurrentYear_ShouldThrowInvalidMovieReleaseYearException()
    {
        // Arrange
        int currentYear = DateTime.Now.Year;
        int invalidYear = currentYear + 1;
        Action act = () => MovieReleaseYear.Create(invalidYear);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithMinimumBoundaryYear_ShouldSetValue()
    {
        // Arrange
        int year = 1888;

        // Act
        MovieReleaseYear movieReleaseYear = MovieReleaseYear.Create(year);

        // Assert
        Assert.Equal(year, movieReleaseYear.Value);
    }

    [Fact]
    public void Create_WithCurrentYearBoundary_ShouldSetValue()
    {
        // Arrange
        int currentYear = DateTime.Now.Year;

        // Act
        MovieReleaseYear movieReleaseYear = MovieReleaseYear.Create(currentYear);

        // Assert
        Assert.Equal(currentYear, movieReleaseYear.Value);
    }
}
