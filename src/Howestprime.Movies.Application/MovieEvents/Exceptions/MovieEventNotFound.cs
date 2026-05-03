using Howestprime.Movies.Application.Shared;

namespace Howestprime.Movies.Application.MovieEvents.Exceptions;

public sealed class MovieEventNotFoundException : MovieApplicationException
{
    public MovieEventNotFoundException(Guid id) : base(
        $"MovieEvent with id {id} doesn't exists."
    )
    {
    }
}