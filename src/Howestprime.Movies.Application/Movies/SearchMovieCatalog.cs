using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Data.Movies;


namespace Howestprime.Movies.Application.Movies;

public sealed record SearchMovieCatalogInput(string Role, string? Title, string[]? Genre);

public sealed class SearchMovieCatalog(
    ISearchMovieCatalogQuery moviesQuery,
    IAuthorizationService authorizationService
): IUseCase<SearchMovieCatalogInput, Optional<IReadOnlyList<MovieData>>>
{
    public async Task<Optional<IReadOnlyList<MovieData>>> Execute(SearchMovieCatalogInput input)
    {
       await authorizationService.Authorize(input.Role, nameof(SearchMovieCatalog));
       
       IList<MovieGenre>? normalizedGenres = input.Genre?.Select(g => MovieGenre.Create(g)).ToList();

       return await moviesQuery.Fetch(
         MovieDataFilter.ByTitleAndGenre(input.Title, normalizedGenres)
        );

    }
}

