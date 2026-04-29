using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public sealed class InvalidMovieGenreException : DomainException
{
    public InvalidMovieGenreException(
        string genre,
        int maxLength
    ) : base($"The movie genre '{genre}' is invalid. The genre must be between 1 and {maxLength} characters long.")
    {
    }
}