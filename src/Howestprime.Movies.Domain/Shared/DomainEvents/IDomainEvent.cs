namespace Howestprime.Movies.Domain.Shared.DomainEvents;

public interface IDomainEvent
{
    string FQDN { get; }
    DateTimeOffset OccurredOn { get; }
}
