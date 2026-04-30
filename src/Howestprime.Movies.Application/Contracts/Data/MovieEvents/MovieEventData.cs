
namespace Howestprime.Movies.Application.Contracts.Data;

public sealed record MovieEventData
{
    public required Guid Id { get; init; }
    public required DateTime ShowTime { get; init; }
    public required int Capacity { get; init; }
    public required MovieData Movie { get; init; }
    public required RoomData Room { get; init; }

}