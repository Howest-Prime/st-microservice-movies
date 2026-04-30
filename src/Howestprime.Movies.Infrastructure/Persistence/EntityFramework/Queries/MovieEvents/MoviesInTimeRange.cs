

using System.Linq.Expressions;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries.MovieEvents;

public sealed class MoviesInTimeRange(QueryDbContext context) : ISearchMovieEventsInTimeRangeQuery
{
 
    public Task<IReadOnlyList<MovieEventData>> Fetch(
        Expression<Func<MovieEventData, bool>>? filter = null
    )
    {
        IQueryable<MovieEventData> query = context.MovieEvents
                                        .Include(me => me.Movie)
                                        .Include(me => me.Room);

        IQueryable<MovieEventData> filteredQuery = query
            .Where(filter ?? (me => true));

        
        IReadOnlyList<MovieEventData> results = filteredQuery.ToList();

        return Task.FromResult(results);   
    }
}