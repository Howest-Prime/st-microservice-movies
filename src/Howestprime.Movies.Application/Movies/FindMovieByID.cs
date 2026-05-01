using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Data.Movies;
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

        IReadOnlyList<MovieData> movies = 
                    (await moviesQuery.Fetch(MovieDataFilter.ByID(input.MovieID)))
                    .OrElseThrow(() => new MovieNotFoundException(input.MovieID) );

        return movies.FirstOrDefault() ;
    }
}