
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
        Expression<Func<MovieData, bool>> filter
    )
    {
         return await context.Set<MovieData>()
            .Where(filter)
            .Include(m => m.Genres)
            .Include(m => m.Actors)
            .ToListAsync() ?? new List<MovieData>();
       
    }
    
}