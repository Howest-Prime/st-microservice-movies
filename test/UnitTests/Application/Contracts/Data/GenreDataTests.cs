using Howestprime.Movies.Application.Contracts.Data.Movies;

namespace UnitTests.Application.Contracts.Data;

public sealed class GenreDataTests
{
    [Fact]
    public void Create_WithDefaults_ShouldInitializeStrings()
    {
        // Arrange
        GenreData data = new();

        // Act
        string value = data.Value;

        // Assert
        Assert.Equal(string.Empty, value);
    }

    [Fact]
    public void Create_WithValues_ShouldSetAllProperties()
    {
        // Arrange
        GenreData data = new()
        {
            Value = "Sci-Fi"
        };

        // Act
        string value = data.Value;

        // Assert
        Assert.Equal("Sci-Fi", value);
    }
}
