using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record FindMovieByIDRequest(
    [FromHeader(Name = "X-User-Role")] string UserRole,
    [FromRoute] string Id,
    [FromServices] IUseCase<FindMovieByIDInput, Optional<MovieData>> FindMovieByIDUseCase
);



public static class FindMovieByIDController
{
    public static async Task<IResult> Invoke(
        [AsParameters] FindMovieByIDRequest request
    )
    {
        if (!Guid.TryParse(request.Id, out Guid movieId))
        {
            return Results.BadRequest($"Invalid movie ID '{request.Id}'.");
        }

        FindMovieByIDInput input = new FindMovieByIDInput(request.UserRole, movieId);
        Optional<MovieData> result = await request.FindMovieByIDUseCase.Execute(input);

        Console.WriteLine($"FindMovieByIDController: Fetched movie for ID '{movieId}': {(result.HasValue ? "Found" : "Not found")}");
        if (!result.HasValue || result.Value is null)
            return Results.NotFound();

        return Results.Ok(MapToResponse(result.Value));
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