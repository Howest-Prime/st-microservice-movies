using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Movies.Room;
using Howestprime.Movies.Domain.Movies.Room.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Shared.Exceptions;
using UnitTests.Application.Shared;

namespace UnitTests.Application.MovieEvents;

public sealed class ScheduleMovieEventTests : ApplicationTestBase
{
    private readonly ScheduleMovieEvent _scheduleMovieEvent;

    public ScheduleMovieEventTests()
    {
        _scheduleMovieEvent = new ScheduleMovieEvent(UnitOfWork, CreateLogger<ScheduleMovieEvent>());
    }

    [Fact]
    public async Task Execute_WithMovieAndRoom_ShouldCreateMovieEvent()
    {
        // Arrange
        Movie movie = CreateMovie();
        Room room = Room.Create(EntityId.New<RoomId>(), "Room A", 80);
        await MovieRepository.Save(movie);
        await RoomRepository.Save(room);
        DateTime showTime = NextShowTime();

        // Act
        ScheduleMovieEventOutput output = await _scheduleMovieEvent.Execute(
            new ScheduleMovieEventInput(movie.Id.Value, room.Id.Value, showTime));

        // Assert
        MovieEvent movieEvent = (await MovieEventRepository.ById(new MovieEventId(output.MovieEventId))).Value;
        Assert.Equal(movie.Id.Value, movieEvent.MovieId);
        Assert.Equal(room.Id.Value, movieEvent.RoomId);
        Assert.Equal(showTime.ToUniversalTime(), movieEvent.ShowTime.Value);
        Assert.Equal(room.Capacity, movieEvent.Capacity.Value);
    }

    [Fact]
    public async Task Execute_WhenRoomAndShowTimeAlreadyExist_ShouldUpdateMovieOnExistingEvent()
    {
        // Arrange
        Movie originalMovie = CreateMovie();
        Movie newMovie = CreateMovie();
        Room room = Room.Create(EntityId.New<RoomId>(), "Room A", 100);
        DateTime showTime = NextShowTime();
        MovieEvent existingMovieEvent = MovieEvent.Create(
            EntityId.New<MovieEventId>(),
            originalMovie.Id.Value,
            room.Id.Value,
            MovieEventShowTime.Create(showTime),
            Capacity.Create(room.Capacity));
        await MovieRepository.Save(originalMovie);
        await MovieRepository.Save(newMovie);
        await RoomRepository.Save(room);
        await MovieEventRepository.Save(existingMovieEvent);

        // Act
        ScheduleMovieEventOutput output = await _scheduleMovieEvent.Execute(
            new ScheduleMovieEventInput(newMovie.Id.Value, room.Id.Value, showTime));

        // Assert
        MovieEvent updatedMovieEvent = (await MovieEventRepository.ById(new MovieEventId(output.MovieEventId))).Value;
        Assert.Equal(existingMovieEvent.Id.Value, output.MovieEventId);
        Assert.Equal(newMovie.Id.Value, updatedMovieEvent.MovieId);
    }

    [Fact]
    public async Task Execute_WithMissingMovie_ShouldThrow()
    {
        // Arrange
        Room room = Room.Create(EntityId.New<RoomId>(), "Room A", 100);
        await RoomRepository.Save(room);

        // Act
        Task act = _scheduleMovieEvent.Execute(
            new ScheduleMovieEventInput(Guid.NewGuid(), room.Id.Value, NextShowTime()));

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => act);
    }

    [Fact]
    public async Task Execute_WithMissingRoom_ShouldThrow()
    {
        // Arrange
        Movie movie = CreateMovie();
        await MovieRepository.Save(movie);

        // Act
        Task act = _scheduleMovieEvent.Execute(
            new ScheduleMovieEventInput(movie.Id.Value, Guid.NewGuid(), NextShowTime()));

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => act);
    }

    private static Movie CreateMovie()
    {
        return Movie.Create(
            EntityId.New<MovieId>(),
            MovieTitle.Create("Inception"),
            MovieDescription.Create("A skilled thief steals corporate secrets through dream-sharing technology."),
            [MovieGenre.Create("Sci-Fi"), MovieGenre.Create("Thriller")],
            [Actor.Create("Leonardo DiCaprio"), Actor.Create("Joseph Gordon-Levitt")],
            MovieReleaseYear.Create(2010),
            MovieDuration.Create(148),
            AgeRating.From(12),
            PosterUrl.Create("https://example.com/inception.jpg"));
    }

    private static DateTime NextShowTime()
    {
        DateTime showTime = DateTime.Now.Date.AddDays(1).AddHours(15);
        return showTime <= DateTime.Now ? showTime.AddDays(1) : showTime;
    }
}
