using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Shared.Exceptions;

namespace Howestprime.Movies.Application.Movies;

public sealed record FindMovieByIdInput(string Role, Guid MovieId);

public sealed class FindMovieById(
    ISearchMovieCatalogQuery moviesQuery,
    IAuthorizationService authorizationService
) : IUseCase<FindMovieByIdInput, MovieData>
{
    public async Task<MovieData> Execute(FindMovieByIdInput input)
    {
        await authorizationService.Authorize(input.Role, nameof(FindMovieById));

        IReadOnlyList<MovieData> movies = (await moviesQuery.Fetch(MovieDataFilter.ById(input.MovieId))).OrElse([]);
        
        return movies.Count > 0 ? movies[0]
            : throw new NotFoundException($"Movie with id {input.MovieId} not found");
    }
}