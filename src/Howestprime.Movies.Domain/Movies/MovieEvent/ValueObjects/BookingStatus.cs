using Howestprime.Movies.Domain.Shared;


namespace Howestprime.Movies.Domain.MovieEvent.ValueObject;
public enum BookingStatusState
{
    Pending,
    Confirmed,
    Cancelled
}

public sealed class BookingStatus 
{
    public BookingStatusState Value { get; private init; }

    private BookingStatus(
        BookingStatusState state
    )
    {
        Value = state;
    } 
    
    public static BookingStatus Pending   => new BookingStatus(BookingStatusState.Pending);
    public static BookingStatus Confirmed => new BookingStatus(BookingStatusState.Confirmed);
    public static BookingStatus Cancelled => new BookingStatus(BookingStatusState.Cancelled);
}