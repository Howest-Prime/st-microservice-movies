using Howestprime.Movies.Domain.MovieEvent.Exceptions;
using Howestprime.Movies.Domain.Shared;
namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public sealed record MovieEventShowTime: ValueObject
{
    private static readonly IReadOnlyList<int> VALID_HOURS = new List<int>() {15, 19};
    public DateTime Value {get; private set;}

    private MovieEventShowTime(
        DateTime value
    )
    {
        Value = value;
    }

    public static MovieEventShowTime Create (
        DateTime value
    )
    {
        MovieEventShowTime newShowTime = new MovieEventShowTime(value);
        newShowTime.Validate();
        return  newShowTime;
    }



    private void Validate()
    {
        ValidateAllowedHour();
        ValidateFutureTime();
    }

    private void ValidateAllowedHour()
    {
        if (!VALID_HOURS.Contains(Value.Hour))
        {
           throw new InvalidShowHourException(VALID_HOURS);
        }
    }

    private void ValidateFutureTime()
    {
        if (Value <= DateTime.Now)
        {
            throw new InvalidShowTimeException();
        }
    }

}