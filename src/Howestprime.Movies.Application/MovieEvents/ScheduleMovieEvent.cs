using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;
using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Domain.Movies.Room;
using Howestprime.Movies.Domain.Movies.Room.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record ScheduleMovieEventInput( 
    Guid MovieID,
    Guid RoomID,
    DateTime ShowTime
);

public sealed record ScheduleMovieEventOutput(
    Guid MovieEventID
);

public sealed class ScheduleMovieEvent(
    IUnitOfWork uow,
    ILogger<ScheduleMovieEvent> logger
) : IUseCase<ScheduleMovieEventInput, ScheduleMovieEventOutput>
{
    public async Task<ScheduleMovieEventOutput> Execute(ScheduleMovieEventInput input)
    {
        MovieId movieId = EntityId.New<MovieId>(input.MovieID);
        RoomID roomId = EntityId.New<RoomID>(input.RoomID);

        MovieEventShowTime normalizedShowTime = MovieEventShowTime.Create(input.ShowTime);
        MovieEvent? existingMovieEvent = await uow.Repo<IMovieEventRepository>().ByRoomIdAndShowTime(roomId.Value, normalizedShowTime);
        
        if(existingMovieEvent != null)
        {
            existingMovieEvent.UpdateMovie(movieId.Value);

            await uow.Save<IMovieEventRepository>(existingMovieEvent);
            await uow.Do();

            logger.LogInformation("Updated existing movie event with id {MovieEventID} to movie {MovieID} in room {RoomID} at {ShowTime}.", existingMovieEvent.Id, existingMovieEvent.MovieId, existingMovieEvent.RoomId, existingMovieEvent.ShowTime);
            return new ScheduleMovieEventOutput(existingMovieEvent.Id.Value);
        }
    
        Movie movie = (await uow.Repo<IMovieRepository>().ById(movieId))
                        .OrElseThrow(() => new NotFoundException($"Movie with id {input.MovieID} not found."));

        Room room = (await uow.Repo<IRoomRepository>().ById(roomId))
                        .OrElseThrow(() => new NotFoundException($"Room with id {input.RoomID} not found."));

        MovieEvent newMovieEvent = MovieEvent.Create(
            EntityId.New<MovieEventID>(),
            movieId.Value,
            roomId.Value,
            normalizedShowTime,
            Capacity.Create(room.Capacity)
        );

        await uow.Save<IMovieEventRepository>(newMovieEvent);
        await uow.Do();
        
        logger.LogInformation("Scheduled movie event with id {MovieEventID} for movie {MovieID} in room {RoomID} at {ShowTime}.", newMovieEvent.Id, newMovieEvent.MovieId, newMovieEvent.RoomId, newMovieEvent.ShowTime);
        return new ScheduleMovieEventOutput(newMovieEvent.Id.Value);
    }


}