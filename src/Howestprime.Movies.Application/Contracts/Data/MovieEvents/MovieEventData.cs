using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Data.Room;

namespace Howestprime.Movies.Application.Contracts.Data.MovieEvents;

public sealed record MovieEventData
{
    public Guid Id { get; init; } = Guid.Empty;
    public DateTimeOffset ShowTime { get; init; }
    public int Capacity { get; init; }
    public MovieData Movie { get; init; } = new MovieData();
    public RoomData Room { get; init; } = new RoomData();

}