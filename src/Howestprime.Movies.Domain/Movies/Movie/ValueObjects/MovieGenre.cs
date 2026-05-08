using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public sealed record MovieGenre : ValueObject
{
    private static readonly int MAX_LENGTH = 50;

    public string Value { get; private init; }

    private MovieGenre(
        string value
    )
    {
        Value = value;
    }

    public static MovieGenre Create(
        string genre
    )
    {
        Asserts.EnsureNotEmpty(genre);
        string trimmedGenre = genre.Trim();
        if (trimmedGenre.Length > MAX_LENGTH)
        {
            throw new ArgumentException($"Value cannot exceed {MAX_LENGTH} characters.");
        }
        return new MovieGenre(trimmedGenre);
    }
}