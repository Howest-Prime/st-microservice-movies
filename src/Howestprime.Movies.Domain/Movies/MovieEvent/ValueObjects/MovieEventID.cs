using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public readonly struct MovieEventID : IEntityId
{
    public Guid Value {get;}

    public MovieEventID(
        Guid value
    )
    {
        Value = value;
    }
}