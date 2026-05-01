using Howestprime.Movies.Application.Contracts.Data.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Data;

public sealed class MovieDataConfiguration : IEntityTypeConfiguration<MovieData>
{
    public void Configure(EntityTypeBuilder<MovieData> builder)
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

        builder.OwnsMany(m => m.Actors, actorBuilder =>
        {
            actorBuilder.ToJson();
            actorBuilder.Property(a => a.Name);
        });

        builder.OwnsMany(m => m.Genres, genreBuilder =>
        {
            genreBuilder.ToJson();
            genreBuilder.Property(g => g.Value);
        });
    }
}
