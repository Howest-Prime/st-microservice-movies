using Howestprime.Movies.Domain.Shared;
namespace Howestprime.Movies.Domain.Movies.Room.ValueObjects;

public readonly struct RoomID: IEntityId
{
    public Guid Value {get;}

    public RoomID(
        Guid value
    )
    {
        Value = value;
    }
}

