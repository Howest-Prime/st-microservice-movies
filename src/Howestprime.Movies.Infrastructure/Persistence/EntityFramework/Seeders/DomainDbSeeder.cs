using Microsoft.Extensions.Logging;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Seeders;

using Howestprime.Movies.Domain.Movies.Room;
using Howestprime.Movies.Domain.Movies.Room.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Microsoft.Extensions.Logging;


public class DomainDbSeeder(DomainDbContext context, ILogger<DomainDbSeeder> logger)
{
    public async Task Seed()
    {
        await SeedRooms();
    }

    private async Task SeedRooms()
    {
        if (context.Rooms.Any())
        {
            logger.LogInformation("Rooms already seeded, skipping.");
            return;
        }

        logger.LogInformation("Seeding rooms...");

        var rooms = new List<Room>
        {
            Room.Create(EntityId.New<RoomId>(Guid.Parse("019d059e-d220-71db-8a1a-ec7569492999")),"Blue Room", 100),
            Room.Create(EntityId.New<RoomId>(Guid.Parse("019d059e-d220-75fe-b936-0a97cd75216e")), "Yellow Room", 80)

        };

        context.Rooms.AddRange(rooms);
        await context.SaveChangesAsync();

        logger.LogInformation("Seeded {Count} rooms.", rooms.Count);
    }
}
