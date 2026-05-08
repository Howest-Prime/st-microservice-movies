using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

public sealed record MovieEventShowTime : ValueObject
{
    public DateTime Value { get; private set; }

    private MovieEventShowTime(DateTime value)
    {
        Value = value;
    }

    public static MovieEventShowTime Create(DateTime value)
    {
        ValidateMovieEventShowTime(value);
        return new MovieEventShowTime(value.ToUniversalTime());
    }

    private static void ValidateMovieEventShowTime(DateTime dateTime)
    {
        EnsureShowtimeIsAt15hOr19h(dateTime);
        EnsureShowtimeIsInTheFuture(dateTime);
    }

    public static void EnsureShowtimeIsAt15hOr19h(DateTime time)
    {
       if (time.Hour != 15 && time.Hour != 19)
           throw new ArgumentException("Time must be at 15h or 19h.");
    }

    public static void EnsureShowtimeIsInTheFuture(DateTime time)
    {
       if (time <= DateTime.Now)
           throw new ArgumentException($"Movie event {time} must be in the future.");
    }



}