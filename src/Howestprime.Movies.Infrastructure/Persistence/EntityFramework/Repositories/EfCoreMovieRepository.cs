using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public class EfCoreMovieRepository : EfCoreGenericRepository<Movie, MovieId>, IMovieRepository
{
    public EfCoreMovieRepository(DomainDbContext context) : base(context) { }

    public async Task AddAsync(Movie movie)
    {
        await _context.Set<Movie>().AddAsync(movie);
    }

    public async Task<Movie?> ByIdAsync(MovieId id)
    {
        return await _context.Set<Movie>().FindAsync(id);
    }

    public async Task UpdateAsync(Movie movie)
    {
        _context.Set<Movie>().Update(movie);
    }

    public async Task DeleteAsync(MovieId id)
    {
        Movie? movie = await _context.Set<Movie>().FindAsync(id);
        if (movie is not null)
        {
            _context.Set<Movie>().Remove(movie);
        }
            
    }
}