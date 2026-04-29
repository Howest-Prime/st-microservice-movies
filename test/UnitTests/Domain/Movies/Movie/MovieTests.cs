using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.Events;
using Howestprime.Movies.Domain.Movies.Movie.Exceptions;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace UnitTests.Domain.Movies.Movie;

public sealed class MovieTests
{
    [Fact]
    public void Create_WithValidMinimalInput_ShouldInitializeState()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Test Movie");
        MovieDescription description = MovieDescription.Create("A test movie");
        List<MovieGenre> genres = [MovieGenre.Create("Drama")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(90);
        AgeRating ageRating = AgeRating.From(0);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/poster.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(id, movie.Id);
        Assert.Equal(title, movie.Title);
        Assert.Equal(description, movie.Description);
        Assert.Equal(releaseYear, movie.ReleaseYear);
        Assert.Equal(duration, movie.Duration);
        Assert.Equal(ageRating, movie.AgeRating);
        Assert.Equal(posterUrl, movie.PosterUrl);
    }

    [Fact]
    public void Create_WithValidMultipleGenresAndActors_ShouldInitializeCollections()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Multi-Genre Film");
        MovieDescription description = MovieDescription.Create("A complex narrative film");
        List<MovieGenre> genres = [MovieGenre.Create("Drama"), MovieGenre.Create("Thriller"), MovieGenre.Create("Mystery")];
        List<Actor> actors = [Actor.Create("Actor One"), Actor.Create("Actor Two"), Actor.Create("Actor Three")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2015);
        MovieDuration duration = MovieDuration.Create(150);
        AgeRating ageRating = AgeRating.From(16);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/complex.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(3, movie.Genres.Count);
        Assert.Equal(3, movie.Actors.Count);
        Assert.Contains(genres[0], movie.Genres);
        Assert.Contains(actors[0], movie.Actors);
    }

    [Fact]
    public void Create_WithEmptyGenres_ShouldInitializeEmptyGenreCollection()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("No Genre Film");
        MovieDescription description = MovieDescription.Create("Unclassified");
        List<MovieGenre> genres = [];
        List<Actor> actors = [Actor.Create("Solo Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2010);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/unclassified.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Empty(movie.Genres);
    }

    [Fact]
    public void Create_WithEmptyActors_ShouldInitializeEmptyActorCollection()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("No Cast Film");
        MovieDescription description = MovieDescription.Create("Documentary");
        List<MovieGenre> genres = [MovieGenre.Create("Documentary")];
        List<Actor> actors = [];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2018);
        MovieDuration duration = MovieDuration.Create(85);
        AgeRating ageRating = AgeRating.From(0);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/documentary.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Empty(movie.Actors);
    }

    [Fact]
    public void Create_WithInvalidTitle_ShouldThrowInvalidMovieTitleException()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieDescription description = MovieDescription.Create("Valid");
        List<MovieGenre> genres = [MovieGenre.Create("Action")];
        List<Actor> actors = [Actor.Create("Valid Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/valid.jpg");
        
        Action act = () => Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, MovieTitle.Create(new string('X', 51)), description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Act & Assert
        Assert.Throws<InvalidMovieTitleException>(act);
    }

    [Fact]
    public void Create_WithInvalidDescription_ShouldThrowInvalidMovieDescriptionException()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Valid Title");
        List<MovieGenre> genres = [MovieGenre.Create("Action")];
        List<Actor> actors = [Actor.Create("Valid Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/valid.jpg");
        
        Action act = () => Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, MovieDescription.Create(new string('Y', 301)), genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Act & Assert
        Assert.Throws<InvalidMovieDescriptionException>(act);
    }

    [Fact]
    public void Create_WithInvalidDuration_ShouldThrowInvalidMovieDurationException()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Valid Title");
        MovieDescription description = MovieDescription.Create("Valid");
        List<MovieGenre> genres = [MovieGenre.Create("Action")];
        List<Actor> actors = [Actor.Create("Valid Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/valid.jpg");
        
        Action act = () => Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, MovieDuration.Create(0), ageRating, posterUrl
        );

        // Act & Assert
        Assert.Throws<InvalidMovieDurationException>(act);
    }

    [Fact]
    public void Create_WithInvalidReleaseYear_ShouldThrowInvalidMovieReleaseYearException()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Valid Title");
        MovieDescription description = MovieDescription.Create("Valid");
        List<MovieGenre> genres = [MovieGenre.Create("Action")];
        List<Actor> actors = [Actor.Create("Valid Actor")];
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/valid.jpg");
        
        Action act = () => Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, MovieReleaseYear.Create(1887), duration, ageRating, posterUrl
        );

        // Act & Assert
        Assert.Throws<InvalidMovieReleaseYearException>(act);
    }

    [Fact]
    public void Create_WithInvalidAgeRating_ShouldThrowInvalidAgeRatingException()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Valid Title");
        MovieDescription description = MovieDescription.Create("Valid");
        List<MovieGenre> genres = [MovieGenre.Create("Action")];
        List<Actor> actors = [Actor.Create("Valid Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/valid.jpg");
        
        Action act = () => Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, AgeRating.From(99), posterUrl
        );

        // Act & Assert
        Assert.Throws<InvalidAgeRatingException>(act);
    }

    [Fact]
    public void Create_WithInvalidGenre_ShouldThrowInvalidMovieGenreException()
    {
        // Arrange
        string oversizedGenre = new('G', 51);

        // Act & Assert
        Assert.Throws<InvalidMovieGenreException>(() => MovieGenre.Create(oversizedGenre));
    }

    [Fact]
    public void Create_WithInvalidActor_ShouldThrowInvalidActorException()
    {
        // Arrange
        string whitespaceActor = "   ";

        // Act & Assert
        Assert.Throws<InvalidActorException>(() => Actor.Create(whitespaceActor));
    }

    [Fact]
    public void Create_WithInvalidPosterUrl_ShouldThrowInvalidMoviePosterUrlException()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Valid Title");
        MovieDescription description = MovieDescription.Create("Valid");
        List<MovieGenre> genres = [MovieGenre.Create("Action")];
        List<Actor> actors = [Actor.Create("Valid Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        
        Action act = () => Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, PosterUrl.Create("   ")
        );

        // Act & Assert
        Assert.Throws<InvalidMoviePosterUrlException>(act);
    }

    [Fact]
    public void Create_WithMinimumBoundaryReleaseYear_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Ancient Cinema");
        MovieDescription description = MovieDescription.Create("Earliest known film");
        List<MovieGenre> genres = [MovieGenre.Create("Historical")];
        List<Actor> actors = [Actor.Create("Pioneer")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(1888);
        MovieDuration duration = MovieDuration.Create(1);
        AgeRating ageRating = AgeRating.From(0);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/ancient.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(1888, movie.ReleaseYear.Value);
    }

    [Fact]
    public void Create_WithCurrentYearBoundary_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Current Film");
        MovieDescription description = MovieDescription.Create("Just released");
        List<MovieGenre> genres = [MovieGenre.Create("Contemporary")];
        List<Actor> actors = [Actor.Create("Modern Actor")];
        int currentYear = DateTime.Now.Year;
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(currentYear);
        MovieDuration duration = MovieDuration.Create(120);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/current.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(currentYear, movie.ReleaseYear.Value);
    }

    [Fact]
    public void Create_WithMinimumDurationBoundary_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Short Film");
        MovieDescription description = MovieDescription.Create("One minute");
        List<MovieGenre> genres = [MovieGenre.Create("Short")];
        List<Actor> actors = [Actor.Create("Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(1);
        AgeRating ageRating = AgeRating.From(0);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/short.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(1, movie.Duration.Value);
    }

    [Fact]
    public void Create_WithAllAgeRatingValues_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        List<MovieGenre> genres = [MovieGenre.Create("Test")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(90);
        int[] validRatings = [0, 6, 9, 12, 14, 16, 18];

        foreach (int rating in validRatings)
        {
            MovieTitle title = MovieTitle.Create($"Test {rating}");
            MovieDescription description = MovieDescription.Create($"Rating {rating}");
            AgeRating ageRating = AgeRating.From(rating);
            PosterUrl posterUrl = PosterUrl.Create("https://example.com/test.jpg");

            // Act
            Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
                new MovieId(Guid.NewGuid()), title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
            );

            // Assert
            Assert.NotNull(movie);
        }
    }

    [Fact]
    public void Create_ShouldRaiseMovieRegisteredDomainEvent()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Event Test");
        MovieDescription description = MovieDescription.Create("Tests domain events");
        List<MovieGenre> genres = [MovieGenre.Create("Test")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/event.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        var domainEvent = Assert.Single(movie.DomainEvents);
        MovieRegistered movieRegistered = Assert.IsType<MovieRegistered>(domainEvent);
        Assert.Equal(id, movieRegistered.MovieId);
        Assert.Equal(title, movieRegistered.Title);
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllEvents()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Clear Test");
        MovieDescription description = MovieDescription.Create("Tests clearing events");
        List<MovieGenre> genres = [MovieGenre.Create("Test")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/clear.jpg");
        
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Act
        movie.ClearDomainEvents();

        // Assert
        Assert.Empty(movie.DomainEvents);
    }

    [Fact]
    public void ValidateState_WithValidAggregate_ShouldNotThrow()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Validate Test");
        MovieDescription description = MovieDescription.Create("Tests validation");
        List<MovieGenre> genres = [MovieGenre.Create("Test")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/validate.jpg");
        
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Act
        Exception? exception = Record.Exception(() => movie.ValidateState());

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ActorsCollection_ShouldBeReadOnly()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("ReadOnly Test");
        MovieDescription description = MovieDescription.Create("Tests immutability");
        List<MovieGenre> genres = [MovieGenre.Create("Test")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/readonly.jpg");
        
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Act
        bool isReadOnly = movie.Actors.GetType().Name.Contains("ReadOnly", StringComparison.Ordinal);

        // Assert
        Assert.True(isReadOnly);
    }

    [Fact]
    public void GenresCollection_ShouldBeReadOnly()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("GenreReadOnly Test");
        MovieDescription description = MovieDescription.Create("Tests genre immutability");
        List<MovieGenre> genres = [MovieGenre.Create("Test")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/genrereadonly.jpg");
        
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Act
        bool isReadOnly = movie.Genres.GetType().Name.Contains("ReadOnly", StringComparison.Ordinal);

        // Assert
        Assert.True(isReadOnly);
    }

    [Fact]
    public void Create_WithLargeTitleBoundary_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        string maxTitle = new('T', 50);
        MovieTitle title = MovieTitle.Create(maxTitle);
        MovieDescription description = MovieDescription.Create("Boundary test");
        List<MovieGenre> genres = [MovieGenre.Create("Test")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/boundary.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(maxTitle, movie.Title.Value);
    }

    [Fact]
    public void Create_WithLargeDescriptionBoundary_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Desc Boundary Test");
        string maxDesc = new('D', 300);
        MovieDescription description = MovieDescription.Create(maxDesc);
        List<MovieGenre> genres = [MovieGenre.Create("Test")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/descboundary.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(maxDesc, movie.Description.Value);
    }

    [Fact]
    public void Create_WithMultipleDuplicateGenres_ShouldIncludeAllGenres()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Duplicate Genres");
        MovieDescription description = MovieDescription.Create("Test duplicates");
        List<MovieGenre> genres = [MovieGenre.Create("Action"), MovieGenre.Create("Action"), MovieGenre.Create("Drama")];
        List<Actor> actors = [Actor.Create("Test Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/dupgenre.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(3, movie.Genres.Count);
    }

    [Fact]
    public void Create_WithMultipleDuplicateActors_ShouldIncludeAllActors()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Duplicate Actors");
        MovieDescription description = MovieDescription.Create("Test actor duplicates");
        List<MovieGenre> genres = [MovieGenre.Create("Drama")];
        List<Actor> actors = [Actor.Create("John"), Actor.Create("John"), Actor.Create("Jane")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/dupactor.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(3, movie.Actors.Count);
    }

    [Fact]
    public void Create_WithYearAfterMinimumBoundary_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Post-1888 Film");
        MovieDescription description = MovieDescription.Create("One year after minimum");
        List<MovieGenre> genres = [MovieGenre.Create("Historical")];
        List<Actor> actors = [Actor.Create("Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(1889);
        MovieDuration duration = MovieDuration.Create(90);
        AgeRating ageRating = AgeRating.From(0);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/1889.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(1889, movie.ReleaseYear.Value);
    }

    [Fact]
    public void Create_WithYearBeforeCurrentBoundary_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Previous Year Film");
        MovieDescription description = MovieDescription.Create("One year before current");
        List<MovieGenre> genres = [MovieGenre.Create("Drama")];
        List<Actor> actors = [Actor.Create("Actor")];
        int previousYear = DateTime.Now.Year - 1;
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(previousYear);
        MovieDuration duration = MovieDuration.Create(110);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/prevyear.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(previousYear, movie.ReleaseYear.Value);
    }

    [Fact]
    public void Create_WithLargeDuration_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Long Film");
        MovieDescription description = MovieDescription.Create("Very long duration");
        List<MovieGenre> genres = [MovieGenre.Create("Epic")];
        List<Actor> actors = [Actor.Create("Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2015);
        MovieDuration duration = MovieDuration.Create(500);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/long.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(500, movie.Duration.Value);
    }

    [Fact]
    public void DomainEvent_ShouldContainAllMovieData()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Full Event Data");
        MovieDescription description = MovieDescription.Create("Tests event data completeness");
        MovieGenre drama = MovieGenre.Create("Drama");
        MovieGenre thriller = MovieGenre.Create("Thriller");
        List<MovieGenre> genres = [drama, thriller];
        Actor actor1 = Actor.Create("Lead Actor");
        Actor actor2 = Actor.Create("Supporting Actor");
        List<Actor> actors = [actor1, actor2];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2022);
        MovieDuration duration = MovieDuration.Create(125);
        AgeRating ageRating = AgeRating.From(14);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/fullevent.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        var domainEvent = Assert.Single(movie.DomainEvents);
        MovieRegistered movieRegistered = Assert.IsType<MovieRegistered>(domainEvent);
        Assert.Equal(id, movieRegistered.MovieId);
        Assert.Equal(title, movieRegistered.Title);
        Assert.Equal(releaseYear, movieRegistered.ReleaseYear);
        Assert.Equal(duration, movieRegistered.Duration);
        Assert.Equal(ageRating, movieRegistered.AgeRating);
        Assert.Equal(posterUrl, movieRegistered.PosterUrl);
        Assert.Equal(2, movieRegistered.Genres.Count);
        Assert.Equal(2, movieRegistered.Actors.Count);
    }

    [Fact]
    public void Create_WithEmptyStringGenre_ShouldThrow()
    {
        // Arrange
        string emptyGenre = "";

        // Act & Assert
        Assert.Throws<InvalidMovieGenreException>(() => MovieGenre.Create(emptyGenre));
    }

    [Fact]
    public void Create_WithEmptyStringActor_ShouldThrow()
    {
        // Arrange
        string emptyActor = "";

        // Act & Assert
        Assert.Throws<InvalidActorException>(() => Actor.Create(emptyActor));
    }

    [Fact]
    public void Create_WithEmptyStringTitle_ShouldThrow()
    {
        // Arrange
        string emptyTitle = "";

        // Act & Assert
        Assert.Throws<InvalidMovieTitleException>(() => MovieTitle.Create(emptyTitle));
    }

    [Fact]
    public void Create_WithEmptyStringDescription_ShouldThrow()
    {
        // Arrange
        string emptyDesc = "";

        // Act & Assert
        Assert.Throws<InvalidMovieDescriptionException>(() => MovieDescription.Create(emptyDesc));
    }

    [Fact]
    public void Create_WithEmptyStringPosterUrl_ShouldThrow()
    {
        // Arrange
        string emptyUrl = "";

        // Act & Assert
        Assert.Throws<InvalidMoviePosterUrlException>(() => PosterUrl.Create(emptyUrl));
    }

    [Fact]
    public void Create_WithNegativeDuration_ShouldThrow()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<InvalidMovieDurationException>(() => MovieDuration.Create(-10));
    }

    [Fact]
    public void Create_WithFutureYear_ShouldThrow()
    {
        // Arrange
        int futureYear = DateTime.Now.Year + 1;

        // Act & Assert
        Assert.Throws<InvalidMovieReleaseYearException>(() => MovieReleaseYear.Create(futureYear));
    }

    [Fact]
    public void Id_ShouldBeUnique()
    {
        // Arrange
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        MovieId id1 = new(guid1);
        MovieId id2 = new(guid2);
        MovieTitle title = MovieTitle.Create("Test");
        MovieDescription description = MovieDescription.Create("Test");
        List<MovieGenre> genres = [MovieGenre.Create("Drama")];
        List<Actor> actors = [Actor.Create("Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/test.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie1 = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id1, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );
        Howestprime.Movies.Domain.Movies.Movie.Movie movie2 = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id2, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.NotEqual(movie1.Id, movie2.Id);
        Assert.Equal(guid1, movie1.Id.Value);
        Assert.Equal(guid2, movie2.Id.Value);
    }

    [Fact]
    public void Create_ShouldNotModifyInputCollections()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Collection Test");
        MovieDescription description = MovieDescription.Create("Tests collection immutability");
        List<MovieGenre> genres = [MovieGenre.Create("Drama"), MovieGenre.Create("Thriller")];
        List<Actor> actors = [Actor.Create("Actor1"), Actor.Create("Actor2")];
        int originalGenreCount = genres.Count;
        int originalActorCount = actors.Count;
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/colltest.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(originalGenreCount, genres.Count);
        Assert.Equal(originalActorCount, actors.Count);
        Assert.Equal(originalGenreCount, movie.Genres.Count);
        Assert.Equal(originalActorCount, movie.Actors.Count);
    }

    [Fact]
    public void GenreAndActorIndex_ShouldPreserveInsertionOrder()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("Order Test");
        MovieDescription description = MovieDescription.Create("Tests order preservation");
        MovieGenre genre1 = MovieGenre.Create("Action");
        MovieGenre genre2 = MovieGenre.Create("Drama");
        MovieGenre genre3 = MovieGenre.Create("Thriller");
        List<MovieGenre> genres = [genre1, genre2, genre3];
        Actor actor1 = Actor.Create("First");
        Actor actor2 = Actor.Create("Second");
        List<Actor> actors = [actor1, actor2];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(100);
        AgeRating ageRating = AgeRating.From(12);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/order.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(genre1, movie.Genres[0]);
        Assert.Equal(genre2, movie.Genres[1]);
        Assert.Equal(genre3, movie.Genres[2]);
        Assert.Equal(actor1, movie.Actors[0]);
        Assert.Equal(actor2, movie.Actors[1]);
    }

    [Fact]
    public void Create_WithAgeRatingZero_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("All Ages");
        MovieDescription description = MovieDescription.Create("Suitable for all ages");
        List<MovieGenre> genres = [MovieGenre.Create("Children")];
        List<Actor> actors = [Actor.Create("Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(80);
        AgeRating ageRating = AgeRating.From(0);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/allages.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(ageRating, movie.AgeRating);
    }

    [Fact]
    public void Create_WithMaxAgeRating_ShouldSucceed()
    {
        // Arrange
        MovieId id = new(Guid.NewGuid());
        MovieTitle title = MovieTitle.Create("18 Plus");
        MovieDescription description = MovieDescription.Create("Adults only");
        List<MovieGenre> genres = [MovieGenre.Create("Mature")];
        List<Actor> actors = [Actor.Create("Actor")];
        MovieReleaseYear releaseYear = MovieReleaseYear.Create(2020);
        MovieDuration duration = MovieDuration.Create(110);
        AgeRating ageRating = AgeRating.From(18);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/18plus.jpg");

        // Act
        Howestprime.Movies.Domain.Movies.Movie.Movie movie = Howestprime.Movies.Domain.Movies.Movie.Movie.Create(
            id, title, description, genres, actors, releaseYear, duration, ageRating, posterUrl
        );

        // Assert
        Assert.Equal(ageRating, movie.AgeRating);
    }
}
