using Howestprime.Movies.Domain.Shared;
namespace Howestprime.Movies.Domain.Movies.Room.ValueObjects;

public readonly struct RoomId: IEntityId
{
    public Guid Value {get;}

    public RoomId(
        Guid value
    )
    {
        Value = value;
    }
}

