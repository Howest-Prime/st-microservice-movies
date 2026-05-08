using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.MovieEvent.Events;
using Howestprime.Movies.Shared.Exceptions;

namespace Howestprime.Movies.Domain.Movies.MovieEvent;

public sealed class MovieEvent : AggregateRoot<MovieEventId>
{

    public Guid MovieId {get; private set;}
    public Guid RoomId {get; private set;}
    public MovieEventShowTime ShowTime {get; private set;}
    public Capacity Capacity {get; private set;}
    public IList<Booking> Bookings {get; private set;} = new List<Booking>();
    public int Visitors => Bookings.Where(b => b.PaymentStatus != PaymentStatus.Failed)
                                        .Sum(booking => booking.TotalVisitors);


    private MovieEvent() {}

    private MovieEvent(
        MovieEventId id,
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime,
        Capacity capacity 
       
    ) : base(id)
    {
        MovieId = movieId;
        RoomId = roomId;
        ShowTime = showTime;
        Capacity = capacity;
    }

    public static MovieEvent Create(
        MovieEventId id,
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime,
        Capacity capacity
    )
    {
        MovieEvent newMovieEvent = new MovieEvent(id, movieId, roomId, showTime, capacity);
        newMovieEvent.ValidateState();
        return newMovieEvent;
    }

    public BookingId Book(
        int standardVisitors,
        int discountVisitors
    )
    {
        EnsureValidBooking(standardVisitors + discountVisitors);

        BookingId newBookingId = EntityId.New<BookingId>();
        Booking newBooking = Booking.Create(
            newBookingId,
            standardVisitors,
            discountVisitors,
            Visitors + 1     
        );

        Bookings.Add(newBooking);
        
        RaiseDomainEvent(BookingOpened.Create(
            newBookingId,
            MovieId,
            RoomId,
            ShowTime,
            standardVisitors,
            discountVisitors,
            Bookings.ToList()
        ));

        return newBookingId;
    }

     public void UpdateMovie(Guid value)
    {
        MovieId = value;
    }

    public Booking CloseBooking(BookingId bookingId , PaymentStatus paymentStatus)
    {
        Booking bookingToClose = GetBookingById(bookingId);
        bookingToClose.Close(paymentStatus);
        return bookingToClose;
    }


    private Booking GetBookingById(BookingId bookingId)
    {
        Booking? booking = Bookings.FirstOrDefault(b => b.Id.Equals(bookingId))
            ?? throw new NotFoundException($"Booking with id '{bookingId}' was not found.");

        return booking;
    }
    
    public override void ValidateState()
    {
       // throw new NotImplementedException();
    }

    private void EnsureValidBooking(int toAddVisitors)
    {
        EnsureCapacityAvailableForMovieEvent(toAddVisitors);
        EnsureMovieEventIsIn14Days();
    }

    private void EnsureCapacityAvailableForMovieEvent(int toAddVisitors)
    {
        if(Capacity.Value < Visitors + toAddVisitors)
        {
            throw new ArgumentException(
            $"Not enough capacity. Only {Capacity.Value - Visitors} spot(s) remaining.");
        }
    }

    private void EnsureMovieEventIsIn14Days()
    {
        if(ShowTime.Value > DateTimeOffset.UtcNow.AddDays(14))
        {
          throw new ArgumentException(
            "Cannot book a movie event that is more than 14 days in the future.");
        }
    }
}
