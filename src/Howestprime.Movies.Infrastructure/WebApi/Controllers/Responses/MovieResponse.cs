namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public record MovieResponse(
    Guid Id,
    string Title,
    string Description,
    int Duration,
    IReadOnlyList<string> Genres,
    int ReleaseYear,
    IReadOnlyList<string> Actors,
    int AgeRating,
    string PosterUrl
);
