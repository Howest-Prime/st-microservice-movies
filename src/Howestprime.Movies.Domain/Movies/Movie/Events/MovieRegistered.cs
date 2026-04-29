using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Domain.Movies.Movie.Events;
public class MovieRegistered : MovieDomainEvent
{
    public MovieId MovieId { get; private init; }
    public MovieTitle Title { get; private init; }
    public IReadOnlyList<MovieGenre> Genres { get; private init; }
    public IReadOnlyList<Actor> Actors { get; private init; }
    public MovieReleaseYear ReleaseYear { get; private init; }
    public MovieDuration Duration { get; private init; }
    public AgeRating AgeRating { get; private init; }
    public PosterUrl PosterUrl { get; private init; }

    private MovieRegistered(
        MovieId movieId, 
        MovieTitle title, 
        IReadOnlyList<MovieGenre> genres, 
        IReadOnlyList<Actor> actors, 
        MovieReleaseYear releaseYear, 
        MovieDuration duration, 
        AgeRating ageRating, 
        PosterUrl posterUrl
    ) : base(nameof(MovieRegistered))
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

    public static MovieRegistered Create(
        MovieId movieId, 
        MovieTitle title, 
        IReadOnlyList<MovieGenre> genres, 
        IReadOnlyList<Actor> actors, 
        MovieReleaseYear releaseYear, 
        MovieDuration duration, 
        AgeRating ageRating, 
        PosterUrl posterUrl)
    {
        return new MovieRegistered(
            movieId, title, genres, actors, releaseYear, duration, ageRating, posterUrl
        );
    }
}