using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Shared.Exceptions;

namespace Howestprime.Movies.Application.Movies;

public sealed record FindMovieByIdInput(string Role, Guid MovieId);

public sealed class FindMovieById(
    ISearchMovieCatalogQuery moviesQuery,
    IAuthorizationService authorizationService
) : IUseCase<FindMovieByIdInput, Optional<MovieData>>
{
    public async Task<Optional<MovieData>> Execute(FindMovieByIdInput input)
    {
        await authorizationService.Authorize(input.Role, nameof(FindMovieById));

        IReadOnlyList<MovieData>? movies = 
                    (await moviesQuery.Fetch(MovieDataFilter.ById(input.MovieId)))
                    .OrElseThrow(() => new NotFoundException($"Movie with id {input.MovieId} not found."));

        return movies.FirstOrDefault() ;
    }
}