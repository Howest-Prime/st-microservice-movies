namespace Howestprime.Movies.Application.MovieEvents.Exceptions;

public sealed class InvalidCloseBookingReasonException : ApplicationException
{
    public InvalidCloseBookingReasonException(string reason) : base($"The provided reason '{reason}' is not valid for closing a booking.")
    {
    }
}