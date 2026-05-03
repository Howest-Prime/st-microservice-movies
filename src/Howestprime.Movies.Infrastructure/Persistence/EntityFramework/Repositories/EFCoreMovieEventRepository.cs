
using System.Data.Common;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public class EFCoreMovieEventRepository : EfCoreGenericRepository<MovieEvent, MovieEventID>, IMovieEventRepository
{
    public EFCoreMovieEventRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


   public async Task<MovieEvent?> ByRoomIdAndShowTime(Guid roomId, MovieEventShowTime showTime)
    {
        return await _context.MovieEvents
            .FirstOrDefaultAsync(me => me.RoomId == roomId && 
                        me.ShowTime == showTime);
    }   

   public async Task<MovieEvent?> ByBookingId(BookingID bookingId)
    {
        return await _context.MovieEvents
            .FirstOrDefaultAsync(me => me.Bookings.Any(b => b.Id.Equals(bookingId)));
    }
}