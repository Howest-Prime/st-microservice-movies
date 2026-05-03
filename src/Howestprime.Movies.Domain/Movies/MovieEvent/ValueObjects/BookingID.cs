using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public readonly struct BookingId : IEntityId
{
    public Guid Value {get;}

    public BookingId(
        Guid value
    )
    {
        Value = value;
    }
}