using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public sealed record PosterUrl : ValueObject
{
    public string Value { get; private init; }

    private PosterUrl(
        string value
    )
    {
        Value = value;
    }

    public static PosterUrl Create(
        string posterUrl
    )
    {
        Asserts.EnsureNotEmpty(posterUrl, nameof(posterUrl));
        string trimmedUrl = posterUrl.Trim();
        return new PosterUrl(trimmedUrl);
    }
}