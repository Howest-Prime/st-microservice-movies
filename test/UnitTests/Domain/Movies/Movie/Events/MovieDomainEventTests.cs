using Howestprime.Movies.Domain.Movies.Movie.Events;

namespace UnitTests.Domain.Movies.Movie.Events;

public sealed class MovieDomainEventTests
{
    [Fact]
    public void Constructor_WithEventName_ShouldSetMovieAggregateFqdn()
    {
        // Arrange
        const string eventName = "SampleEvent";

        // Act
        MovieDomainEvent domainEvent = new(eventName);

        // Assert
        Assert.Equal("Howestprime.Movies.Movie.SampleEvent", domainEvent.FQDN);
        Assert.True(domainEvent.OccurredOn <= DateTime.UtcNow);
    }
}
