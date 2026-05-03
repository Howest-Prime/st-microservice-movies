using System.Linq.Expressions;
using Howestprime.Movies.Application.Contracts.Data.Movies;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Application.Contracts.Data.Filters;

public static class MovieDataFilter
{
    public static Expression<Func<MovieData, bool>> ByTitleAndGenre(string? title, IList<MovieGenre>? genres)
    {
        List<string> genreValues = genres == null
                        ? new List<string>() 
                        : genres.Select(g => g.Value.ToLower()).ToList();

        string normalizedTitle = title == null
                        ? string.Empty
                        : title.ToLower();

        return movie =>
                (string.IsNullOrWhiteSpace(title) ||
                    movie.Title.ToLower().Contains(normalizedTitle)) &&
                (!genreValues.Any() ||  genreValues.All(g =>
                                        movie.Genres.Any(genre =>
                                            genre.Value.ToLower() == g
        )));
    }

    public static Expression<Func<MovieData, bool>> ById(Guid id)
    {
        return movie => movie.Id == id;
    } 
}