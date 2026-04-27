
namespace Howestprime.Movies.Domain.Movies.Movie.Events;
public class MovieRegistered : MovieDomainEvent
{
    public string MovieId { get; private init; }
    public string Title { get; private init; }
    public string[] Genres { get; private init; }
    public DateTime ReleaseDate { get; private init; }

    public int Duration { get; private init; }

    public string[] Actors { get; private init; }

    public int AgeRating { get; private init; }

    public string PosterUrl { get; private init; }

    public MovieRegistered(string movieId, string title, string[] genres, DateTime releaseDate, int duration, string[] actors, int ageRating, string posterUrl) 
                        : base(nameof(MovieRegistered))
    {
        MovieId = movieId;
        Title = title;
        Genres = genres;
        ReleaseDate = releaseDate;
        Duration = duration;
        Actors = actors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }
}