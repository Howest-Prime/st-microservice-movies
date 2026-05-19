using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Domain.Movies.Movie.Events;

public sealed class MovieDetailsChanged : MovieDomainEvent
{
    public MovieId MovieId { get; }
    public string Title { get; }
    public string Description { get; }
    public int ReleaseYear { get; }
    public IReadOnlyList<string> Genres { get; }
    public IReadOnlyList<string> Actors { get; }
    public int AgeRating { get; }
    public string PosterUrl { get; }

    public MovieDetailsChanged(
        MovieId movieId,
        string title,
        string description,
        int releaseYear,
        IReadOnlyList<string> genres,
        IReadOnlyList<string> actors,
        int ageRating,
        string posterUrl
    ) : base(nameof(MovieDetailsChanged))
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
