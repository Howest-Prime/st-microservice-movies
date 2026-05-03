using Howestprime.Movies.Domain.Movies.Room;
using Howestprime.Movies.Domain.Movies.Room.ValueObjects;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public class EFCoreRoomRepository : EfCoreGenericRepository<Room, RoomId>, IRoomRepository
{
    public EFCoreRoomRepository(DomainDbContext context) : base(context) { }

}