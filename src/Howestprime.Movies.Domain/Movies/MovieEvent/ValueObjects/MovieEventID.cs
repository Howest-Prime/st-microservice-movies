using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public readonly struct MovieEventId : IEntityId
{
    public Guid Value {get;}

    public MovieEventId(
        Guid value
    )
    {
        Value = value;
    }
}