using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie;
using Microsoft.Extensions.Logging;
using Howestprime.Movies.Application.Movies.Exceptions;

namespace Howestprime.Movies.Application.Movies;

public record ChangeMovieDetailsInput(
    Guid MovieId,
    string Title,
    string Description,
    int ReleaseYear,
    string[] Genre,
    string[] Actors,
    int ageRating,
    string PosterUrl
);

public record ChangeMovieDetailsOutput(
    Guid MovieId,
    string Title,
    string Description,
    int ReleaseYear,
    string[] Genre,
    string[] Actors,
    string ageRating,
    string PosterUrl
);

public sealed class ChangeMovieDetails(
    IUnitOfWork unitOfWork,
    ILogger<ChangeMovieDetails> logger
) : IUseCase<ChangeMovieDetailsInput, ChangeMovieDetailsOutput>
{
    public async Task<ChangeMovieDetailsOutput> Execute(ChangeMovieDetailsInput input)
    {

        MovieId movieId = EntityId.New<MovieId>(input.MovieId);
        Movie? movie = (await unitOfWork.Repo<IMovieRepository>().ById(movieId))
                        .OrElseThrow(() =>new MovieNotFoundException(movieId.Value));

        movie.ChangeDetails(
            MovieTitle.Create(input.Title),
            MovieDescription.Create(input.Description),
            MovieReleaseYear.Create(input.ReleaseYear),
            input.Genre.Select(g => MovieGenre.Create(g)).ToList(),
            input.Actors.Select(a => Actor.Create(a)).ToList(),
            AgeRating.From(input.ageRating),
            PosterUrl.Create(input.PosterUrl)
        );

        await unitOfWork.Save<IMovieRepository>(movie);
        await unitOfWork.Do();

        logger.LogInformation("Movie details changed: {MovieId}", movie.Id.Value);

        return new ChangeMovieDetailsOutput(
            movie.Id.Value,
            movie.Title.Value,
            movie.Description.Value,
            movie.ReleaseYear.Value,
            movie.Genres.Select(g => g.Value).ToArray(),
            movie.Actors.Select(a => a.Name).ToArray(),
            movie.AgeRating.Value.ToString(),
            movie.PosterUrl.Value
        );
    }
        

}
   
