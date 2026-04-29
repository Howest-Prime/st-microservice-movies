

using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies.Exceptions;

namespace Howestprime.Movies.Application.Movies;

public sealed record FindMovieByIDInput(string Role, Guid MovieID);

public sealed class FindMovieByID(
    ISearchMovieCatalogQuery moviesQuery,
    IAuthorizationService authorizationService
) : IUseCase<FindMovieByIDInput, Optional<MovieData>>
{
    public async Task<Optional<MovieData>> Execute(FindMovieByIDInput input)
    {
        await authorizationService.Authorize(input.Role, nameof(FindMovieByID));

        Optional<IReadOnlyList<MovieData>> movies = await moviesQuery.Fetch(MovieDataFilter.ByID(input.MovieID));

        if(movies.Value.Count < 1)
        {
            throw new MovieNotFoundException(input.MovieID);
        }
        
        return movies.Value.FirstOrDefault() ;
    }
}