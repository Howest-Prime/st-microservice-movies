using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Microsoft.Extensions.Logging;

namespace Howestprime.Movies.Application.Movies;

public record RegisterMovieInput(string Title, string Description, int Duration, List<string> Genres,
    int ReleaseYear, List<string> Actors, int AgeRating, string PosterUrl);

public record RegisterMovieOutput(Guid Id);

public sealed class RegisterMovie(
    IUnitOfWork uow,
    ILogger<RegisterMovie> logger
) : IUseCase<RegisterMovieInput, RegisterMovieOutput>
{

    public async Task<RegisterMovieOutput> Execute(RegisterMovieInput input)
    {
        Movie movie = Movie.Create(
            EntityId.New<MovieId>(),
            MovieTitle.Create(input.Title),
            MovieDescription.Create(input.Description),
            input.Genres.Select(g => MovieGenre.Create(g)).ToList(),
            input.Actors.Select(a => Actor.Create(a)).ToList(),
            MovieReleaseYear.Create(input.ReleaseYear),
            MovieDuration.Create(input.Duration),
            AgeRating.From(input.AgeRating),
            PosterUrl.Create(input.PosterUrl)
        );

        await uow.Save<IMovieRepository>(movie);
        await uow.Do();

        logger.LogInformation("Movie registered: {MovieId}", movie.Id.Value);

        return new RegisterMovieOutput(movie.Id.Value);
    }
}