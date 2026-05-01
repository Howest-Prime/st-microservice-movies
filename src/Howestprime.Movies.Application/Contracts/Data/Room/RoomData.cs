
namespace Howestprime.Movies.Application.Contracts.Data.Room;

public sealed record RoomData
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required int Capacity { get; init; }
}