using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public class InvalidMovieReleaseYearException : DomainException
{
    public InvalidMovieReleaseYearException(int? year, int minYear, int maxYear) : base($"The movie release year '{year}' is invalid. The year must be between {minYear} and {maxYear}.")
    {
    }
}