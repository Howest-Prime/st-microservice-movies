namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record MovieEventResponse(
    Guid Id,
    DateTime ShowTime,
    int Capacity,
    Guid MovieId,
    string MovieTitle,
    string MovieDescription,
    int MovieDuration,
    int MovieReleaseYear,
    int MovieAgeRating,
    string MoviePosterUrl,
    Guid RoomId,
    string RoomName,
    int RoomCapacity
);