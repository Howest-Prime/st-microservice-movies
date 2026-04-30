
using System.Linq.Expressions;

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