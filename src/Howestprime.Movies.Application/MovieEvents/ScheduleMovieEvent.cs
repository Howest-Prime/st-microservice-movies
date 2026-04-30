

using Aornis;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Domain.MovieEvent.ValueObject;
using Howestprime.Movies.Domain.Movies.Movie;
using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Domain.Movies.Movie.ValueObjects;
using Howestprime.Movies.Domain.Movies.Room;
using Howestprime.Movies.Domain.Movies.Room.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Shared.Exceptions;
using Howestprime.Movies.Application.MovieEvents.Exceptions;
using Microsoft.Extensions.Logging;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

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
    IUnitOfWork UoW,
    ILogger<ScheduleMovieEvent> Logger
) : IUseCase<ScheduleMovieEventInput, ScheduleMovieEventOutput>
{
    public async Task<ScheduleMovieEventOutput> Execute(ScheduleMovieEventInput input)
    {
       MovieId movieId = EntityId.New<MovieId>(input.MovieID);
       Movie? movie = (await UoW.Repo<IMovieRepository>().ById(movieId))
                        .OrElseThrow(() => new NotFoundException($"Movie with id {input.MovieID} not found."));

       RoomID roomId = EntityId.New<RoomID>(input.RoomID);
       Room? room = (await UoW.Repo<IRoomRepository>().ById(roomId))
                        .OrElseThrow(() => new NotFoundException($"Room with id {input.RoomID} not found."));

    

        MovieEventShowTime normalizedShowTime = MovieEventShowTime.Create(input.ShowTime);
        bool movieAlreadyScheduled = await UoW.Repo<IMovieEventRepository>().ByIdAndShowTime(movieId, normalizedShowTime);
        
        if(movieAlreadyScheduled)
        {
            throw new EventAlreadyExists(input.MovieID, input.ShowTime);
        }

            MovieEvent newMovieEvent = MovieEvent.Create(
            EntityId.New<MovieEventID>(),
            movieId.Value,
            roomId.Value,
            normalizedShowTime
        );

        await UoW.Save<IMovieEventRepository>(newMovieEvent);
        await UoW.Do();
        
        Logger.LogInformation("Scheduled movie event with id {MovieEventID} for movie {MovieID} in room {RoomID} at {ShowTime}.", newMovieEvent.Id, newMovieEvent.MovieId, newMovieEvent.RoomId, newMovieEvent.ShowTime);
        return new ScheduleMovieEventOutput(newMovieEvent.Id.Value);
    }


}