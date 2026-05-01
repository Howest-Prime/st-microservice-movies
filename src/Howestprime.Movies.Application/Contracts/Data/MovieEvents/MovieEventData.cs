using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Data.Room;

namespace Howestprime.Movies.Application.Contracts.Data.MovieEvents;

public sealed record MovieEventData
{
    public required Guid Id { get; init; }
    public required DateTime ShowTime { get; init; }
    public required int Capacity { get; init; }
    public required MovieData Movie { get; init; }
    public required RoomData Room { get; init; }

}