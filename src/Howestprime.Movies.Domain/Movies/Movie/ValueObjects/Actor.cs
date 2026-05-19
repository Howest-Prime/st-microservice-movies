using Howestprime.Movies.Domain.Shared;

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
        string actorName
    )
    {
        string trimmedName = actorName?.Trim() ?? string.Empty;
        Asserts.EnsureNotEmpty(trimmedName , nameof(actorName));
        return new Actor(trimmedName);
    }
}