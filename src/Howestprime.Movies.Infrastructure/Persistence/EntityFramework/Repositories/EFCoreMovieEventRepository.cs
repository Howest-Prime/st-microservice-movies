
using System.Data.Common;
using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Domain.MovieEvent.ValueObject;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public class EFCoreMovieEventRepository : EfCoreGenericRepository<MovieEvent, MovieEventID>, IMovieEventRepository
{
    public EFCoreMovieEventRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


   public async Task<bool> ByIdAndShowTime(MovieId movieId, MovieEventShowTime showTime)
    {
        return await _context.MovieEvents
            .AnyAsync(me => me.MovieId == movieId.Value && 
                        me.ShowTime == showTime);
    }   
}