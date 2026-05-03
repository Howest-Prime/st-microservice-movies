using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public sealed record ChangeMovieDetailsRequestBody(
    string Title,
    string Description,
    int ReleaseYear,
    List<string> Genres,
    List<string> Actors,
    int AgeRating,
    string PosterUrl
);

public sealed record ChangeMovieDetailsRequest(
  [FromRoute] Guid MovieId,
  [FromBody] ChangeMovieDetailsRequestBody Body,
  [FromServices] IUseCase<ChangeMovieDetailsInput, ChangeMovieDetailsOutput> UseCase
);

public static class ChangeMovieDetailsController
{
    public static async Task<IResult> Invoke([AsParameters] ChangeMovieDetailsRequest request)
    {
        ChangeMovieDetailsInput input = new(
            request.MovieId,
            request.Body.Title,
            request.Body.Description,
            request.Body.ReleaseYear,
            request.Body.Genres.ToArray(),
            request.Body.Actors.ToArray(),
            request.Body.AgeRating,
            request.Body.PosterUrl
        );

        ChangeMovieDetailsOutput output = await request.UseCase.Execute(input);

        return Results.Created($"/movies/{output.MovieId}", output);

    }
}