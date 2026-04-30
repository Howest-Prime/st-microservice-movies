using Howestprime.Movies.Domain.MovieEvent.ValueObject;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent;

public sealed class Booking : Entity<BookingID>
{
    public BookingStatusState BookingStatus { get; private set; }
    public PaymentStatusState PaymentStatus { get; private set; }
    public int StandardVisitors {get; private set;}
    public int DiscountVisitors {get; private set;}
    public IList<string> SeatNumbers {get; private set;}


    private Booking(
        BookingID id,
        BookingStatusState bookingStatus,
        PaymentStatusState paymentStatus,
        int standardVisitors,
        int discountVisitors,
        IList<string> seatNumbers
    ) : base(id)
    {
        BookingStatus = bookingStatus;
        PaymentStatus = paymentStatus;
        StandardVisitors = standardVisitors;
        DiscountVisitors = discountVisitors;
        SeatNumbers = seatNumbers;
    }

    public static Booking Create(
        BookingID id,
        BookingStatusState bookingStatus,
        PaymentStatusState paymentStatus,
        int standardVisitors,
        int discountVisitors,
        IList<string> seatNumbers
    )
    {
        Booking newBooking = new Booking(id, bookingStatus, paymentStatus, standardVisitors, discountVisitors, seatNumbers);
        newBooking.ValidateState();
        return newBooking;
    }
    
    public override void ValidateState()
    {
       
    }
}