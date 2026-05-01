using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

namespace Howestprime.Movies.Infrastructure.WebApi.Extentions;

public static class HowestPrimeScheduleExtention
{
    public static HowestPrimeScheduleResponse ToHowestPrimeScheduleResponse(this MovieEventData movieEventData)
    {
        return new HowestPrimeScheduleResponse(
            MovieIds: new List<Guid> { movieEventData.Movie.Id },
            MovieEvents: new List<MovieEventResponse> { movieEventData.ToMovieEventResponse() }
        );
    }

    public static IReadOnlyList<HowestPrimeScheduleResponse> ToHowestPrimeScheduleResponseList(this IReadOnlyList<MovieEventData> movieEventDatas)
    {
        return movieEventDatas.Select(m => m.ToHowestPrimeScheduleResponse()).ToList();
    }
}