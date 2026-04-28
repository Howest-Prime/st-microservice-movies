using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Howestprime.Movies.Domain.Movies.Movie;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Domain;

public sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(movie => movie.Id);
        builder.Property(movie => movie.Id).ValueGeneratedNever();

        builder.Property(movie => movie.Title).IsRequired();
        builder.Property(movie => movie.Description).IsRequired();
        builder.Property(movie => movie.ReleaseYear).IsRequired();
        builder.Property(movie => movie.Duration).IsRequired();
        builder.Property(movie => movie.AgeRating).IsRequired();
        builder.Property(movie => movie.PosterUrl).IsRequired();

        builder.OwnsMany(movie => movie.Actors, actorBuilder =>
        {
            actorBuilder.ToJson();
            actorBuilder.Property(actor => actor.Value).IsRequired();
        });

        builder.OwnsMany(movie => movie.Genres, genreBuilder =>
        {
            genreBuilder.ToJson();
            genreBuilder.Property(genre => genre.Value).IsRequired();
        });
    }
}
