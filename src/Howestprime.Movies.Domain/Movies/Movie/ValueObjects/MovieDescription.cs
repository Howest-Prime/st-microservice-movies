using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public sealed record MovieDescription : ValueObject
{
    private static readonly int MAX_LENGTH = 300;
    
    public string Value { get; private init; }

    private MovieDescription(
        string value
    )
    {
        Value = value;
    }

    public static MovieDescription Create(
        string movieDescription
    )
    {
        Asserts.EnsureNotEmpty(movieDescription, nameof(movieDescription));
        if (movieDescription.Length > MAX_LENGTH)
        {
            throw new ArgumentException($"Value cannot exceed {MAX_LENGTH} characters.");
        }
        return new MovieDescription(movieDescription);
    }
}