using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public class EfCoreMovieRepository : EfCoreGenericRepository<Movie, MovieId>, IMovieRepository
{
    public EfCoreMovieRepository(DomainDbContext context) : base(context) { }

}