using Howestprime.Movies.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Data;

public sealed class RoomDataConfiguration : IEntityTypeConfiguration<RoomData>
{
    public void Configure(EntityTypeBuilder<RoomData> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Capacity).IsRequired();
    }

}
