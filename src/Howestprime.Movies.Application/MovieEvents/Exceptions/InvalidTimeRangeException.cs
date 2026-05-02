using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.Exceptions;

public class InvalidTimeRangeException : DomainException
{
    public InvalidTimeRangeException(DateTime from, DateTime to)
        : base($"Invalid time range: 'From' ({from}) must be less than or equal to 'To' ({to}).")
    {
    }
}
