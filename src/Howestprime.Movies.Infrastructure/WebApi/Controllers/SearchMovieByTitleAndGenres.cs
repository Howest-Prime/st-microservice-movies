

using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public static class SearchMovieByTitleAndGenres
{
    public static async Task<IResult> Invoke(
        [FromQuery] string? title,
        [FromQuery] string[]? genres,
        IUseCase<SearchMovieCatalogInput, Optional<IReadOnlyList<MovieData>>> searchMovieCatalogUseCase
    )
    {
        SearchMovieCatalogInput input = new SearchMovieCatalogInput(title, genres);
        Optional<IReadOnlyList<MovieData>> result = await searchMovieCatalogUseCase.Execute(input);

        return Results.Ok(result);
    }
}