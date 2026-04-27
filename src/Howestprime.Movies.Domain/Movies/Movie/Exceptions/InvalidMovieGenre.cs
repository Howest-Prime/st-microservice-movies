using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public class InvalidMovieGenreException : DomainException
{
    public InvalidMovieGenreException(string genre, int minLength, int maxLength) : base($"The movie genre '{genre}' is invalid. The genre must be between {minLength} and {maxLength} characters long.")
    {
    }
}