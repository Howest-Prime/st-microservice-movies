
using Howestprime.Movies.Application.Shared;

namespace Howestprime.Movies.Application.MovieEvents.Exceptions;

public class EventAlreadyExists : MovieApplicationException
{
    public EventAlreadyExists(Guid id, DateTime showTime) : base($"A movie event with the id {id} and show time {showTime} already exists.")
    {
    }
}