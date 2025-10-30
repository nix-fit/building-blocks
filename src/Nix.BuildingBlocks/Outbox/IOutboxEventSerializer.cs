using Nix.BuildingBlocks.Domain;

namespace Nix.BuildingBlocks.Outbox;

/// <summary>
/// Интерфейс для сериализации и десериализации доменных событий в Outbox.
/// Обеспечивает преобразование событий в формат, пригодный для хранения.
/// </summary>
public interface IOutboxEventSerializer
{
    /// <summary>
    /// Сериализует доменное событие в OutboxEvent
    /// </summary>
    /// <param name="domainEvent">Доменное событие для сериализации</param>
    /// <returns>OutboxEvent готовый для сохранения</returns>
    OutboxEvent Serialize(DomainEvent domainEvent);

    /// <summary>
    /// Десериализует OutboxEvent обратно в доменное событие
    /// </summary>
    /// <param name="outboxEvent">OutboxEvent из хранилища</param>
    /// <returns>Восстановленное доменное событие</returns>
    DomainEvent Deserialize(OutboxEvent outboxEvent);

    /// <summary>
    /// Проверяет, может ли сериализатор обработать данный тип события
    /// </summary>
    /// <param name="eventType">Тип события для проверки</param>
    /// <returns>True если может обработать, false в противном случае</returns>
    bool CanHandle(string eventType);

    /// <summary>
    /// Получает все поддерживаемые типы событий
    /// </summary>
    /// <returns>Список поддерживаемых типов событий</returns>
    IEnumerable<string> GetSupportedEventTypes();
} 
