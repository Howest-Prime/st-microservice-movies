using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;
public class InvalidMoviePosterUrlException : DomainException
{
  public InvalidMoviePosterUrlException(string posterUrl)
    : base($"The poster URL '{posterUrl}' is invalid. It must be a valid, non-empty URL.")
{
}
}