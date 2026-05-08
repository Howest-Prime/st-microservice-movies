using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public sealed record MovieEventShowTime : ValueObject
{
    public DateTimeOffset Value { get; private set; }

    private MovieEventShowTime(DateTimeOffset value)
    {
        Value = value;
    }

    public static MovieEventShowTime Create(DateTimeOffset value)
    {
        ValidateMovieEventShowTime(value);
        return new MovieEventShowTime(value.ToUniversalTime());
    }

    private static void ValidateMovieEventShowTime(DateTimeOffset dateTime)
    {
        EnsureShowtimeIsAt15hOr19h(dateTime);
        EnsureShowtimeIsInTheFuture(dateTime);
    }

    public static void EnsureShowtimeIsAt15hOr19h(DateTimeOffset time)
    {
       if (time.Hour != 15 && time.Hour != 19)
        {
            throw new ArgumentException("Time must be at 15h or 19h.");
        }
    }

    public static void EnsureShowtimeIsInTheFuture(DateTimeOffset time)
    {
       if (time <= DateTimeOffset.Now)
        {
            throw new ArgumentException($"Movie event {time} must be in the future.");
        }
    }



}