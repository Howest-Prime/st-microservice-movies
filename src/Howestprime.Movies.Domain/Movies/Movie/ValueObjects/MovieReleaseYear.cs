using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public sealed record MovieReleaseYear : ValueObject
{
    private static readonly int MIN_YEAR = 1888; // The year of the earliest known film
    public int Value { get; private init; }


    private MovieReleaseYear(
        int value
    )
    {
        Value = value;
    }

    public static MovieReleaseYear Create(
        int movieReleaseYear
    )
    {
        EnsureValidMovieReleaseYear(movieReleaseYear);
        return new MovieReleaseYear(movieReleaseYear);
    }

    private static void EnsureValidMovieReleaseYear(int year)
    {
        int currentYear = DateTime.Now.Year;
         if (year < MIN_YEAR || year > currentYear)
        {
            throw new ArgumentException($"Invalid movie release year. The release year should be between {MIN_YEAR} and {currentYear}");
        }
    }
}