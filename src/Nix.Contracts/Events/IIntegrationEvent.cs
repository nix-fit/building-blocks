namespace Nix.Contracts.Events;

/// <summary>
/// Базовый интерфейс для integration events между сервисами
/// </summary>
public interface IIntegrationEvent
{
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    Guid EventId { get; }
    
    /// <summary>
    /// Время возникновения события (UTC)
    /// </summary>
    DateTime OccurredAt { get; }
    
    /// <summary>
    /// Тип события в формате: service.entity.action.version
    /// Пример: "enrollment.access.granted.v1"
    /// </summary>
    string EventType { get; }
}

