using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent.Exceptions;

public sealed class MovieEventTooFarInFutureException : DomainException
{
    public MovieEventTooFarInFutureException() : base("Cannot book this movie event. Reservations are only allowed within 14 days of the event date.")
    {
    }
}