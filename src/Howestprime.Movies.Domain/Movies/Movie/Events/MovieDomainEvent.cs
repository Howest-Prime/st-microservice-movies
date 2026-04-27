using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Events;
public class MovieDomainEvent : BaseDomainEvent
{
    public MovieDomainEvent(string eventName) : base(eventName, nameof(Movie))
    {
    }

}