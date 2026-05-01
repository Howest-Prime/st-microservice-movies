using Howestprime.Movies.Domain.MovieEvent.Exceptions;
using Howestprime.Movies.Domain.Shared;
namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public sealed record Capacity(int Value) : ValueObject
{
    public static Capacity Create(
        int value
    )
    {
        Capacity newCapacity = new Capacity(value);
        newCapacity.Validate();
        return newCapacity;
    }

    private void Validate()
    {
        if (Value <= 0)
        {
            throw new InvalidCapacityException();
        }
    }


}