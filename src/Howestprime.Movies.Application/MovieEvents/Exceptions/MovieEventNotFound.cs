using Howestprime.Movies.Application.Shared;

namespace Howestprime.Movies.Application.MovieEvents.Exceptions;

public sealed class MovieEventNotfoundException : MovieApplicationException
{
    public MovieEventNotfoundException(Guid id) : base(
        $"MovieEvent with id {id} doesn't exists."
    )
    {
    }
}