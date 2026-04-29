
namespace Howestprime.Movies.Infrastructure.WebApi.DTOs;

public record RegisterMovieBody(
    string Title,
    string Description,
    int Duration,
    string[] Genres,
    int ReleaseYear,
    string[] Actors,
    int AgeRating,
    string PosterUrl
);