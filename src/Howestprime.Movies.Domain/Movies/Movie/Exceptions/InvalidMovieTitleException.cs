using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public class InvalidMovieTitleException : DomainException
{
    public InvalidMovieTitleException(string title, int min, int max) : base($"The movie title '{title}' is invalid. The title must be between {min} and {max} characters.")
    {
    }
}