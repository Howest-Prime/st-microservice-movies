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
    public IList<Booking> Bookings {get; private set;}
    public int Visitors {get; private set;}


    private MovieEvent()
    {
    }
    private MovieEvent(
        MovieEventID id,
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime,
        Capacity? capacity = null,
        IList<Booking>? bookings = null,
        int? visitors = null
    ) : base(id)
    {
        MovieId = movieId;
        RoomId = roomId;
        ShowTime = showTime;
        Capacity = capacity ?? Capacity.Create(0);
        Bookings = bookings ?? new List<Booking>();
        Visitors = visitors ?? 0;
    }

    public static MovieEvent Create(
        MovieEventID id,
        Guid movieId,
        Guid roomId,
        MovieEventShowTime showTime
    )
    {
        MovieEvent newMovieEvent = new MovieEvent(id, movieId, roomId,  showTime);
        newMovieEvent.ValidateState();
        return newMovieEvent;
    }

    
    public override void ValidateState()
    {
       // throw new NotImplementedException();
    }

   
}