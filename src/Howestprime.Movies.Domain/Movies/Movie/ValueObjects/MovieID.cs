using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public readonly struct MovieId : IEntityId
{
    public Guid Value { get; }

    public MovieId(Guid value)
    {
        Value = value;
    }
}