using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public record MovieDescription : ValueObject
{
    private static readonly int MIN_LENGTH = 1;
    private static readonly int MAX_LENGTH = 300;
    
    public string Value { get; private init; }

    private MovieDescription(string value)
    {
        Value = value;
    }

    public static MovieDescription Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
        {
            throw new InvalidMovieDescriptionException(value, MIN_LENGTH, MAX_LENGTH);
        }
        
        return new MovieDescription(value);
    }
}