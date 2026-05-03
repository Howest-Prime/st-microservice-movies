using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.Exceptions;

public sealed class InvalidBookingException : DomainException
{
    public InvalidBookingException(Guid bookingId) : base($"Booking with ID {bookingId} is invalid.")
    {
    }
}