using MediatR;

namespace Nix.BuildingBlocks.Domain;

public class DomainEvent : INotification
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
