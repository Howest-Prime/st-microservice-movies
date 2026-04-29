using Howestprime.Movies.Domain.Movies.Movie.Exceptions;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie.ValueObjects;

public sealed class ActorTests
{
    [Fact]
    public void Create_WithNullName_ShouldThrowInvalidActorException()
    {
        // Arrange
        string actorName = null!;
        Action act = () => Actor.Create(actorName);

        // Act
        InvalidActorException exception = Assert.Throws<InvalidActorException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithWhitespaceName_ShouldThrowInvalidActorException()
    {
        // Arrange
        string actorName = "   ";
        Action act = () => Actor.Create(actorName);

        // Act
        InvalidActorException exception = Assert.Throws<InvalidActorException>(act);

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void Create_WithValidName_ShouldSetTrimmedValue()
    {
        // Arrange
        string actorName = "  Keanu Reeves  ";
        string expectedValue = "Keanu Reeves";

        // Act
        Actor actor = Actor.Create(actorName);

        // Assert
        Assert.Equal(expectedValue, actor.Name);
    }
}
