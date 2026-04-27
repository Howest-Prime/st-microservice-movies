using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public record MovieTitle : ValueObject
{

    private static readonly int MIN_LENGTH = 1;
    private static readonly int MAX_LENGTH = 50;

    public string Value { get; private init; }

    private MovieTitle(string value)
    {
        Value = value;
    }

    public static MovieTitle Create(string value)
    {
        string trimmedValue = value?.Trim() ?? string.Empty;

        if(string.IsNullOrWhiteSpace(trimmedValue) || trimmedValue.Length > MAX_LENGTH || trimmedValue.Length < MIN_LENGTH)
        {
            throw new InvalidMovieTitleException(trimmedValue, MIN_LENGTH, MAX_LENGTH);
        }
        return new MovieTitle(trimmedValue);
    }
} 