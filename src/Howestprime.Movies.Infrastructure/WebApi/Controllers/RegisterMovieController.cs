using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.DTOs;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public static class RegisterMovieController
{
    public static async Task<IResult> Invoke(
        [FromBody] RegisterMovieDTO movie,
        IUseCase<RegisterMovieInput, RegisterMovieOutput> registerMovieUseCase
    )
    {  
        RegisterMovieInput newMovie = new RegisterMovieInput(
            movie.Title,
            movie.Description,
            movie.Duration,
            movie.Genres,
            movie.ReleaseYear,
            movie.Actors,
            movie.AgeRating,
            movie.PosterUrl
        );

        RegisterMovieOutput result = await registerMovieUseCase.Execute(newMovie);
        return Results.Ok(result);
    }
}