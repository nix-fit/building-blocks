using System.Text.Json;
using System.Text.Json.Serialization;
using Nix.BuildingBlocks.Domain;

namespace Nix.BuildingBlocks.Outbox;

/// <summary>
/// Реализация сериализатора Outbox событий на основе System.Text.Json.
/// Оптимизирована для Native AOT и высокой производительности.
/// </summary>
public class JsonOutboxEventSerializer : IOutboxEventSerializer
{
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly Dictionary<string, Type> _eventTypes;

    public JsonOutboxEventSerializer()
    {
        // Настройки JSON сериализатора для максимальной производительности
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        };

        // Кэш типов событий для быстрого поиска
        _eventTypes = new Dictionary<string, Type>();
        RegisterEventTypes();
    }

    /// <summary>
    /// Сериализует доменное событие в OutboxEvent
    /// </summary>
    public OutboxEvent Serialize(DomainEvent domainEvent)
    {
        var eventType = domainEvent.GetType();
        var eventTypeName = GetEventTypeName(eventType);
        
        try
        {
            var eventData = JsonSerializer.Serialize(domainEvent, eventType, _jsonOptions);
            
            return new OutboxEvent
            {
                EventType = eventTypeName,
                EventData = eventData,
                CreatedAt = DateTime.UtcNow
            };
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"Failed to serialize domain event of type {eventTypeName}: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Десериализует OutboxEvent обратно в доменное событие
    /// </summary>
    public DomainEvent Deserialize(OutboxEvent outboxEvent)
    {
        if (!_eventTypes.TryGetValue(outboxEvent.EventType, out var eventType))
        {
            throw new InvalidOperationException(
                $"Unknown event type: {outboxEvent.EventType}. Make sure it's registered in the serializer.");
        }

        try
        {
            var domainEvent = JsonSerializer.Deserialize(outboxEvent.EventData, eventType, _jsonOptions);
            
            if (domainEvent is not DomainEvent result)
            {
                throw new InvalidOperationException(
                    $"Deserialized object is not a DomainEvent: {eventType.Name}");
            }

            return result;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"Failed to deserialize event of type {outboxEvent.EventType}: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Проверяет, может ли сериализатор обработать данный тип события
    /// </summary>
    public bool CanHandle(string eventType)
    {
        return _eventTypes.ContainsKey(eventType);
    }

    /// <summary>
    /// Получает все поддерживаемые типы событий
    /// </summary>
    public IEnumerable<string> GetSupportedEventTypes()
    {
        return _eventTypes.Keys;
    }

    /// <summary>
    /// Регистрирует типы событий для сериализации.
    /// В реальном приложении это может быть автоматическое сканирование сборок.
    /// </summary>
    private void RegisterEventTypes()
    {
        // Для Native AOT регистрируем типы явно
        // В будущем можно добавить автоматическое сканирование через рефлексию
        
        // Пока оставляем пустым - типы будут регистрироваться в конкретных сервисах
    }

    /// <summary>
    /// Регистрирует новый тип события для сериализации
    /// </summary>
    /// <typeparam name="T">Тип доменного события</typeparam>
    public void RegisterEventType<T>() where T : DomainEvent
    {
        var eventType = typeof(T);
        var eventTypeName = GetEventTypeName(eventType);
        _eventTypes[eventTypeName] = eventType;
    }

    /// <summary>
    /// Регистрирует тип события по Type
    /// </summary>
    /// <param name="eventType">Тип доменного события</param>
    public void RegisterEventType(Type eventType)
    {
        if (!typeof(DomainEvent).IsAssignableFrom(eventType))
        {
            throw new ArgumentException($"Type {eventType.Name} must inherit from DomainEvent", nameof(eventType));
        }

        var eventTypeName = GetEventTypeName(eventType);
        _eventTypes[eventTypeName] = eventType;
    }

    /// <summary>
    /// Получает стандартизированное имя типа события
    /// </summary>
    private static string GetEventTypeName(Type eventType)
    {
        // Используем полное имя типа для уникальности
        return eventType.FullName ?? eventType.Name;
    }
} 
