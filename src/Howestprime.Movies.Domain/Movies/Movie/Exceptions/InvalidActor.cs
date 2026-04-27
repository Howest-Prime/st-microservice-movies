using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public class InvalidActorException : DomainException
{
    public InvalidActorException(string actor) : base($"The actor '{actor}' is invalid. The actor must be non-empty and cannot contain only whitespace.")
    {
    }
}