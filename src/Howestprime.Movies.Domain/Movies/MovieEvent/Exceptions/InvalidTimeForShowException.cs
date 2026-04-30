
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent.Exceptions;

public sealed class InvalidShowHourException : DomainException
{
    public InvalidShowHourException(IEnumerable<int> allowedHours)
        : base($"Showtime must be at one of the allowed hours: {string.Join(", ", allowedHours)}")
    {
       
    }
}

public sealed class InvalidShowTimeException : DomainException
{
    public InvalidShowTimeException()
        : base("Showtime must be in the future.")
    {
    }
}