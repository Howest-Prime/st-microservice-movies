using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie.Events;

namespace Howestprime.Movies.Domain.Movies.Movie;

public sealed class Movie : AggregateRoot<MovieId>
{
    public MovieTitle Title { get; private set; }
    public MovieDescription Description { get; private set; }
    public MovieReleaseYear ReleaseYear { get; private set; }
    public MovieDuration Duration { get; private set; }
    public AgeRating AgeRating { get; private set; }
    public PosterUrl PosterUrl { get; private set; }

    public IReadOnlyList<Actor> Actors => _actors.AsReadOnly();
    public IReadOnlyList<MovieGenre> Genres => _genres.AsReadOnly();

    private List<Actor> _actors = new List<Actor>();
    private List<MovieGenre> _genres = new List<MovieGenre>();

    private Movie() : base(){}

    private Movie(
        MovieId id,
        MovieTitle title,
        MovieDescription description,
        List<MovieGenre> genres,
        List<Actor> actors,
        MovieReleaseYear releaseYear,
        MovieDuration duration,
        AgeRating ageRating,
        PosterUrl posterUrl
    ) : base(id)
    {
        Title = title;
        Description = description;
        _genres = genres;
        _actors = actors;
        ReleaseYear = releaseYear;
        Duration = duration;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }

    public static Movie Create(
        MovieId id,
        MovieTitle title,
        MovieDescription description,
        List<MovieGenre> genres,
        List<Actor> actors,
        MovieReleaseYear releaseYear,
        MovieDuration duration,
        AgeRating ageRating,
        PosterUrl posterUrl
    )
    {
        Movie movie = new(id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl);
        movie.ValidateState();
        movie.RaiseDomainEvent(
            MovieRegistered.Create(id, title, genres, actors, releaseYear, duration, ageRating, posterUrl)
        );
        return movie;
    }

    public void ChangeDetails(MovieTitle movieTitle, MovieDescription movieDescription, MovieReleaseYear movieReleaseYear, List<MovieGenre> movieGenres, List<Actor> actors, AgeRating ageRating, PosterUrl posterUrl)
    {
        Title = movieTitle;
        Description = movieDescription;
        ReleaseYear = movieReleaseYear;
        _genres = movieGenres;
        _actors = actors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;

        RaiseDomainEvent(
            new MovieDetailsChanged(Id, Title, Description, ReleaseYear, Genres, Actors, AgeRating, PosterUrl)
        );

        
    }

    public override void ValidateState() { }


}