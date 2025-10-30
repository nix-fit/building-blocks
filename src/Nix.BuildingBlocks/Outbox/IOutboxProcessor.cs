namespace Nix.BuildingBlocks.Outbox;

/// <summary>
/// Интерфейс для обработки событий из Outbox.
/// Обеспечивает надежную доставку событий с retry логикой.
/// </summary>
public interface IOutboxProcessor
{
    /// <summary>
    /// Обрабатывает пакет необработанных событий из Outbox
    /// </summary>
    /// <param name="batchSize">Размер пакета для обработки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество успешно обработанных событий</returns>
    Task<int> ProcessPendingEventsAsync(int batchSize = 100, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обрабатывает одно конкретное событие
    /// </summary>
    /// <param name="outboxEvent">Событие для обработки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True если событие обработано успешно, false в противном случае</returns>
    Task<bool> ProcessEventAsync(OutboxEvent outboxEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Очищает обработанные события старше указанного времени
    /// </summary>
    /// <param name="olderThan">Время, старше которого события будут удалены</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество удаленных событий</returns>
    Task<int> CleanupProcessedEventsAsync(DateTime olderThan, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает статистику Outbox для мониторинга
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Статистика обработки событий</returns>
    Task<OutboxStatistics> GetStatisticsAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Статистика работы Outbox для мониторинга
/// </summary>
public record OutboxStatistics(
    int UnprocessedEvents,
    int FailedEvents,
    int ProcessedEventsToday,
    DateTime LastProcessingTime
); 
