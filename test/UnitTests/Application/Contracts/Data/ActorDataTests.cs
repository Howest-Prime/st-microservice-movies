using Howestprime.Movies.Application.Contracts.Data.Movies;

namespace UnitTests.Application.Contracts.Data;

public sealed class ActorDataTests
{
    [Fact]
    public void Create_WithDefaults_ShouldInitializeStrings()
    {
        // Arrange
        ActorData data = new();

        // Act
        string name = data.Name;

        // Assert
        Assert.Equal(string.Empty, name);
    }

    [Fact]
    public void Create_WithValues_ShouldSetAllProperties()
    {
        // Arrange
        ActorData data = new()
        {
            Name = "Leonardo DiCaprio"
        };

        // Act
        string name = data.Name;

        // Assert
        Assert.Equal("Leonardo DiCaprio", name);
    }
}
