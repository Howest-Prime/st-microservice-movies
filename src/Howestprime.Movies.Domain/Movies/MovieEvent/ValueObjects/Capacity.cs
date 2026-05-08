using Howestprime.Movies.Domain.Shared;
namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public sealed record Capacity(int Value) : ValueObject
{
    public static Capacity Create(
        int capacity
    )
    {
        Asserts.EnsureGreaterThan(capacity, 0);
        return new Capacity(capacity);
    }
}