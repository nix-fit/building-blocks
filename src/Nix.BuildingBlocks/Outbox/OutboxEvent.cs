using System.ComponentModel.DataAnnotations;

namespace Nix.BuildingBlocks.Outbox;

/// <summary>
/// Базовая сущность для хранения событий в Outbox паттерне.
/// Обеспечивает транзакционную целостность между сохранением данных и публикацией событий.
/// </summary>
public class OutboxEvent
{
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Тип события (полное имя класса)
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string EventType { get; init; } = string.Empty;

    /// <summary>
    /// Сериализованные данные события в JSON формате
    /// </summary>
    [Required]
    public string EventData { get; init; } = string.Empty;

    /// <summary>
    /// Время создания события
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Время обработки события (null если не обработано)
    /// </summary>
    public DateTime? ProcessedAt { get; set; }

    /// <summary>
    /// Количество попыток обработки
    /// </summary>
    public int ProcessingAttempts { get; set; } = 0;

    /// <summary>
    /// Максимальное количество попыток обработки
    /// </summary>
    public int MaxRetryAttempts { get; init; } = 3;

    /// <summary>
    /// Время следующей попытки обработки (для retry логики)
    /// </summary>
    public DateTime? NextRetryAt { get; set; }

    /// <summary>
    /// Сообщение об ошибке при последней неудачной попытке
    /// </summary>
    [MaxLength(2000)]
    public string? LastError { get; set; }

    /// <summary>
    /// Проверка, обработано ли событие
    /// </summary>
    public bool IsProcessed => ProcessedAt.HasValue;

    /// <summary>
    /// Проверка, нужно ли повторить обработку
    /// </summary>
    public bool ShouldRetry => 
        !IsProcessed && 
        ProcessingAttempts < MaxRetryAttempts && 
        (NextRetryAt == null || NextRetryAt <= DateTime.UtcNow);

    /// <summary>
    /// Проверка, превышено ли максимальное количество попыток
    /// </summary>
    public bool IsMaxRetriesExceeded => ProcessingAttempts >= MaxRetryAttempts;

    /// <summary>
    /// Отмечает событие как обработанное
    /// </summary>
    public void MarkAsProcessed()
    {
        ProcessedAt = DateTime.UtcNow;
        LastError = null;
        NextRetryAt = null;
    }

    /// <summary>
    /// Отмечает неудачную попытку обработки и планирует следующую
    /// </summary>
    /// <param name="error">Сообщение об ошибке</param>
    public void MarkProcessingFailed(string error)
    {
        ProcessingAttempts++;
        LastError = error;
        
        if (ProcessingAttempts < MaxRetryAttempts)
        {
            // Экспоненциальная задержка: 1 мин, 5 мин, 25 мин
            var delayMinutes = Math.Pow(5, ProcessingAttempts - 1);
            NextRetryAt = DateTime.UtcNow.AddMinutes(delayMinutes);
        }
    }
} 
