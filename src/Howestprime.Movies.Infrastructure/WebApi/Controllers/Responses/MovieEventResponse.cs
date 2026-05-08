namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record MovieEventResponse(
    Guid Id,
    DateTimeOffset ShowTime,
    int Capacity,
    MovieResponse Movie,
    RoomResponse Room
);

public sealed record RoomResponse(
    Guid Id,
    string Name,
    int Capacity
);