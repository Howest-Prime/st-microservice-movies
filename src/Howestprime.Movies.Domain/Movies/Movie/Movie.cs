using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie.Events;

namespace Howestprime.Movies.Domain.Movies.Movie;

public class Movie: AggregateRoot<MovieId>
{
    public MovieTitle Title { get; private set; }
    public MovieDescription Description { get; private set; }
    public IReadOnlyList<MovieGenre> Genres { get; private set; }
    public MovieReleaseYear ReleaseYear { get; private set; }
    public MovieDuration Duration { get; private set; }
    public IReadOnlyList<Actor> Actors { get; private set; }
    public AgeRating AgeRating { get; private set; }
    public PosterUrl PosterUrl { get; private set; }

 
    private Movie() : base()
    {
        Title = default!;
        Description = default!;
        Genres = [];
        ReleaseYear = default!;
        Duration = default!;
        Actors = [];
        AgeRating = default!;
        PosterUrl = default!;
    }

    private Movie(MovieId id, MovieTitle title, MovieDescription description, List<MovieGenre> genres, List<Actor> actors, MovieReleaseYear releaseYear, MovieDuration duration, AgeRating ageRating, PosterUrl posterUrl) 
                : base(id)
    {
        Title = title;
        Description = description;
        Genres = genres;
        ReleaseYear = releaseYear;
        Duration = duration;
        Actors = actors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }

    public static Movie Create(MovieId id, MovieTitle title, MovieDescription description, List<MovieGenre> genres, MovieReleaseYear releaseYear, MovieDuration duration, List<Actor> actors, AgeRating ageRating, PosterUrl posterUrl)
    {
        Movie movie = new Movie(id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl);
        movie.ValidateState();
        movie.RaiseDomainEvent(
            MovieRegistered.Create(
                            movie.Id.Value.ToString(),
                            movie.Title.Value, 
                            movie.Genres.Select(g => g.Value).ToArray(), 
                            movie.Actors.Select(a => a.Value).ToArray(), 
                            movie.ReleaseYear.Value, 
                            movie.Duration.Value, 
                            movie.AgeRating.Value.ToString(), 
                            movie.PosterUrl.Value)
        )               ;
        return movie;
    }
        
    public override void ValidateState()
    {
     
    }
}