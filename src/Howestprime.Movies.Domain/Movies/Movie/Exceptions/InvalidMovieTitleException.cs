using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public sealed class InvalidMovieTitleException : DomainException
{
    public InvalidMovieTitleException(
        string title,
        int max
    ) : base($"The movie title '{title}' is invalid. The title must be between 1 and {max} characters.")
    {
    }
}