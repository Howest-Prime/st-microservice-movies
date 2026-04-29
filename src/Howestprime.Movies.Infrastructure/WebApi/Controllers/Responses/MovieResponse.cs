namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public record MovieResponse(
    string Id,
    string Title,
    string Description,
    int Duration,
    IList<string> Genres,
    int ReleaseYear,
    IList<string> Actors,
    int AgeRating,
    string PosterUrl
);
