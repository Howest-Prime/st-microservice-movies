using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent.Exceptions;

public sealed class InvalidCapacityException : DomainException
{
    public InvalidCapacityException() : base("Capacity cannot be negative.")
    {
    }
}