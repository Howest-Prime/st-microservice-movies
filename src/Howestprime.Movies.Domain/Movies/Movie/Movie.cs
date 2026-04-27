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
    public int AgeRating { get; private set; }
    public string PosterUrl { get; private set; }

    private Movie(MovieId id, MovieTitle title, MovieDescription description, List<MovieGenre> genres, List<Actor> actors, MovieReleaseYear releaseYear, MovieDuration duration, int ageRating, string posterUrl) 
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

    public static Movie Create(MovieId id, MovieTitle title, MovieDescription description, List<MovieGenre> genres, MovieReleaseYear releaseYear, MovieDuration duration, List<Actor> actors, int ageRating, string posterUrl)
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
                            movie.AgeRating, 
                            movie.PosterUrl)
        )               ;
        return movie;
    }
        
    public override void ValidateState()
    {
        throw new NotImplementedException();
    }
}