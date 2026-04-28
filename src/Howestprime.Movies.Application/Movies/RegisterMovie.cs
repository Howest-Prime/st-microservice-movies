using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Application.Movies.Exceptions;

namespace Howestprime.Movies.Application.Movies;

public record RegisterMovieInput (string Title, string Description, int Duration, string[] Genres, 
                                    int ReleaseYear, string[] Actors, int AgeRating, string PosterUrl);
public record RegisterMovieOutput(string Id);


public class RegisterMovie: IUseCase<RegisterMovieInput, RegisterMovieOutput>
{
    private readonly IUnitOfWork _uow;

    public RegisterMovie(IUnitOfWork uow )
    {
        _uow = uow;
    }
    public async Task<RegisterMovieOutput> Execute(RegisterMovieInput input)
    {
        Movie movie = Movie.Create(
            EntityId.New<MovieId>(),
            MovieTitle.Create(input.Title),
            MovieDescription.Create(input.Description),
            input.Genres.Select(g => MovieGenre.Create(g)).ToList(), 
            MovieReleaseYear.Create(input.ReleaseYear) ,
            MovieDuration.Create(input.Duration),
            input.Actors.Select(a => Actor.Create(a)).ToList(),
            AgeRating.Create((AgeRatingValue)input.AgeRating),
            PosterUrl.Create(input.PosterUrl)
           
        );

        IMovieRepository movieRepository = _uow.Repo<IMovieRepository>();

        if(movieRepository == null)
        {
            throw new RepositoryNotFoundException(nameof(IMovieRepository));
        }
        
        await movieRepository.AddAsync(movie);
        await _uow.Save<IMovieRepository>(movie);
        await _uow.Do();
        
        return new RegisterMovieOutput(movie.Id.Value.ToString());
    }
}