using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.Exceptions;

public sealed class BookingAlreadyClosedException : DomainException
{
    public BookingAlreadyClosedException(Guid bookingId) : base($"Booking with ID {bookingId} is already closed.")
    {
    }
}