using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

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