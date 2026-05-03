using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Domain.Movies.Movie.Events;


public sealed class MovieDetailsChanged : MovieDomainEvent
{
    public MovieId MovieId { get; }
    public MovieTitle Title { get; }
    public MovieDescription Description { get; }
    public MovieReleaseYear ReleaseYear { get; }
    public IReadOnlyList<MovieGenre> Genres { get; }
    public IReadOnlyList<Actor> Actors { get; }
    public AgeRating AgeRating { get; }
    public PosterUrl PosterUrl { get; }

    public MovieDetailsChanged(
        MovieId movieId,
        MovieTitle title,
        MovieDescription description,
        MovieReleaseYear releaseYear,
        IReadOnlyList<MovieGenre> genres,
        IReadOnlyList<Actor> actors,
        AgeRating ageRating,
        PosterUrl posterUrl
    ): base(nameof(MovieDetailsChanged))
    {
        MovieId = movieId;
        Title = title;
        Description = description;
        ReleaseYear = releaseYear;
        Genres = genres;
        Actors = actors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }
}
