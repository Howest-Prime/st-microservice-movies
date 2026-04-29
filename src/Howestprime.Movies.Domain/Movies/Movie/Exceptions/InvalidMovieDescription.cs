using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public class InvalidMovieDescriptionException : DomainException
{
    public InvalidMovieDescriptionException(
        string description,
        int max
    ) : base($"The movie description '{description}' is invalid. The description must be between 1 and {max} characters.")
    {
    }
}