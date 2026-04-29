using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.Exceptions;

public sealed class InvalidAgeRatingException: DomainException
{
    public InvalidAgeRatingException(
        string rating,
        IEnumerable<string> validRatings
    ) : base($"The age rating '{rating}' is not valid. Valid ratings are: {string.Join(", ", validRatings)}.")
    {
    }
} 