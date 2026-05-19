using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movies.MovieEvent;

public sealed class Booking : Entity<BookingId>
{
    public BookingStatus BookingStatus { get; private set; } = BookingStatus.Open;
    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;
    public int StandardVisitors {get; private set;}
    public int DiscountVisitors {get; private set;}
    public IReadOnlyList<int> SeatNumbers {get; private set;}

    public int TotalVisitors => StandardVisitors + DiscountVisitors;


    private Booking(){ }

    private Booking(
        BookingId id,
        int standardVisitors,
        int discountVisitors,
        IReadOnlyList<int> bookingSeats
    ) : base(id)
    {
        StandardVisitors = standardVisitors;
        DiscountVisitors = discountVisitors;
        SeatNumbers = bookingSeats;
    }

    public static Booking Create(
        BookingId id,
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

    public Booking Close(PaymentStatus paymentStatus)
    {
        if(BookingStatus == BookingStatus.Closed)
        {
            throw new InvalidOperationException($"Booking with bookingId {Id} already closed.");
        }
        
        BookingStatus = BookingStatus.Closed;
        PaymentStatus = paymentStatus;
        SeatNumbers = paymentStatus == PaymentStatus.Failed 
                                ? new List<int>() 
                                : SeatNumbers;

        return this;
    }

    
    public override void ValidateState()
    {
       Asserts.EnsureGreaterThan(TotalVisitors, 0);
    }

    private static IReadOnlyList<int> GenerateSeatNumbers(
        int totalVisitors,
        int startSeatNumber
    )
    {
        IList<int> seats = new List<int>();

        for (int i = 0; i < totalVisitors; i++)
        {
            int seatNr = startSeatNumber + i;
            seats.Add(seatNr);
        }

        return seats.AsReadOnly();
    }
}