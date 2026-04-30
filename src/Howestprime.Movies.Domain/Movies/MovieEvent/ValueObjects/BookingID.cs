using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent.ValueObject;

public readonly struct BookingID : IEntityId
{
    public Guid Value {get;}

    public BookingID(
        Guid value
    )
    {
        Value = value;
    }
}