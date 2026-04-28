
using Microsoft.AspNetCore.Mvc;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.DTOs;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public class RegisterMovieController : ControllerBase
{
    private readonly RegisterMovie _registerMovieUseCase;

    public RegisterMovieController(RegisterMovie registerMovieUseCase)
    {
        _registerMovieUseCase = registerMovieUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterMovie([FromBody] RegisterMovieDTO movie)
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

        RegisterMovieOutput result = await _registerMovieUseCase.Execute(newMovie);
        return Ok(result);
    }
}