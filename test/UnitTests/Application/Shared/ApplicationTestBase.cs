using Howestprime.Movies.Domain.Movies.Movie.Repositories;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.Room;
using Howestprime.Movies.Domain.Shared.DomainEvents;
using Howestprime.Movies.Infrastructure.Messaging.DomainEvents;
using UnitTests.Application.Mocks;
using UnitTests.Mocks;

namespace UnitTests.Application.Shared;

public abstract class ApplicationTestBase
{
    protected readonly IDomainEventPublisher DomainEventPublisher;
    protected readonly InMemoryMovieRepository MovieRepository;
    protected readonly InMemoryMovieEventRepository MovieEventRepository;
    protected readonly InMemoryRoomRepository RoomRepository;
    protected readonly InMemoryUnitOfWork UnitOfWork;

    protected ApplicationTestBase()
    {
        DomainEventPublisher = new DomainEventPublisher([], CreateLogger<DomainEventPublisher>());
        MovieRepository = new InMemoryMovieRepository();
        MovieEventRepository = new InMemoryMovieEventRepository();
        RoomRepository = new InMemoryRoomRepository();

        UnitOfWork = new InMemoryUnitOfWork(DomainEventPublisher, CreateLogger<InMemoryUnitOfWork>());
        UnitOfWork.RegisterRepository<IMovieRepository>(MovieRepository);
        UnitOfWork.RegisterRepository<IMovieEventRepository>(MovieEventRepository);
        UnitOfWork.RegisterRepository<IRoomRepository>(RoomRepository);
    }

    protected static TestLogger<T> CreateLogger<T>() => new();
}
