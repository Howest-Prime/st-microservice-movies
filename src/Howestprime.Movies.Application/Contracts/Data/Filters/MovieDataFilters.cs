using System.Linq.Expressions;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Application.Contracts.Data.Filters;

public static class MovieDataFilter
{
    public static Expression<Func<MovieData, bool>> ByTitleAndGenre(string? title, IList<MovieGenre>? genres)
    {
    return movie =>
        (string.IsNullOrWhiteSpace(title) || movie.Title.ToLower().Contains(title.ToLower())) &&
        (genres == null || genres.Count == 0 || genres.All(g => movie.Genres.Any(genre => genre.Value.ToLower().Contains(g.Value.ToLower()))));
    }

    public static Expression<Func<MovieData, bool>> ByID(Guid id)
    {
        return movie => movie.Id == id;
    } 
}