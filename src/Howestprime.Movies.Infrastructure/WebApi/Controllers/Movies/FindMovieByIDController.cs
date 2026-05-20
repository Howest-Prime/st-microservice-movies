using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Microsoft.AspNetCore.Mvc;
using Howestprime.Movies.Infrastructure.WebApi.Extentions;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Microsoft.AspNetCore.Http.HttpResults;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public sealed record FindMovieByIdRequest(
    [FromHeader(Name = "X-User-Role")] string UserRole,
    [FromRoute] Guid MovieId,
    [FromServices] IUseCase<FindMovieByIdInput, MovieData> UseCase
);

public static class FindMovieByIdController
{
    public static async Task<Results<Ok<MovieResponse>, BadRequest>> Invoke([AsParameters] FindMovieByIdRequest request)
    {
        FindMovieByIdInput input = new(request.UserRole, request.MovieId);
        MovieData result = await request.UseCase.Execute(input);
        return TypedResults.Ok(result.ToMovieResponse());
    }
}