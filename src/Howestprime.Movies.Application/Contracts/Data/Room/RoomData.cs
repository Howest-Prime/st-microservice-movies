
namespace Howestprime.Movies.Application.Contracts.Data.Room;

public sealed record RoomData
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public int Capacity { get; init; }
}