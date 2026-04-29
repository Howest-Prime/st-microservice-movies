using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public record MovieGenre : ValueObject
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
        string value
    )
    {
        string trimmed = value?.Trim() ?? string.Empty;

        if(string.IsNullOrWhiteSpace(trimmed) || trimmed.Length > MAX_LENGTH)
        {
            throw new InvalidMovieGenreException(trimmed, MAX_LENGTH);
        }

        return new MovieGenre(trimmed);
    }
}