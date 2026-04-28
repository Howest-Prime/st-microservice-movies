using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Filters;


namespace Howestprime.Movies.Application.Movies;

public sealed record SearchMovieCatalogInput(string role , string? Title, string[]? Genre);

public class SearchMovieCatalog(
    ISearchMovieCatalogQuery moviesQuery,
    IAuthorizationService authorizationService
): IUseCase<SearchMovieCatalogInput, Optional<IReadOnlyList<MovieData>>>
{
    public async Task<Optional<IReadOnlyList<MovieData>>> Execute(SearchMovieCatalogInput input)
    {
       if (!Enum.TryParse<Role>(input.role?.Trim(), ignoreCase: true, out Role role))
            throw new UnauthorizedAccessException($"Invalid role '{input.role}'.");

       await authorizationService.Authorize(role, nameof(SearchMovieCatalog));
       IList<MovieGenre>? normalizedGenres = input.Genre?.Select(g => MovieGenre.Create(g)).ToList();

       return await moviesQuery.Fetch(
         MovieDataFilter.ByTitleAndGenre(input.Title, normalizedGenres)
        );

    }
}

