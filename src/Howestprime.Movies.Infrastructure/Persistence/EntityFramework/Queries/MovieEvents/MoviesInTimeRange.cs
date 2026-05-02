using Aornis;
using System.Linq.Expressions;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries.MovieEvents;

public sealed class MoviesInTimeRange(QueryDbContext context) : IMovieEventQuery
{
    public async Task<Optional<IReadOnlyList<MovieEventData>>> Fetch(
        Expression<Func<MovieEventData, bool>>? filter = null)
    {
        IQueryable<MovieEventData> query = context.MovieEvents
            .Include(me => me.Movie)
            .Include(me => me.Room)
            .AsNoTracking();

        IReadOnlyList<MovieEventData> results = await query
            .Where(filter ?? (me => true))
            .ToListAsync();

        return results.Count > 0
            ? Optional.Of(results)
            : Optional.Empty;
    }
}