using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
namespace Howestprime.Movies.Infrastructure.WebApi.Extentions;

public static class HowestPrimeScheduleExtention
{
    public static HowestPrimeScheduleResponse ToHowestPrimeScheduleResponse(this MovieEventData movieEventData)
    {
        return new HowestPrimeScheduleResponse(
           new List<Guid> { movieEventData.Id },
           new List<MovieEventData>()
        );
    }

    public static IReadOnlyList<HowestPrimeScheduleResponse> ToHowestPrimeScheduleResponse(this IReadOnlyList<MovieEventData> movieEventDatas)
    {
        return movieEventDatas.Select(m => m.ToHowestPrimeScheduleResponse()).ToList();
    }
}