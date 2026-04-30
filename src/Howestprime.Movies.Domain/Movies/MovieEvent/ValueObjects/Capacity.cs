using Howestprime.Movies.Domain.Shared;
namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public sealed record Capacity(int Value) : ValueObject
{
    public static Capacity Create(
        int value
    )
    {
        return new Capacity(value);
    }


}