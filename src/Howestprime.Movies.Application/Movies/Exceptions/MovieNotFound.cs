using Howestprime.Movies.Application.Shared;

namespace Howestprime.Movies.Application.Movies.Exceptions;

public sealed class MovieNotFoundException : MovieApplicationException
{
    public MovieNotFoundException(Guid id) : base($"Movie with ID '{id}' was not found.")
    {
    }
}