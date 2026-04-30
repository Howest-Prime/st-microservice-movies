using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent.ValueObject;

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