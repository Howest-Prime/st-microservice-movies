using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public sealed record MovieTitle : ValueObject
{
    private static readonly int MAX_LENGTH = 50;

    public string Value { get; private init; }

    private MovieTitle(
        string value
    )
    {
        Value = value;
    }

    public static MovieTitle Create(
        string movieTitle
    )
    {
        Asserts.EnsureNotEmpty(movieTitle);
        string trimmedValue = movieTitle.Trim();
        if (trimmedValue.Length > MAX_LENGTH)
        {
            throw new ArgumentException($"Value cannot exceed {MAX_LENGTH} characters.");
        }
        return new MovieTitle(trimmedValue);
    }
} 