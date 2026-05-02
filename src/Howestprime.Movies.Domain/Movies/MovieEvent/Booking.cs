using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent;

public sealed class Booking : Entity<BookingID>
{
    public BookingStatus BookingStatus { get; private set; } = BookingStatus.Open;
    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;
    public int StandardVisitors {get; private set;}
    public int DiscountVisitors {get; private set;}
    public IReadOnlyList<string> SeatNumbers {get; private set;}

    public int TotalVisitors => StandardVisitors + DiscountVisitors;


    private Booking()
    {
        
    }

    private Booking(
        BookingID id,
        int standardVisitors,
        int discountVisitors,
        IReadOnlyList<string> bookingSeats
    ) : base(id)
    {
        StandardVisitors = standardVisitors;
        DiscountVisitors = discountVisitors;
        SeatNumbers = bookingSeats;
    }

    public static Booking Create(
        BookingID id,
        int standardVisitors,
        int discountVisitors,
        int startSeatNumber
    )
    {
        Booking newBooking =  new Booking(
            id,
            standardVisitors,
            discountVisitors,
            GenerateSeatNumbers(
                standardVisitors + discountVisitors,
                startSeatNumber
            )
        );

        newBooking.ValidateState();
        return newBooking;
    }

    
    public override void ValidateState()
    {
       Asserts.EnsureGreaterThan(TotalVisitors, 0);
    }

    private static IReadOnlyList<string> GenerateSeatNumbers(
        int totalVisitors,
        int startSeatNumber
    )
    {
        IList<string> seats = new List<string>();

        for (int i = 0; i < totalVisitors; i++)
        {
            seats.Add((startSeatNumber + i).ToString());
        }

        return seats.AsReadOnly();
    }
}