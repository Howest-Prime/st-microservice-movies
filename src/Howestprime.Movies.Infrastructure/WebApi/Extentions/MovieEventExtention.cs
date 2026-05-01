using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
namespace Howestprime.Movies.Infrastructure.WebApi.Extentions;

public static class MovieEventExtention
{
    public static MovieEventResponse ToMovieEventResponse(this MovieEventData movieEvent)
    {
       return new MovieEventResponse(
            Id: movieEvent.Id,
            ShowTime: movieEvent.ShowTime,
            Capacity: movieEvent.Capacity,
            MovieId: movieEvent.Movie.Id,
            MovieTitle: movieEvent.Movie.Title,
            MovieDescription: movieEvent.Movie.Description,
            MovieDuration: movieEvent.Movie.Duration,
            MovieReleaseYear: movieEvent.Movie.ReleaseYear,
            MovieAgeRating: movieEvent.Movie.AgeRating,
            MoviePosterUrl: movieEvent.Movie.PosterUrl,
            RoomId: movieEvent.Room.Id,
            RoomName: movieEvent.Room.Name,
            RoomCapacity: movieEvent.Room.Capacity
        );
    }

    public static IReadOnlyList<MovieEventResponse> ToMovieEventResponseList(this IEnumerable<MovieEventData> movieEvents)
    {
        return movieEvents.Select(me => me.ToMovieEventResponse()).ToList();
    }
}