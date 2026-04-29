using System.Reflection;
using Howestprime.Movies.Domain.Movies.Movie;

namespace UnitTests.Domain.Movies.Movie;

public sealed class MovieConstructorCoverageTests
{
    [Fact]
    public void PrivateParameterlessConstructor_ShouldBeInvokableByReflection()
    {
        // Arrange
        Type movieType = typeof(Howestprime.Movies.Domain.Movies.Movie.Movie);

        // Act
        object? instance = Activator.CreateInstance(movieType, nonPublic: true);

        // Assert
        Assert.NotNull(instance);
        Assert.IsType<Howestprime.Movies.Domain.Movies.Movie.Movie>(instance);
    }

    [Fact]
    public void PrivateParameterlessConstructor_ShouldInitializeCollections()
    {
        // Arrange
        ConstructorInfo? constructor = typeof(Howestprime.Movies.Domain.Movies.Movie.Movie)
            .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);

        // Act
        var movie = (Howestprime.Movies.Domain.Movies.Movie.Movie?)constructor?.Invoke(null);

        // Assert
        Assert.NotNull(movie);
        Assert.Empty(movie!.Actors);
        Assert.Empty(movie.Genres);
    }
}
