using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Howestprime.Movies.Domain.Movies.MovieEvent;
using Howestprime.Movies.Domain.Movies.MovieEvent.ValueObjects;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Domain;

public sealed class MovieEventConfiguration : IEntityTypeConfiguration<MovieEvent>
{
    public void Configure(EntityTypeBuilder<MovieEvent> builder)
    {
        builder.ToTable("MovieEvents");

        builder.HasKey(movieEvent => movieEvent.Id);
        builder.Property(movieEvent => movieEvent.Id).ValueGeneratedNever();

        builder.Property(movieEvent => movieEvent.ShowTime)
            .HasConversion(
                v => v.Value,
                v => MovieEventShowTime.Create(v)
            ).IsRequired();
        builder.Property(movieEvent => movieEvent.MovieId).IsRequired();
        builder.Property(movieEvent => movieEvent.RoomId).IsRequired();
        builder.Property(movieEvent => movieEvent.Capacity).IsRequired();
        
         builder.OwnsMany(movie => movie.Bookings, actorBuilder =>
        {
            actorBuilder.ToJson();

            actorBuilder.Property(booking => booking.Id).IsRequired();

            actorBuilder.Property(booking => booking.BookingStatus)
                    .HasConversion<string>()
                    .IsRequired();

            actorBuilder.Property(booking => booking.PaymentStatus)
                    .HasConversion<string>()
                    .IsRequired();

            actorBuilder.Property(booking => booking.StandardVisitors).IsRequired();
            actorBuilder.Property(booking => booking.DiscountVisitors).IsRequired();
            actorBuilder.Property(booking => booking.SeatNumbers).IsRequired();
        });
    }
}