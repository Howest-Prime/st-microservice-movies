
namespace Howestprime.Movies.Domain.Movies.Movie.Events;
public class MovieRegistered : MovieDomainEvent
{
    public string MovieId { get; private init; }
    public string Title { get; private init; }
    public string[] Genres { get; private init; }
    public string[] Actors { get; private init; }
    public int ReleaseYear { get; private init; }
    public int Duration { get; private init; }
  
    public int AgeRating { get; private init; }
    public string PosterUrl { get; private init; }

    private MovieRegistered(string movieId, string title, string[] genres,  string[] actors, int releaseYear, int duration, int ageRating, string posterUrl) 
                        : base(nameof(MovieRegistered))
    {
        MovieId = movieId;
        Title = title;
        Genres = genres;
        ReleaseYear = releaseYear;
        Duration = duration;
        Actors = actors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }

    public static MovieRegistered Create(string movieId, string title, string[] genres, string[] actors, int releaseYear, int duration, int ageRating, string            posterUrl)
    {
        return new MovieRegistered(movieId, title, genres, actors, releaseYear, duration, ageRating, posterUrl);
    }
}