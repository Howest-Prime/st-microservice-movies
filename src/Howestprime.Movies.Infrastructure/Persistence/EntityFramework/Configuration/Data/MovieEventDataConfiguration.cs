using Howestprime.Movies.Application.Contracts.Data.MovieEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Data;

public sealed class MovieEventDataConfiguration : IEntityTypeConfiguration<MovieEventData>
{
    public void Configure(EntityTypeBuilder<MovieEventData> builder)
    {
        builder.ToTable("MovieEvents");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.ShowTime).IsRequired();
        builder.Property(e => e.Capacity).IsRequired();
        builder.HasOne(e => e.Movie)
            .WithMany()
            .HasForeignKey("MovieId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.Room)
            .WithMany()
            .HasForeignKey("RoomId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }

}
