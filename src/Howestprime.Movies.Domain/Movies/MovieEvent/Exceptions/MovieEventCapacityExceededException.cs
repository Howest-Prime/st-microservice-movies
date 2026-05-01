using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent.Exceptions;

public sealed class MovieEventCapacityExceededException : DomainException
{
    public MovieEventCapacityExceededException(int remainingCapacity) : base($"Booking failed: only {remainingCapacity} seats are still available for this movie event.")
    {
    }
}