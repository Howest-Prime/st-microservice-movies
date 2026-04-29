using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.DTOs;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;


public sealed record RegisterMovieRequest(
        [FromBody] RegisterMovieBody Body,
        [FromServices] IUseCase<RegisterMovieInput, RegisterMovieOutput> useCase
);

public  static class RegisterMovieController
{
    public static async Task<IResult> Invoke(
        [AsParameters] RegisterMovieRequest request
    )
    {  
        RegisterMovieInput newMovie = new RegisterMovieInput(
            request.Body.Title,
            request.Body.Description,
            request.Body.Duration,
            request.Body.Genres.ToList(),
            request.Body.ReleaseYear,
            request.Body.Actors.ToList(),
            request.Body.AgeRating,
            request.Body.PosterUrl
        );

        RegisterMovieOutput result = await request.useCase.Execute(newMovie);
        
        return Results.Created($"/movies/{result.Id}", result);
    }
}