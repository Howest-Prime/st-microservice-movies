using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Aornis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Howestprime.Movies.Application.Contracts.Data.Movies;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries.Movies;

public sealed class SearchMovieCatalog(QueryDbContext context) : ISearchMovieCatalogQuery
{
    public async Task<Optional<IReadOnlyList<MovieData>>> Fetch(
        Expression<Func<MovieData, bool>>? filter = null
    )
    {
        IQueryable<MovieData> query = context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Actors)
            .AsNoTracking();

        IQueryable<MovieData> filteredQuery = query
            .Where(filter ?? (m => true));

        IReadOnlyList<MovieData>? results = await filteredQuery.ToListAsync();

        return Optional.Of(results);
    }
}