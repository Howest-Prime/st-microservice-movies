using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Howestprime.Movies.Application.Contracts.Data;
using Aornis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries;

public sealed class MoviesByTitleAndGenres(QueryDbContext context) : ISearchMovieCatalogQuery
{
    public async Task<Optional<IReadOnlyList<MovieData>>> Fetch(
        Expression<Func<MovieData, bool>>? filter = null
    )
    {
        IQueryable<MovieData> query = context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Actors);

        IQueryable<MovieData> filteredQuery = query
            .Where(filter ?? (m => true));

        IReadOnlyList<MovieData> results = await filteredQuery.ToListAsync();

        return results.Count > 0
            ? Optional.Of(results)
            : Optional.Empty;
    }
}