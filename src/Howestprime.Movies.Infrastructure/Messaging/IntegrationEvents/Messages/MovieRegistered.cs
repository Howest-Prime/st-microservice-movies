namespace Howestprime.Movies.Infrastructure.Messaging.IntegrationEvents.Messages;

public sealed record MovieRegistered(
    Guid MovieId,
    string Title,
    IReadOnlyList<string> Genres,
    IReadOnlyList<string> Actors,
    int ReleaseYear,
    int Duration,
    string AgeRating,
    string PosterUrl
);