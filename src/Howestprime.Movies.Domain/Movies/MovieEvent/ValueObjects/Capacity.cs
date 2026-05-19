using Howestprime.Movies.Domain.Shared;
namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public sealed record Capacity(int Value) : ValueObject
{
    public static Capacity Create(
        int movieCapacity
    )
    {
        Asserts.EnsureGreaterThan(movieCapacity, 0, nameof(movieCapacity));
        return new Capacity(movieCapacity);
    }
}