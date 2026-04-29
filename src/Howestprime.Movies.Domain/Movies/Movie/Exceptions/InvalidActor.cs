using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public class InvalidActorException : DomainException
{
    public InvalidActorException(
        string actorName
    ) : base($"The actor name '{actorName}' is invalid. The actor name must be non-empty and cannot contain only whitespace.")
    {
    }
}