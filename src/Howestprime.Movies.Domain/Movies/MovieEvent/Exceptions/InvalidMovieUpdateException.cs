using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.Exceptions;

public class InvalidMovieUpdateException : DomainException
{
    public InvalidMovieUpdateException() : base("Invalid movie update.")
    {
    }
}