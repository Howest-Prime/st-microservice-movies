using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;

namespace Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

public sealed record Actor : ValueObject
{
    public string Name { get; private init; }

    private Actor(
        string name
    )
    {
        Name = name;
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