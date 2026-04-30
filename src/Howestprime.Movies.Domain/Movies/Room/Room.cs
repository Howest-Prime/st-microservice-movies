using Howestprime.Movies.Domain.Movies.Room.ValueObjects;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Room;

public sealed class Room: AggregateRoot<RoomID>
{
    public string Name {get; private set;}
    public int Capacity {get; private set;}
    
    private Room(
        RoomID id,
        string name,
        int capacity
    ): base(id)
    {
        Name = name;
        Capacity = capacity;
    }

    public static Room Create(
        RoomID id,
        string name,
        int capacity
    )
    {
        Room newRoom = new Room(id, name , capacity);
        newRoom.ValidateState();
        return newRoom;
    }

    public override void ValidateState()
    {
        
    }
}

