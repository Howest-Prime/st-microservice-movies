using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public record MovieDuration : ValueObject
{
    public int Value { get; private init; }

    private MovieDuration(int value)
    {
        Value = value;
    }

    public static MovieDuration  Create(int value)
    {
        if (value <= 0)
        {
            throw new InvalidMovieDurationException(value);
        }
        return new MovieDuration(value);
    }
}