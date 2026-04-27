using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public record MovieReleaseYear : ValueObject
{
    private static readonly int MIN_YEAR = 1888; // The year of the earliest known film
    public int Value { get; private init; }


    private MovieReleaseYear(int value)
    {
        Value = value;
    }

    public static MovieReleaseYear Create(int value)
    {
        int currentYear = DateTime.Now.Year;
        if (value < MIN_YEAR || value > currentYear)
        {
            throw new InvalidMovieReleaseYearException(value, MIN_YEAR, currentYear);
        }
        return new MovieReleaseYear(value);
    }
}