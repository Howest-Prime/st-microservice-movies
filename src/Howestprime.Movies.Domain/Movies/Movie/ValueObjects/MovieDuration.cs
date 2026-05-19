using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public sealed record MovieDuration : ValueObject
{
    public int Value { get; private init; }

    private MovieDuration(
        int value
    )
    {
        Value = value;
    }

    public static MovieDuration Create(
        int movieDuration
    )
    {
        Asserts.EnsureGreaterThan(movieDuration, 0, nameof(movieDuration));
        return new MovieDuration(movieDuration);
    }
}