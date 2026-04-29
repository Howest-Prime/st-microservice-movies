using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public record PosterUrl : ValueObject
{
    public string Value { get; private init; }

    private PosterUrl(
        string value
    )
    {
        Value = value;
    }

    public static PosterUrl Create(
        string value
    )
    {
        string trimmedUrl = value?.Trim() ?? string.Empty;

        if(string.IsNullOrWhiteSpace(trimmedUrl))
        {
            throw new InvalidMoviePosterUrlException(trimmedUrl);
        }
        return new PosterUrl(trimmedUrl);
    }
}