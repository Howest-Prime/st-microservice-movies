using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public record Actor : ValueObject
{
    public string Value { get; private init; }

    private Actor(string value)
    {
        Value = value;
    }

    public static Actor Create(string value)
    {
        string trimmed = value?.Trim() ?? string.Empty;

        if(string.IsNullOrWhiteSpace(trimmed) || trimmed.Length == 0)
        {
            throw new InvalidActorException(trimmed);
        }

        return new Actor(trimmed);
    }
}