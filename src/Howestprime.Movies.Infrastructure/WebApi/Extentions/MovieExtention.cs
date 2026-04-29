using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
namespace Howestprime.Movies.Infrastructure.WebApi.Extentions;

public static class MovieExtention
{
    public static MovieResponse ToMovieResponse(this Movie movie)
    {
        return new MovieResponse(
            Id: movie.Id.Value.ToString(),
            Title: movie.Title.Value,
            Description: movie.Description.Value,
            Duration: movie.Duration.Value,
            Genres: movie.Genres.Select(g => g.Value).ToList(),
            ReleaseYear: movie.ReleaseYear.Value,
            Actors: movie.Actors.Select(a => a.Name).ToList(),
            AgeRating: (int)movie.AgeRating.Value,
            PosterUrl: movie.PosterUrl.Value
        );
    }

    public static IReadOnlyList<MovieResponse> ToMovieResponseList(this IEnumerable<Movie> movies)
    {
        return movies.Select(m => m.ToMovieResponse()).ToList();
    }
}