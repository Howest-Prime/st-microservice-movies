using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
namespace Howestprime.Movies.Infrastructure.WebApi.Extentions;

public static class MovieExtention
{
    public static MovieResponse ToMovieResponse(this MovieData movie)
    {
        return new MovieResponse(
            Id: movie.Id.ToString(),
            Title: movie.Title,
            Description: movie.Description,
            Duration: movie.Duration,
            Genres: movie.Genres.Select(g => g.Value).ToList(),
            ReleaseYear: movie.ReleaseYear,
            Actors: movie.Actors.Select(a => a.Name).ToList(),
            AgeRating: (int)movie.AgeRating,
            PosterUrl: movie.PosterUrl
        );
    }

    public static IReadOnlyList<MovieResponse> ToMovieResponseList(this IEnumerable<MovieData> movies)
    {
        return movies.Select(m => m.ToMovieResponse()).ToList();
    }
}