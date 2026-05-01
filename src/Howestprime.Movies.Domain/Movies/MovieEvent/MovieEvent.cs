using Howestprime.Movies.Domain.MovieEvent.Exceptions;
using Howestprime.Movies.Domain.MovieEvent.ValueObject;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent;

public sealed class MovieEvent : AggregateRoot<MovieEventID>
{

    public Guid MovieId {get; private set;}
    public Guid RoomId {get; private set;}
    public MovieEventShowTime ShowTime {get; private set;}
    public Capacity Capacity {get; private set;}
    public IList<Booking> Bookings {get; private set;} = new List<Booking>();
    public int Visitors => Bookings.Sum(booking => booking.TotalVisitors);


    private MovieEvent()
    {
    }

    private MovieEvent(
        MovieEventID id,
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime,
        Capacity? capacity 
       
    ) : base(id)
    {
        MovieId = movieId;
        RoomId = roomId;
        ShowTime = showTime;
        Capacity = capacity ?? Capacity.Create(0);
    }

    public static MovieEvent Create(
        MovieEventID id,
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime,
        Capacity? capacity = null
    )
    {
        MovieEvent newMovieEvent = new MovieEvent(id, movieId, roomId, showTime, capacity);
        newMovieEvent.ValidateState();
        return newMovieEvent;
    }

    public BookingID Book(
        int standardVisitors,
        int discountVisitors
    )
    {
        EnsureValidBooking(standardVisitors + discountVisitors);

        BookingID newBookingId = EntityId.New<BookingID>();
        Booking newBooking = Booking.Create(
            newBookingId,
            standardVisitors,
            discountVisitors,
            Visitors + 1     
        );

        Bookings.Add(newBooking);

        return newBookingId;
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
            throw new MovieEventCapacityExceededException(Capacity.Value - Visitors);
        }
    }

    private void EnsureMovieEventIsIn14Days()
    {
        if(ShowTime.Value > DateTime.UtcNow.AddDays(14))
        {
            throw new MovieEventBookingTooLateException();
        }
    }

   
}