using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Domain.Movies.Movie.Events;
public sealed class MovieRegistered : MovieDomainEvent
{
    public MovieId MovieId { get; private init; }
    public string Title { get; private init; }
    public IReadOnlyList<string> Genres { get; private init; }
    public IReadOnlyList<string> Actors { get; private init; }
    public int ReleaseYear { get; private init; }
    public int Duration { get; private init; }
    public int AgeRating { get; private init; }
    public string PosterUrl { get; private init; }

    private MovieRegistered(
        MovieId movieId,
        string title,
        IReadOnlyList<string> genres,
        IReadOnlyList<string> actors,
        int releaseYear,
        int duration,
        int ageRating,
        string posterUrl
    ) : base(nameof(MovieRegistered))
    {
        MovieId = movieId;
        Title = title;
        Genres = genres;
        Actors = actors;
        ReleaseYear = releaseYear;
        Duration = duration;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }

    public static MovieRegistered Create(
        MovieId movieId,
        string title,
        IReadOnlyList<string> genres,
        IReadOnlyList<string> actors,
        int releaseYear,
        int duration,
        int ageRating,
        string posterUrl)
    {
        return new MovieRegistered(
            movieId, title, genres, actors, releaseYear, duration, ageRating, posterUrl
        );
    }
}