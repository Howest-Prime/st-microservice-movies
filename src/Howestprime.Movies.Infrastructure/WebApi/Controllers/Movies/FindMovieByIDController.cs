using Aornis;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Howestprime.Movies.Infrastructure.WebApi.Extentions;
using Howestprime.Movies.Application.Contracts.Data.Movies;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public sealed record FindMovieByIDRequest(
    [FromHeader(Name = "X-User-Role")] string UserRole,
    [FromRoute] Guid MovieId,
    [FromServices] IUseCase<FindMovieByIDInput, Optional<MovieData>>UseCase
);

public static class FindMovieByIDController
{
    public static async Task<IResult> Invoke([AsParameters] FindMovieByIDRequest request)
    {
        FindMovieByIDInput input = new(request.UserRole, request.MovieId);
        Optional<MovieData> result = await request.UseCase.Execute(input);

        return result.HasValue
            ? Results.Ok(result.Value.ToMovieResponse())
            : Results.NotFound();
    }

}