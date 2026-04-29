

using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies.Exceptions;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;

namespace Howestprime.Movies.Application.Movies;

public sealed record FindMovieByIDInput(string Role, Guid MovieID);

public sealed class FindMovieByID(
    ISearchMovieCatalogQuery moviesQuery,
    IAuthorizationService authorizationService
) : IUseCase<FindMovieByIDInput, Optional<MovieData>>
{
    public async Task<Optional<MovieData>> Execute(FindMovieByIDInput input)
    {
        string normalizedRole = input.Role?.Trim() ?? string.Empty;
        
        if (!Enum.TryParse<Role>(normalizedRole, ignoreCase: true, out Role role))
        {
            throw new UnauthorizedAccessException($"Invalid role '{input.Role}'.");
        }

        await authorizationService.Authorize(role, nameof(FindMovieByID));
        Optional<IReadOnlyList<MovieData>> moviesResult = await moviesQuery.Fetch(MovieDataFilter.ByID(input.MovieID));
        
        if(moviesResult.HasValue && moviesResult.Value is not null)
        {
            return moviesResult.Value.FirstOrDefault();
        }
        
        throw new MovieNotFoundException(input.MovieID);
    }
}