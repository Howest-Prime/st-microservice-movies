using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public class InvalidMovieDescriptionException : DomainException
{
    public InvalidMovieDescriptionException(string description, int min, int max) : base($"The movie description '{description}' is invalid. The description must be between {min} and {max} characters.")
    {
    }
}