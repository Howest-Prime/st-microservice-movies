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
            Movie: new MovieResponse(
                Id: movieEvent.Movie.Id,
                Title: movieEvent.Movie.Title,
                Description: movieEvent.Movie.Description,
                Duration: movieEvent.Movie.Duration,
                Genres: movieEvent.Movie.Genres.Select(g => g.Value).ToList(),
                ReleaseYear: movieEvent.Movie.ReleaseYear,
                Actors: movieEvent.Movie.Actors.Select(a => a.Name).ToList(),
                AgeRating: movieEvent.Movie.AgeRating,
                PosterUrl: movieEvent.Movie.PosterUrl
            ),
            Room: new RoomResponse(
                Id: movieEvent.Room.Id,
                Name: movieEvent.Room.Name,
                Capacity: movieEvent.Room.Capacity
            )
        );
    }

    public static IReadOnlyList<MovieEventResponse> ToMovieEventResponseList(this IEnumerable<MovieEventData> movieEvents)
    {
        return movieEvents.Select(me => me.ToMovieEventResponse()).ToList();
    }
}