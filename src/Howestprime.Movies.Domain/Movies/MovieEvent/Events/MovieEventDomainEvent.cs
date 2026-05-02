using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.Events;
public class MovieEventDomainEvent : BaseDomainEvent
{
    public MovieEventDomainEvent(string eventName) : base(eventName, nameof(MovieEvent))
    {
    }

}