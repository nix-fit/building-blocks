namespace Nix.BuildingBlocks.Outbox;

/// <summary>
/// Интерфейс репозитория для работы с Outbox событиями.
/// Обеспечивает транзакционное сохранение и извлечение событий.
/// </summary>
public interface IOutboxRepository
{
    /// <summary>
    /// Добавляет новое событие в Outbox в рамках текущей транзакции
    /// </summary>
    /// <param name="outboxEvent">Событие для добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task AddAsync(OutboxEvent outboxEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет несколько событий в Outbox в рамках текущей транзакции
    /// </summary>
    /// <param name="outboxEvents">События для добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task AddRangeAsync(IEnumerable<OutboxEvent> outboxEvents, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает необработанные события, готовые для обработки
    /// </summary>
    /// <param name="batchSize">Максимальное количество событий в пакете</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список событий для обработки</returns>
    Task<List<OutboxEvent>> GetUnprocessedEventsAsync(int batchSize = 100, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет состояние события после попытки обработки
    /// </summary>
    /// <param name="outboxEvent">Событие для обновления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task UpdateAsync(OutboxEvent outboxEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет состояние нескольких событий после попытки обработки
    /// </summary>
    /// <param name="outboxEvents">События для обновления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task UpdateRangeAsync(IEnumerable<OutboxEvent> outboxEvents, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет обработанные события старше указанного времени (для очистки)
    /// </summary>
    /// <param name="olderThan">Время, старше которого события будут удалены</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество удаленных событий</returns>
    Task<int> DeleteProcessedEventsOlderThanAsync(DateTime olderThan, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает количество необработанных событий (для мониторинга)
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество необработанных событий</returns>
    Task<int> GetUnprocessedEventsCountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает количество событий с превышенным лимитом повторов (для мониторинга)
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество событий с превышенным лимитом</returns>
    Task<int> GetFailedEventsCountAsync(CancellationToken cancellationToken = default);
} 
