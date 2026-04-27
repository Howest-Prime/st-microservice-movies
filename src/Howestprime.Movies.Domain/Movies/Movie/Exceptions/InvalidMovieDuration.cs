using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public class InvalidMovieDurationException : DomainException
{
    public InvalidMovieDurationException(int duration)
        : base($"Movie duration '{duration}' is invalid. It must be greater than 0 minutes.")
    {
    }
}