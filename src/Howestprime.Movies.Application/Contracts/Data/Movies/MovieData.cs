namespace Howestprime.Movies.Application.Contracts.Data.Movies;

public sealed record MovieData
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int ReleaseYear { get; init; }
    public int Duration { get; init; }
    public IReadOnlyList<GenreData> Genres { get; init; } = new List<GenreData>();
    public IReadOnlyList<ActorData> Actors { get; init; } = new List<ActorData>();
    public int AgeRating { get; init; }
    public string PosterUrl { get; init; } = string.Empty;
}