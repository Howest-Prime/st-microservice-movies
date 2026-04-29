using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

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

        if (!result.HasValue || result.Value is null)
            return Results.Ok(Enumerable.Empty<MovieResponse>());

        return Results.Ok(result.Value.Select(MapToResponse));
    }

    private static MovieResponse MapToResponse(MovieData movieData) =>
        new(
            movieData.Id.ToString(),
            movieData.Title,
            movieData.Description,
            movieData.Duration,
            movieData.Genres.Select(g => g.Value).ToList(),
            movieData.ReleaseYear,
            movieData.Actors.Select(a => a.Name).ToList(),
            movieData.AgeRating,
            movieData.PosterUrl
        );
}