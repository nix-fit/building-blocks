namespace Nix.Contracts.Events;

/// <summary>
/// Базовый класс для integration events с автоматической генерацией метаданных
/// </summary>
public abstract record IntegrationEventBase : IIntegrationEvent
{
    /// <inheritdoc />
    public Guid EventId { get; init; } = Guid.NewGuid();
    
    /// <inheritdoc />
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    
    /// <inheritdoc />
    public abstract string EventType { get; }
}

