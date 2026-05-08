using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Howestprime.Movies.Infrastructure.WebApi.Extentions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public sealed record SearchMovieByTitleAndGenresRequest(
    [FromHeader(Name = "X-User-Role")] string UserRole,
    [FromQuery] string? Title,
    [FromQuery] string[]? Genres,
    [FromServices] IUseCase<SearchMovieCatalogInput, Optional<IReadOnlyList<MovieData>>> SearchMovieCatalogUseCase
);

public static class SearchMovieByTitleAndGenresController
{
    public static async Task<Results<Ok<IReadOnlyList<MovieResponse>>, NotFound>> Invoke(
        [AsParameters] SearchMovieByTitleAndGenresRequest request
    )
    {
        SearchMovieCatalogInput input = new SearchMovieCatalogInput(request.UserRole, request.Title, request.Genres);

        Optional<IReadOnlyList<MovieData>> result = await request.SearchMovieCatalogUseCase.Execute(input);

        if (!result.HasValue)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(result.Value.ToMovieResponseList());
    }
}