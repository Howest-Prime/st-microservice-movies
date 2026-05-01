using Aornis;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Howestprime.Movies.Infrastructure.WebApi.Extentions;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public sealed record SearchMovieByTitleAndGenresRequest(
    [FromHeader(Name = "X-User-Role")] string UserRole,
    [FromQuery] string? Title,
    [FromQuery] string[]? Genres,
    [FromServices] IUseCase<SearchMovieCatalogInput, Optional<IReadOnlyList<MovieData>>> SearchMovieCatalogUseCase
);

public static class SearchMovieByTitleAndGenresController
{
    public static async Task<IResult> Invoke(
        [AsParameters] SearchMovieByTitleAndGenresRequest request
    )
    {
        SearchMovieCatalogInput input = new SearchMovieCatalogInput(request.UserRole, request.Title, request.Genres);

        Optional<IReadOnlyList<MovieData>> result = await request.SearchMovieCatalogUseCase.Execute(input);

        return Results.Ok(
            result.HasValue
                ? result.Value.ToMovieResponseList()
                : Enumerable.Empty<MovieResponse>()
    );
    }
}