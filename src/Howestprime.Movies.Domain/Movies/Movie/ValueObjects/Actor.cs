using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public record Actor : ValueObject
{
    public string Value { get; private init; }

    private Actor(
        string value
    )
    {
        Value = value;
    }

    public static Actor Create(
        string name
    )
    {
        string trimmedName = name?.Trim() ?? string.Empty;

        if(string.IsNullOrWhiteSpace(trimmedName))
        {
            throw new InvalidActorException(trimmedName);
        }

        return new Actor(trimmedName);
    }
}