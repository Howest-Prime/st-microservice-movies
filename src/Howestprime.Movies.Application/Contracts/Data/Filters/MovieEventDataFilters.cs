using System.Linq.Expressions;
using Howestprime.Movies.Application.Contracts.Data.MovieEvents;

namespace Howestprime.Movies.Application.Contracts.Data.Filters;

public static class MovieEventDataFilters
{
    public static Expression<Func<MovieEventData, bool>> InTimeRange(
        DateTime from,
        DateTime to
    )
    {
        return movieEvent => movieEvent.ShowTime >= from && movieEvent.ShowTime <= to;
    }
    
       
}